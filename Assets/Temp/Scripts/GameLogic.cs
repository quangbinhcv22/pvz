using System;

public static class GameLogic
{
    private static int _food;
    private static int _sushi;

    public static int Food
    {
        get => _food;
        set
        {
            _food = value;
            OnFoodChanged?.Invoke();
        }
    }

    public static int Sushi
    {
        get => _sushi;
        set
        {
            _sushi = value;
            OnSushiChanged?.Invoke();
        }
    }

    public static Action OnFoodChanged;
    public static Action OnSushiChanged;


    public static void StartGame()
    {
        BoardLogic.Clear();

        Food = 10;
        Sushi = 1;
    }
}