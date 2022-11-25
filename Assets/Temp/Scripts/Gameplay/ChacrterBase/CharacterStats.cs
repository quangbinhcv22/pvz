using UnityEngine;

public partial class Character
{
    [Space] [SerializeField] private Team teamTest = Team.Ally;
    public int directionTest = 1;

    [Space] public float presetAttackSpeedFactor = 1f;

    [Space] public float delayStartCycle = 1f;
    public float ultimateTime = 1f;


    public readonly ObservableFloat actionTime = new();
    public readonly ObservableFloat actionCooldown = new();
    public readonly ObservableFloat actionSpeedFactor = new();


    public readonly ObservableLimitFloat HpCurrent = new();
    public readonly ObservableFloat HpMax = new();


    protected float attackBreakTime;

    private void ReCalculateAttackBreakTime(float unknown)
    {
        attackBreakTime = actionCooldown.Value / actionSpeedFactor.Value;
    }
}