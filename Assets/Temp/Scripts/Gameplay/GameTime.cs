using UnityEngine;

public static class GameTime
{
    public enum SpeedTime
    {
        Pause = 0,
        Normal = 1,
        Fast = 2,
    }

    public static SpeedTime speedTime = SpeedTime.Normal;
    public static SpeedTime preSpeedTime = SpeedTime.Normal;


    public static void ChangeSpeed()
    {
        preSpeedTime = speedTime;
        speedTime = speedTime is SpeedTime.Fast ? SpeedTime.Normal : SpeedTime.Fast;

        Refresh();
    }

    public static void Pause()
    {
        speedTime = SpeedTime.Pause;

        Refresh();
    }

    public static void PlayPre()
    {
        speedTime = preSpeedTime;

        Refresh();
    }


    private static void Refresh()
    {
        Time.timeScale = (int)speedTime;
    }
}