using UnityEngine;

[DefaultExecutionOrder(-999)]
public class BoardSetup : MonoBehaviour
{
    public static Vector2 SizeUnit;

    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private Vector2Int size = new(10, 5);

    private static Vector3 startPosition;
    private static Vector3 endPosition;
    public static Vector2Int Size;
    private static Camera _camera;


    private void Awake()
    {
        Size = size;

        startPosition = startPoint.position;
        endPosition = endPoint.position;

        var offset = endPosition - startPosition;
        SizeUnit = new Vector2(offset.x / size.x, offset.y / size.y);


        _camera = Camera.main;
    }


    public static Vector2Int CursorIndex;
    public static Vector2 CursorIndexPosition => GetPosition(CursorIndex);

    public static void UpdateCursorIndex()
    {
        var cursorPoint = _camera.ScreenToWorldPoint(Input.mousePosition);
        CursorIndex = GetCellIndex(cursorPoint);
    }

    public static bool CursorInRegion(Vector2Int index)
    {
        return !(index.x < 0 || index.x >= Size.x || index.y < 0 || index.y >= Size.y);
    }

    public static Vector2Int GetCellIndex(Vector3 position)
    {
        if (position.x < startPosition.x || position.x > endPosition.x || position.y < startPosition.x ||
            position.y > endPosition.y)
            return new(-1, -1);

        var xSizePer = Mathf.Abs(endPosition.x - startPosition.x) / Size.x;
        var ySizePer = Mathf.Abs(endPosition.y - startPosition.y) / Size.y;

        var index = new Vector2Int((int)((position.x - startPosition.x) / xSizePer), (int)((position.y - startPosition.y) / ySizePer));

        return index;
    }

    public static Vector2 GetPosition(Vector2 index)
    {
        var xSizePer = Mathf.Abs(endPosition.x - startPosition.x) / Size.x;
        var ySizePer = Mathf.Abs(endPosition.y - startPosition.y) / Size.y;


        index.x = (index.x + 0.5f) * xSizePer + startPosition.x;
        index.y = (index.y + 0.5f) * ySizePer + startPosition.y;

        return index;
    }
}