using System;
using System.Collections.Generic;
using System.Linq;
using Kryz.CharacterStats;
using UnityEngine;

public class TS : MonoBehaviour
{
    public CharacterStat speed = new(0);
    // public float atkDurationStandard = 1f;

    public List<Effect> effects;

    [Space] public float speedValue;

    public int dCount;
    // public float atkDuration;

    public List<Disabler> disablers = new();
    public List<Disabler> usingDisablers = new();


    private void Awake()
    {
        for (int i = 0; i < 2; i++)
        {
            disablers.Add(new Disabler(this));
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var d = disablers.Last();

            speed.AddDisabler(d);

            disablers.Remove(d);
            usingDisablers.Add(d);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            var d = usingDisablers.First();

            speed.RemoveDisabler(d);

            usingDisablers.Remove(d);
            disablers.Add(d);
        }

        speedValue = speed.Value;
        dCount = speed.Disablers.Count;

        // atkDuration = atkDurationStandard / (1 + atkSpeed.Value);
    }
}

[Serializable]
public class Effect
{
    public string name;
    public EffectID id;

    [Space] public bool isDisableStat;

    [Space] public StatType statType;
    public float value;
    public StatModType modifyType;

    [Space] public float duration;
    public float interval;


    public virtual void Execute(CharacterAB target)
    {
    }

    public virtual void UnExecute(CharacterAB target)
    {
    }
}


public enum StatType
{
    Unset = 0,

    HealthMax = 1,
    HealthCurrent = 2,

    Damage = 3,
    
    AttackDuration = 3,
    AttackCooldown = 4,
    AttackSpeed = 5,

    CriticalRate = 6,
    CriticalDamage = 7,
    GeneralSpeed = 8,
    MovementSpeed = 9,
}