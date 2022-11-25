using UnityEngine;

public class LawnMower : MonoBehaviour
{
    public int damage = 9999;
    public float moveTime = 3f;
    public int direction = 1;

    [Space] [SerializeField] private EnemyDetector enemyDetector;

    private void OnEnable()
    {
        IsRunning = false;
        enemyDetector.onEnemyEnter = OnEnemyEnter;
    }


    private bool _isRunning;

    private bool IsRunning
    {
        get => _isRunning;
        set
        {
            _isRunning = value;

            if (_isRunning)
            {
                MonoLifeCycle.OnUpdate -= Move;
                MonoLifeCycle.OnUpdate += Move;
            }
            else
            {
                MonoLifeCycle.OnUpdate -= Move;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (IsRunning) return;

        IsRunning = true;
    }

    private void Move()
    {
        transform.position += new Vector3(direction * moveTime * Time.deltaTime, default);
    }


    private void OnEnemyEnter(Character enemy)
    {
        enemy.TakeDamage(damage);
    }
}