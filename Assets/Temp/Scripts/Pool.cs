using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

public static class Pool<T> where T : Component
{
    static Pool()
    {
        container = new GameObject("pool").transform;
        Object.DontDestroyOnLoad(container);
    }

    public static readonly Transform container;


    private static readonly Dictionary<string, PoolRegion<T>> pool = new();

    public static T Get(string key)
    {
        if (pool.ContainsKey(key))
        {
            return pool[key].Get();
        }
        else
        {
            var newPool = new PoolRegion<T>(key);
            pool.Add(key, newPool);

            return newPool.Get();
        }
    }

    public static void ReturnPool(T bullet)
    {
        var key = bullet.name;

        if (pool.ContainsKey(key))
        {
            pool[key].ReturnPool(bullet);
        }
    }
}

[Serializable]
public class PoolRegion<T> where T : Component
{
    private List<T> unUseBullets = new();

    public PoolRegion(string key)
    {
        _key = key;
    }


    private string _key;

    internal T Get()
    {
        if (unUseBullets.Any())
        {
            var firstBullet = unUseBullets.First();
            unUseBullets.Remove(firstBullet);

            firstBullet.gameObject.SetActive(true);


            return firstBullet;
        }

        var newBullet = Addressables.InstantiateAsync(_key, Pool<T>.container).WaitForCompletion().GetComponent<T>();
        newBullet.name = _key;

        return newBullet;
    }

    internal void ReturnPool(T bullet)
    {
        bullet.gameObject.SetActive(false);
        unUseBullets.Add(bullet);
    }
}