using System.Collections.Generic;
using Character_Stats;
using Kryz.CharacterStats;
using UnityEngine;

public partial class Character
{
    public Dictionary<StatType, CharacterStat> Stats;


    protected virtual void InitStats()
    {
        Stats = new();

        Stats.Add(StatType.HealthMax, new CharacterStat());
        {
            Stats[StatType.HealthMax].OnValueChanged += UpdateHpBar;
        }

        Stats.Add(StatType.HealthCurrent, new StatClamp()
        {
            MinNumber = 0f,
            MaxStat = Stats[StatType.HealthMax],
        });
        {
            Stats[StatType.HealthCurrent].OnValueChanged += OnHealthChanged;
            Stats[StatType.HealthCurrent].OnValueChanged += UpdateHpBar;
        }
    }

    public void SetStats(CharacterData data)
    {
        if (Stats == null) InitStats();

        Stats[StatType.HealthMax].RuntimeBaseValue = data.health;
        Stats[StatType.HealthCurrent].RuntimeBaseValue = data.health;
    }


    protected void OnHealthChanged()
    {
        var healthCurrent = Stats[StatType.HealthCurrent].RuntimeBaseValue;

        if (healthCurrent <= 0)
        {
            StartDie();
        }
    }


    [Space] [SerializeField] private Team teamTest = Team.Ally;
    public int directionTest = 1;

    [Space] public float presetAttackSpeedFactor = 1f;

    [Space] public float delayStartCycle = 1f;
    public float ultimateTime = 1f;


    public readonly ObservableFloat actionTime = new();
    public readonly ObservableFloat actionCooldown = new();
    public readonly ObservableFloat actionSpeedFactor = new();


    protected float attackBreakTime;

    private void ReCalculateAttackBreakTime(float unknown)
    {
        attackBreakTime = actionCooldown.Value / actionSpeedFactor.Value;
    }
}