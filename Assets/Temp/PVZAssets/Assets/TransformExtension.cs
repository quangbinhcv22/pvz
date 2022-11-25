using UnityEngine;

public static class TransformExtension
{
    public static Vector2 Position2D(this Transform transform)
    {
        var position = transform.position;
        return new Vector2(position.x, position.y);
    }
}