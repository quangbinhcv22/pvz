using UnityEngine;

public class IcePet : Ally
{
    [Space] public float freezeTime = 1.5f;

    private Effect freezeEffect;


    protected override void Init()
    {
        base.Init();

        freezeEffect = new Effect()
        {
            id = EffectID.Freeze,
            duration = freezeTime,
        };
    }

    protected override void OnActionDone()
    {
        foreach (var enemy in enemyDetector.enemies)
        {
            enemy.AddEffect(freezeEffect);
        }

        ReturnPool();
    }
}