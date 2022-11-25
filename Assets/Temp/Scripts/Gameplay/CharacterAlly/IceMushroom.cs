using UnityEngine;

public class IceMushroom : AllyInstantAction
{
    [Space] public float freezeTime = 3f;

    private Effect freezeEffect;


    // protected override void Init()
    // {
    //     base.Init();
    //
    //     freezeEffect = new Effect()
    //     {
    //         id = EffectID.Freeze,
    //         duration = freezeTime,
    //     };
    // }


    protected override void ActionForEnemy(Character enemy)
    {
        enemy.AddEffect(freezeEffect);
    }
}