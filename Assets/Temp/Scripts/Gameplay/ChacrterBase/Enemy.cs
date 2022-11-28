using System.Linq;
using Kryz.CharacterStats;
using UnityEngine;

public class Enemy : Character
{
    protected override void InitStats()
    {
        base.InitStats();

        Stats.Add(StatType.MovementSpeed, new CharacterStat());
    }

    public override void SetConfig(Team team, CharacterData data)
    {
        base.SetConfig(team, data);

        var enemyConfig = (EnemyData)data;

        Stats[StatType.MovementSpeed].RuntimeBaseValue = enemyConfig.moveSpeed;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        MonoLifeCycle.OnUpdate -= Moving;
    }

    public override void StartCycle()
    {
        EnableLogic();

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
        transform.position += new Vector3(directionTest * Stats[StatType.MovementSpeed].Value * Time.deltaTime, 0);
    }
}