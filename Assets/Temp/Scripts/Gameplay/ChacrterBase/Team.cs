using UnityEngine;

public enum Team
{
    Ally,
    Enemy,
}

public static class TeamExtension
{
    public static int ToLayer(this Team team)
    {
        return LayerMask.NameToLayer(team is Team.Ally ? Layer.Ally : Layer.Enemy);
    }
    
    public static int ToLayerDetector(this Team team)
    {
        return LayerMask.NameToLayer(team is Team.Ally ? Layer.EnemyDetector : Layer.AllyDetector);
    }
}