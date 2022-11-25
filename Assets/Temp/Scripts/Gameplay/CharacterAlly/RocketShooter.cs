using UnityEngine;

public class RocketShooter : Ally
{
    protected override void StartAction()
    {
        base.StartAction();

        var bullet = Pool<Bullet>.Get("bullet_rocket");
        bullet.SetOwner(this).SetPosition(transform.position).Shoot();
    }

    public override void StartUltimate()
    {
        base.StartUltimate();
        Debug.Log("RocketShooter ultimate");
    }
}