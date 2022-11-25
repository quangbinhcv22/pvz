using UnityEngine;

public class BulletNormal : Bullet
{
    public float speed = 1f;
    
    protected override void OnSetOwner()
    {
        cachedTransform.position = owner.transform.position;
    }


    protected override void Moving()
    {
        cachedTransform.position += new Vector3(speed * Time.deltaTime, default);
    }

    protected override void OnEnemyEnter(Character enemy)
    {
        enemy.TakeDamage(owner._data.attackDamage);


        GameAudio.Play("splat");


        ReturnPool();
    }
}