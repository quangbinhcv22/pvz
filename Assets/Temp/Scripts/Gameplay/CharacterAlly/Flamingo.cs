using UnityEngine;

public class Flamingo : Ally
{
    [Space] public int damage = 5;
    public int powerAttackCount = 3;
    public float powerForce = 3;

    private int attackCount;
    public bool IsPowerAttack => attackCount >= powerAttackCount;


    protected override void StartAction()
    {
        attackCount++;

        if (IsPowerAttack)
        {
            StartPowerAction();
        }
        else
        {
            base.StartAction();
        }
    }

    private void StartPowerAction()
    {
        // timeline.SetSeconds(actionTime.Value).SetCallback(OnActionDone).Restart();
    }


    protected override void OnActionDone()
    {
        base.OnActionDone();


        foreach (var enemy in EnemyDetector.enemies)
        {
            enemy.TakeDamage(damage, DamageSourceType.Normal);

            if (IsPowerAttack)
            {
                enemy.AddForce(new Vector2(powerForce * directionTest, default));
            }
        }


        if (IsPowerAttack) attackCount = 0;
    }
}