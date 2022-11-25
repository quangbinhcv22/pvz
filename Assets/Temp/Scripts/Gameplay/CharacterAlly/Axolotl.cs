using UnityEngine;

public class Axolotl : Ally
{
    protected override void StartAction()
    {
        base.StartAction();

        var bullet = Pool<Bullet>.Get("bullet_axolotl");
        bullet.SetOwner(this).Shoot();
    }

    public override void StartUltimate()
    {
        base.StartUltimate();
        Debug.Log("Axolotl ultimate");
    }
}