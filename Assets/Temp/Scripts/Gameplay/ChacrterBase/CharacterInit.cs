using UnityEngine;

public partial class Character
{
    [Space] public CharacterData config;

    protected virtual void Init()
    {
        SelfCollider = GetComponent<Collider2D>();
        
        LoadHpBar();

        CalculateAnimFactor();

        ListenStatChange();
    }

    protected virtual void ListenStatChange()
    {
        actionTime.onValueChanged += ReCalculateAttackBreakTime;
        actionTime.onValueChanged += ReCalculateAttackAnimSpeed;

        actionSpeedFactor.onValueChanged += ReCalculateAttackBreakTime;
        actionSpeedFactor.onValueChanged += ReCalculateAttackAnimSpeed;

        actionCooldown.onValueChanged += ReCalculateAttackBreakTime;
    }


    protected Collider2D SelfCollider;
    protected EnemyDetector EnemyDetector;

    protected virtual void ListenDetector()
    {
        EnemyDetector.onEnemyEnter += OnEnemyEnter;
        EnemyDetector.onAllEnemyExit += OnAllEnemyEnter;
    }


    public Team Team
    {
        get => teamTest;
        set
        {
            teamTest = value;
            SetLayers();
        }
    }

    protected virtual void SetLayers()
    {
        gameObject.layer = teamTest.ToLayer();

        if (EnemyDetector)
        {
            var detectorLayer = teamTest.ToLayerDetector();
            EnemyDetector.gameObject.layer = detectorLayer;
        }
    }


    protected virtual void OnEnemyEnter(Character enemy)
    {
        NextStateCallback = LoopStateAction;
    }

    protected virtual void OnAllEnemyEnter()
    {
        // NextStateCallback = null;
    }
}