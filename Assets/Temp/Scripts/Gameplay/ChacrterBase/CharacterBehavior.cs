using System;
using System.Linq;

public partial class Character
{
    protected Action nextStateCallback;

    protected Action NextStateCallback
    {
        get => nextStateCallback;
        set
        {
            nextStateCallback = value;
            if (!timeline.IsRunning) NextState();
        }
    }


    public virtual void StartCycle()
    {
        if (!EnemyDetector) nextStateCallback = LoopStateAction;
        timeline.SetSeconds(delayStartCycle).SetCallback(NextState).Restart();
    }

    protected virtual void NextState()
    {
        timeline.Stop();
        nextStateCallback?.Invoke();
    }


    protected virtual void LoopStateAction()
    {
        if (!EnemyDetector || EnemyDetector.enemies.Any())
        {
            StartAction();
        }
    }

    protected virtual void StartAction()
    {
        animator.PlayOnce(StateName.Attack);
        timeline.SetSeconds(actionTime.Value).SetCallback(OnActionDone).Restart();
    }

    protected virtual void OnActionDone()
    {
        timeline.SetSeconds(attackBreakTime).SetCallback(NextState).Restart();
    }


    protected virtual void StartDie()
    {
        SelfCollider.enabled = false;
        EnemyDetector.enabled = false;

        animator.PlayLast(StateName.Die);

        
        timeline.SetSeconds(_data.animations.die.Animation.Duration).SetCallback(OnDieDone).Restart();

        OnDieDone();
    }

    protected virtual void OnDieDone()
    {
        ReturnPool();
    }
}