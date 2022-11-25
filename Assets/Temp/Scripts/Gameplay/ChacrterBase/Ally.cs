using UnityEngine;

public class Ally : Character
{
    public Vector2Int Index { get; set; }


    public virtual void StartUltimate()
    {
        SwitchState(StateName.Ultimate);
        timeline.SetSeconds(ultimateTime).SetCallback(OnUltimateDone).Restart();
    }

    protected virtual void OnUltimateDone()
    {
        timeline.SetSeconds(attackBreakTime).SetCallback(NextState).Restart();
    }

    protected override void ReturnPool()
    {
        base.ReturnPool();
        BoardLogic.OnDie(Index, this);
    }
}