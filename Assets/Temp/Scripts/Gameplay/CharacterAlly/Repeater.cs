using System;
using UnityEngine;
using Task = System.Threading.Tasks.Task;

public class Repeater : Ally
{
    [Space] public float delayNextBullet = 0.25f;
    public int bulletNumber = 2;

    protected override async void StartAction()
    {
        base.StartAction();

        for (int i = 1; i <= bulletNumber; i++)
        {
            var bullet = Pool<Bullet>.Get("bullet_turkey");
            bullet.SetOwner(this).Shoot();

            if (i < bulletNumber) await Task.Delay(TimeSpan.FromSeconds(delayNextBullet));
        }
    }

    public override void StartUltimate()
    {
        base.StartUltimate();
        Debug.Log("Turkey ultimate");
    }
}