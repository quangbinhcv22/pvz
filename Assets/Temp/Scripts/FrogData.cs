using System;
using UnityEngine;

[Serializable]
public class FrogData : CharacterData
{
    [Space] public int killDamage;
    public float killCooldown;
}