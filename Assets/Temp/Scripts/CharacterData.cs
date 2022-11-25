using System;
using UnityEngine;

[Serializable]
public class CharacterData
{
    [Space] public int cost;
    public float cooldown;

    [Space] public int health;
    public float delayStartCycle = 1f;
    public bool powerable = true;

    [Space] public float actionRange;
    public float actionDuration;
    public float actionCooldown;

    [Space] public int attackDamage;


    public bool UseRange => actionRange > 0;
}