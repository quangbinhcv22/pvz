using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class EnemyDetector : MonoBehaviour
{
    private new BoxCollider2D collider;
    private BoxCollider2D Collider => collider ??= GetComponent<BoxCollider2D>();
    
    private void OnDisable()
    {
        enemies.Clear();
    }


    public EnemyDetector SetSize(Vector2 size)
    {
        Collider.size = new Vector2(size.x * BoardSetup.SizeUnit.x, size.y * BoardSetup.SizeUnit.y);
        return this;
    }

    public EnemyDetector SetDirection(int direction)
    {
        // transform.localPosition = new Vector3(position.x + Collider.size.x / 2f, position.y);
        transform.localPosition = new Vector3(Collider.size.x / 2f, default) * direction;
        return this;
    }


    public Action<Character> onEnemyEnter;
    public Action<Character> onEnemyExit;

    public Action onAllEnemyExit;


    public List<Character> enemies = new();


    private void OnTriggerEnter2D(Collider2D col)
    {
        var target = col.GetComponent<Character>();

        if (target)
        {
            enemies.Add(target);
            onEnemyEnter?.Invoke(target);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        var target = col.GetComponent<Character>();

        if (target)
        {
            enemies.Remove(target);
            onEnemyExit?.Invoke(target);

            if (!enemies.Any())
            {
                onAllEnemyExit?.Invoke();
            }
        }
    }


    private void OnValidate()
    {
        var collider = GetComponent<BoxCollider2D>();

        if (collider)
        {
            collider.isTrigger = true;
        }


        var rigidbody = GetComponent<Rigidbody2D>();

        if (rigidbody)
        {
            rigidbody.bodyType = RigidbodyType2D.Kinematic;
            rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}