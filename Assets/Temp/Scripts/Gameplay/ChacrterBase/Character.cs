using UnityEngine;
using UnityEngine.AddressableAssets;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public partial class Character : MonoBehaviour
{
    [Space] public Transform model;

    protected readonly OneEventTimeline timeline = new();


    private void Start()
    {
        Init();


        if (autoStart)
        {
            var config = Addressables.LoadAssetAsync<AllyConfig>($"config_{name}").WaitForCompletion();
            SetConfig(teamTest, config.GetData());

            SetDirection(directionTest);

            
            StartCycle();
        }
    }

    protected virtual void OnDisable()
    {
        timeline.Stop();
    }


    public virtual void TakeDamage(int damage, DamageSourceType source = DamageSourceType.Unset)
    {
        HpCurrent.Value -= damage;
    }

    public virtual void AddEffect(Effect effect)
    {
        Debug.Log($"<color=yellow>{name}</color> take <color=yellow>{effect.id}</color> for <color=yellow>{effect.duration}</color>");
    }

    public virtual void RemoveEffect(Effect effect)
    {
    }


    public virtual void AddForce(Vector2 force)
    {
        Debug.Log($"<color=yellow>{name}</color> add <color=yellow>{force}</color> force");
    }


    protected virtual void ReturnPool()
    {
        timeline.Stop();

        Pool<Character>.ReturnPool(this);
    }


    public virtual void SetConfig(Team team, CharacterData data)
    {
        _data = data;

        HpMax.Value = _data.health;
        HpCurrent.Value = _data.health;

        actionTime.Value = data.actionDuration;
        actionCooldown.Value = data.actionCooldown;
        actionSpeedFactor.Value = 1f;


        if (data.UseRange)
        {
            enemyDetector ??= Instantiate(Addressables.LoadAssetAsync<GameObject>("enemy_detector").WaitForCompletion())
                .GetComponent<EnemyDetector>();

            enemyDetector.transform.SetParent(transform);
            enemyDetector.SetSize(new Vector2(data.actionRange, 1));

            ListenDetector();
        }


        SetLayers();
    }

    public void SetDirection(int direction)
    {
        var yRotate = Mathf.Sign(direction) > 0 ? 0 : 180;

        model.eulerAngles = new Vector3(default, yRotate);
        enemyDetector.SetDirection(direction);
    }
}