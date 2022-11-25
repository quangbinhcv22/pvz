using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

public static class BoardLogic
{
    public static Dictionary<Vector2Int, List<Ally>> allies = new();


    public static void Clear()
    {
        allies.Clear();
    }

    public static bool Place(Vector2Int index, string allyName)
    {
        if (!CanPlace(index, allyName)) return false;

        var placedAllies = allies[index];

        var ally = Pool<Character>.Get(allyName).GetComponent<Ally>();
        {
            ally.transform.position = BoardSetup.GetPosition(index);
            ally.Index = index;


            var config = Addressables.LoadAssetAsync<AllyConfig>($"config_{allyName}").WaitForCompletion();
            ally.SetConfig(Team.Ally, config.GetData());

            ally.SetDirection(1);


            ally.StartCycle();
        }


        placedAllies.Add(ally);


        GameAudio.PlayRandom(AudioDic.Placed);

        return true;
    }

    public static bool CanPlace(Vector2Int index, string allyName)
    {
        if (!BoardSetup.CursorInRegion(index)) return false;

        if (!allies.ContainsKey(index)) allies.Add(index, new List<Ally>());

        var placedAllies = allies[index];

        return !placedAllies.Any();
    }

    public static void OnDie(Vector2Int index, Ally ally)
    {
        allies[index].Remove(ally);
    }

    // public static bool Sell()


    public static bool PowerFor(Vector2Int index)
    {
        if (!BoardSetup.CursorInRegion(index)) return false;

        if (!allies.ContainsKey(index)) allies.Add(index, new List<Ally>());
        var placedAllies = allies[index];


        bool usePower = false;

        foreach (var ally in placedAllies)
        {
            ally.StartUltimate();
            usePower = true;
        }

        return usePower;
    }
}