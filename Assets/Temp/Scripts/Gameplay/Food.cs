using System;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Food : MonoBehaviour
{
    private const float warningTime = 3f;
    private const float destroyTime = 2f;


    public int value;
    public FoodType Type;


    private readonly OneEventTimeline fadeThread = new();


    private BoxCollider2D _collider2D;

    private void Awake()
    {
        _collider2D = GetComponent<BoxCollider2D>();
    }

    private void OnEnable()
    {
        _collider2D.enabled = true;
        fadeThread.SetSeconds(warningTime).SetCallback(WarningDestroy).Restart();
    }

    private void WarningDestroy()
    {
        fadeThread.SetSeconds(destroyTime).SetCallback(ReturnPool).Restart();
    }

    private void ReturnPool()
    {
        Pool<Food>.ReturnPool(this);
    }

    private void PresentClaim()
    {
        var a = Camera.main.ScreenToWorldPoint(GameUI.foodRect.position);
        transform.DOMove(new Vector3(a.x, a.y), 1f).onComplete = ReturnPool;
    }


    private void OnMouseEnter()
    {
        _collider2D.enabled = false;

        switch (Type)
        {
            case FoodType.Food:
                GameLogic.Food += value;
                break;
            case FoodType.Sushi:
                GameLogic.Sushi += value;
                break;
        }

        PresentClaim();
    }
}

public enum FoodType
{
    Food = 0,
    Sushi = 1,
}