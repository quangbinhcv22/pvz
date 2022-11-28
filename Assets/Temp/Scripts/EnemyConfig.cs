using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Common", fileName = "EnemyConfig", order = 0)]
public class EnemyConfig : CharacterConfig
{
    [SerializeField] private EnemyData data;
    
    public override CharacterData GetData() => data;
}


[Serializable]
public class EnemyData : CharacterData
{
    [Space] public float moveSpeed = 0.2f;
}