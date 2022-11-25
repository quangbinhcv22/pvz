using System;
using UnityEngine;

public class CellIndexer : MonoBehaviour
{
    [SerializeField] private float xScaleBase = 1f;
    [SerializeField] private float yScaleBase = 1f;

    [Space, SerializeField] private SpriteRenderer horizontal;
    [SerializeField] private SpriteRenderer vertical;

    [SerializeField] private Vector2 offset;

    [Space] [SerializeField] private Color safeColor = Color.white;
    [SerializeField] private Color unSafeColor = Color.red;

    private Transform _horizontalTrans;
    private Transform _verticalTrans;


    private void Awake()
    {
        _horizontalTrans = horizontal.transform;
        _verticalTrans = vertical.transform;
    }

    public CellIndexer SetSize(Vector2Int size)
    {
        _horizontalTrans.localScale = new Vector3(size.x * xScaleBase, yScaleBase);
        _verticalTrans.localScale = new Vector3(xScaleBase, size.y * yScaleBase);

        return this;
    }

    public CellIndexer SetPosition(Vector2 position)
    {
        horizontal.transform.position = new Vector3(offset.x, position.y);
        vertical.transform.position = new Vector3(position.x, offset.y);
        
        return this;
    }

    public CellIndexer SetState(bool can)
    {
        gameObject.SetActive(can);
        
        // var color = can ? safeColor : unSafeColor;
        //
        // horizontal.color = color;
        // vertical.color = color;
        
        return this;
    }
}