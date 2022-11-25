public class Cherry : AllyInstantAction
{
    protected override void ActionForEnemy(Character enemy)
    {
        enemy.TakeDamage(_data.attackDamage);
    }
}