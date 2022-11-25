using UnityEngine;

public class Turkey : Ally
{
    protected override void StartAction()
    {
        base.StartAction();
        
        var bullet = Pool<Bullet>.Get("bullet_turkey");
        bullet.SetOwner(this).Shoot();

    }

    public override void StartUltimate()
    {
        base.StartUltimate();
        Debug.Log("Turkey ultimate");
    }
}
