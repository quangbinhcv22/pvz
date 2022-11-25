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
        if (!enemyDetector) nextStateCallback = LoopStateAction;
        timeline.SetSeconds(delayStartCycle).SetCallback(NextState).Restart();
    }

    protected virtual void NextState()
    {
        timeline.Stop();
        nextStateCallback?.Invoke();
    }


    protected virtual void LoopStateAction()
    {
        if (!enemyDetector || enemyDetector.enemies.Any())
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
        animator.PlayLast(StateName.Die);
    }
    
    protected virtual void OnDieDone()
    {
        ReturnPool();
    }
}