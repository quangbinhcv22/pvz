using UnityEngine;

public class AllyInstantAction : Ally
{
    public override void StartCycle()
    {
        EnableLogic();

        Debug.Log("Start Action");

        StartAction();
    }

    protected override void OnActionDone()
    {
        timeline.Stop();

        Debug.Log("On Action Done");


        foreach (var enemy in EnemyDetector.enemies)
        {
            ActionForEnemy(enemy);
        }


        Debug.Log("Return");

        ReturnPool();
    }

    protected virtual void ActionForEnemy(Character enemy)
    {
    }
}