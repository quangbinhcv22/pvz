using UnityEngine;

public class Threepeater : Ally
{
    protected override async void StartAction()
    {
        base.StartAction(); 

        for (int i = -1; i <= 1; i++)
        {
            var offset = BoardSetup.SizeUnit.y;

            var bullet = Pool<Bullet>.Get("bullet_turkey");
            bullet.SetOwner(this).SetPosition(transform.position + new Vector3(default, offset * i)).Shoot();
        }
    }

    public override void StartUltimate()
    {
        base.StartUltimate();
        Debug.Log("Turkey ultimate");
    }
}