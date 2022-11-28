using System.Collections.Generic;
using UnityEngine;

public class BulletThrough : Bullet
{
    [SerializeField] private List<string> shootClips;
    [SerializeField] private List<string> impactClips;

    public float speed = 1f;


    private float startX;
    private float endX;


    protected override void OnSetOwner()
    {
        cachedTransform.position = owner.transform.position;

        startX = cachedTransform.position.x;
        endX = startX + owner.config.actionRange * BoardSetup.SizeUnit.x;
    }


    protected override void Moving()
    {
        cachedTransform.position += new Vector3(speed * Time.deltaTime, default);

        if (cachedTransform.position.x >= endX)
        {
            ReturnPool();
        }
    }

    public override void Shoot()
    {
        base.Shoot();
        
        GameAudio.PlayRandom(shootClips);
    }


    protected override void OnEnemyEnter(Character enemy)
    {
        enemy.TakeDamage(owner.config.attackDamage);
        
        GameAudio.PlayRandom(impactClips);
    }
}