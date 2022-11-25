using UnityEngine;

public class Pepper : AllyInstantAction
{
    [Space] public int damage = 1000;

    protected override void ActionForEnemy(Character enemy)
    {
        enemy.TakeDamage(damage);
    }
}