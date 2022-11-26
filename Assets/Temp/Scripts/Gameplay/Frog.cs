using UnityEngine;

public class Frog : Ally
{
    private FrogData _frogData;

    public override void SetConfig(Team team, CharacterData data)
    {
        base.SetConfig(team, data);

        _frogData = (FrogData)data;
    }


    private Character closestEnemy;

    protected override void StartAction()
    {
        base.StartAction();

        closestEnemy = null;
        var minDistance = float.MaxValue;

        foreach (var enemy in EnemyDetector.enemies)
        {
            var distance = Vector2.Distance(transform.position, enemy.transform.position);

            if (distance > minDistance) continue;

            closestEnemy = enemy;
            minDistance = distance;
        }
    }

    protected override void OnActionDone()
    {
        if (closestEnemy)
        {
            if (closestEnemy.Stats[StatType.HealthCurrent].RuntimeBaseValue <= _frogData.killDamage)
            {
                Destroy(closestEnemy.gameObject);

                Debug.Log("Nhai...");


                animator.PlayOnce(StateName.Ultimate);

                timeline.SetSeconds(_frogData.killCooldown).SetCallback(NextState).Restart();
            }
            else
            {
                Debug.Log("Attack...");

                closestEnemy.TakeDamage(_data.attackDamage);

                base.OnActionDone();
            }
        }
    }
}