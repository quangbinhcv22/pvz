using UnityEngine;

public static class RandomUtility
{
    public static bool IsHappen(float percent)
    {
        return Random.Range(0, 1f) < percent;
    }

    public static bool NotHappen(float percent)
    {
        return !IsHappen(percent);
    }
}