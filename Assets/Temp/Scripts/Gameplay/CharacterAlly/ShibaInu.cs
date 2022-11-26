using UnityEngine;

public class ShibaInu : Ally
{
    [Space]
    public int damage = 5;
    public float stunRate = 0.25f;
    public float stunDuration = 0.25f;

    private Effect stunEffect;

    protected override void Init()
    {
        base.Init();

        stunEffect = new Effect()
        {
            id = EffectID.Stun,
            duration = stunDuration,
        };
    }

    protected override void OnActionDone()
    {
        base.OnActionDone();

        foreach (var enemy in EnemyDetector.enemies)
        {
            enemy.TakeDamage(damage, DamageSourceType.Normal);

            if (RandomUtility.IsHappen(stunRate))
            {
                enemy.AddEffect(stunEffect);
            }
        }
    }
}