using System;

[Serializable]
public class Resource
{
    public int type;
    public int id;
    public int number;
}

public enum ResourceType
{
    Currency = 0,
    Character = 1,
    Skin = 2,
    CharacterPackage = 3,
}

public enum CurrencyType
{
    Gold = 0,
    Diamond = 1,
}