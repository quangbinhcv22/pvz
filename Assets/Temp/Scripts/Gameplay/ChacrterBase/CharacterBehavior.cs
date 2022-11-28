using System;
using System.Linq;
using UnityEngine;

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
        EnableLogic();

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
        timeline.SetSeconds(Stats[StatType.AttackDuration].Value).SetCallback(OnActionDone).Restart();
    }

    protected virtual void OnActionDone()
    {
        var AttackBreakTime = Stats[StatType.AttackCooldown].Value / Stats[StatType.AttackSpeed].Value;
        timeline.SetSeconds(AttackBreakTime).SetCallback(NextState).Restart();
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
        Debug.Log("Enable");

        if (SelfCollider) SelfCollider.enabled = true;
        if (EnemyDetector) EnemyDetector.enabled = true;
    }

    protected void DisableLogic()
    {
        Debug.Log("Disable");

        if (SelfCollider) SelfCollider.enabled = false;
        if (EnemyDetector) EnemyDetector.enabled = false;
    }
}