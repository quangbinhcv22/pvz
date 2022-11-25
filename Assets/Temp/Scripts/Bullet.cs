using UnityEngine;

public class Bullet : MonoBehaviour
{
    protected Transform cachedTransform;

    protected EnemyDetector enemyDetector;
    protected Character owner;

    protected virtual void Awake()
    {
        cachedTransform = transform;

        enemyDetector = GetComponent<EnemyDetector>();
        if (enemyDetector) enemyDetector.onEnemyEnter += OnEnemyEnter;
    }

    protected virtual void OnEnemyEnter(Character enemy)
    {
    }

    public virtual Bullet SetOwner(Character owner)
    {
        this.owner = owner;
        gameObject.layer = owner.Team.ToLayerDetector();

        OnSetOwner();

        return this;
    }

    public virtual Bullet SetPosition(Vector2 position)
    {
        transform.position = position;
        return this;
    }

    protected virtual void OnSetOwner()
    {
    }


    public virtual void Shoot()
    {
        MonoLifeCycle.OnUpdate -= Moving;
        MonoLifeCycle.OnUpdate += Moving;
    }

    protected virtual void OnDisable()
    {
        MonoLifeCycle.OnUpdate -= Moving;
    }


    protected virtual void Moving()
    {
    }


    protected virtual void ReturnPool()
    {
        Pool<Bullet>.ReturnPool(this);
    }
}