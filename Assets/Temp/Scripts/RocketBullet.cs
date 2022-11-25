using UnityEngine;

public class RocketBullet : Bullet
{
    public float velocity = 1f;
    public float acceleration = 1.01f;

    public float speedY = 3f;
    public float intensityY = 3f;

    private float xOrigin;
    private float deltaTime;


    [SerializeField] private Transform bulletUp;
    [SerializeField] private Transform bulletDown;


    public override void Shoot()
    {
        base.Shoot();
        
        xOrigin = owner.transform.position.x;
    }


    protected override void Moving()
    {
        var x = xOrigin + velocity * deltaTime + acceleration * Mathf.Pow(deltaTime, 2) / 2;
        var y = Mathf.Sin(deltaTime * speedY) * intensityY;


        {
            Vector3 diff = new Vector3(x, transform.position.y + y) - bulletUp.position;
            diff.Normalize();

            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            bulletUp.rotation = Quaternion.Euler(0f, 0f, rot_z);
        }

        {
            Vector3 diff = new Vector3(x, transform.position.y - y) - bulletDown.position;
            diff.Normalize();

            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            bulletDown.rotation = Quaternion.Euler(0f, 0f, rot_z);
        }

        transform.position = new Vector3(x, transform.position.y);

        bulletUp.localPosition = new Vector3(0, y);
        bulletDown.localPosition = new Vector3(0, -y);

        deltaTime += Time.deltaTime;
    }
}