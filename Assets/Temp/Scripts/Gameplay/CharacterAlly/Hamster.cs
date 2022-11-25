using UnityEngine;

public class Hamster : Ally
{
    protected override void StartAction()
    {
        base.StartAction();
        
        var bullet = Pool<Bullet>.Get("bullet_hamster");
        bullet.SetOwner(this).SetPosition(transform.position).Shoot();

    }

    public override void StartUltimate()
    {
        base.StartUltimate();
        Debug.Log("Hamster ultimate");
    }
}