using UnityEngine;

public class Mantis : Ally
{
    private int attackCount = 0;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartAction();
        }
        
        if (Input.GetKeyDown(KeyCode.D))
        {
            StartDie();
        }
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            animator.Stop();
        }
        
        if (Input.GetKeyDown(KeyCode.C))
        {
            animator.Continue();
        }
    }


    protected override void StartAction()
    {
        attackCount = attackCount++ % 3 + 1;

        var state = $"attack{attackCount}";

        animator.PlayOnce(state);


        var attackDuration = attackCount is 3 ? 1.6667f : 1f;   
        
        timeline.SetSeconds(attackDuration).SetCallback(OnActionDone).Restart();
    }

    protected override void OnActionDone()
    {
        timeline.SetSeconds(0.15f).SetCallback(NextState).Restart();

        foreach (var enemy in enemyDetector.enemies)
        {
            enemy.TakeDamage(_data.attackDamage, DamageSourceType.Normal);
        }
    }
}