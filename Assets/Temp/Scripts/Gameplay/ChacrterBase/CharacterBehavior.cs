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
        animator.ReUse();
        animator.SetStateDefault();
        
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
        DisableLogic();
        
        animator.PlayLast(StateName.Die);

        timeline.SetSeconds(config.animations.die.Animation.Duration).SetCallback(OnDieDone).Restart();
    }

    protected virtual void OnDieDone()
    {
        ReturnPool();
    }


    protected void EnableLogic()
    {
        SelfCollider.enabled = true;
        EnemyDetector.enabled = true;
    }

    protected void DisableLogic()
    {
        SelfCollider.enabled = false;
        EnemyDetector.enabled = false;
    }
}