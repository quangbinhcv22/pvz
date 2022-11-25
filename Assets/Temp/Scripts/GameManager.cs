using UnityEngine;

public class GameManager
{
    [RuntimeInitializeOnLoadMethod]
    private static void Setup()
    {
        Application.targetFrameRate = 60;
    }
}