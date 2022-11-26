using System.Linq;
using UnityEngine;

public class Enemy : Character
{
    [Space] public float moveTime = 0.2f;


    protected override void OnDisable()
    {
        base.OnDisable();
        MonoLifeCycle.OnUpdate -= Moving;
    }

    public override void StartCycle()
    {
        NextStateCallback = LoopStateAction;
    }

    protected override void OnActionDone()
    {
        if (EnemyDetector.enemies.Any())
        {
            base.OnActionDone();

            foreach (var enemy in EnemyDetector.enemies)
            {
                enemy.TakeDamage(_data.attackDamage);
            }
        }
        else
        {
            NextState();
        }
    }


    protected override void SwitchState(string stateName)
    {
        if (stateName != StateName.Walk && stateName != StateName.Run)
        {
            MonoLifeCycle.OnUpdate -= Moving;
        }

        animator.PlayOnce(StateName.Ultimate);

        base.SwitchState(stateName);
    }

    protected override void LoopStateAction()
    {
        if (EnemyDetector.enemies.Any())
        {
            StartAction();
        }
        else
        {
            StartMove();
        }
    }


    protected virtual void StartMove()
    {
        timeline.Stop();


        MonoLifeCycle.OnUpdate -= Moving;
        MonoLifeCycle.OnUpdate += Moving;

        SwitchState(StateName.Walk);
    }

    protected virtual void Moving()
    {
        transform.position += new Vector3(directionTest * moveTime * Time.deltaTime, 0);
    }
}