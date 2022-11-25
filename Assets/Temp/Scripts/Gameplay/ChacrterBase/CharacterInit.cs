using UnityEngine;

public partial class Character
{
    [Space] public CharacterData _data;

    protected virtual void Init()
    {
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


    protected EnemyDetector enemyDetector;

    protected virtual void ListenDetector()
    {
        enemyDetector.onEnemyEnter += OnEnemyEnter;
        enemyDetector.onAllEnemyExit += OnAllEnemyEnter;
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

        if (enemyDetector)
        {
            var detectorLayer = teamTest.ToLayerDetector();
            enemyDetector.gameObject.layer = detectorLayer;
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