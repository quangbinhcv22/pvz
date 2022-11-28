using UnityEngine;

public partial class Character
{
    [Space] public CharacterData config;

    protected virtual void Init()
    {
        LoadHpBar();

        CalculateAnimFactor();
    }


    private Collider2D selfCollider;
    protected Collider2D SelfCollider => selfCollider ??= GetComponent<Collider2D>();

    
    protected EnemyDetector EnemyDetector;
    

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