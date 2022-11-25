using System;
using System.Collections.Generic;
using Character_Stats;
using Kryz.CharacterStats;
using UnityEngine;

[Serializable]
public class CharacterAB : MonoBehaviour
{
    private Dictionary<StatType, CharacterStat> stats = new();

    public void Awake()
    {
        stats.Add(StatType.HealthMax, new CharacterStat(1000f));

        stats.Add(StatType.HealthCurrent, new StatClamp(1000f)
        {
            MinNumber = 0f,
            MaxStat = stats[StatType.HealthMax],
        });

        stats[StatType.HealthCurrent].OnValueChanged += OnHealthChanged;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            // Debug.Log(stats[StatType.HealthCurrent].Value);
            stats[StatType.HealthCurrent].RuntimeBaseValue -= 300f;
        }
    }

    protected void OnHealthChanged()
    {
        var healthCurrent = stats[StatType.HealthCurrent].Value;

        Debug.Log($"Health: {healthCurrent}");

        if (healthCurrent <= 0)
        {
            Debug.Log("Die");
        }
    }
}

public class UniqueAction
{
    private Action action;

    public static UniqueAction operator +(UniqueAction source, Action callback)
    {
        source.action -= callback;
        source.action += callback;

        return source;
    }

    public static UniqueAction operator -(UniqueAction source, Action callback)
    {
        source.action -= callback;

        return source;
    }

    public void Invoke()
    {
        action?.Invoke();
    }
}