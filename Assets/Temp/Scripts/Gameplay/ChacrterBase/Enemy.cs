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
                enemy.TakeDamage(config.attackDamage);
            }
        }
        else
        {
            NextState();
        }
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
        
        animator.PlayLoop(StateName.Move);
    }

    protected override void StartAction()
    {
        MonoLifeCycle.OnUpdate -= Moving;

        base.StartAction();
    }

    protected virtual void Moving()
    {
        transform.position += new Vector3(directionTest * moveTime * Time.deltaTime, 0);
    }
}