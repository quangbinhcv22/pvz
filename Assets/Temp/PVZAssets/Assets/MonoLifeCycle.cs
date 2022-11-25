using System;
using System.Collections;
using UnityEngine;

public class MonoLifeCycle : MonoBehaviour
{
    public static Action OnUpdate;
    public static Action OnLateUpdate;


    private static readonly MonoBehaviour instance;

    static MonoLifeCycle()
    {
        instance = new GameObject(nameof(MonoLifeCycle)).AddComponent<MonoLifeCycle>();
        DontDestroyOnLoad(instance.gameObject);
    }

    public new static void StartCoroutine(IEnumerator enumerator)
    {
        instance.StartCoroutine(enumerator);
    }


    private void Update()
    {
        OnUpdate?.Invoke();
    }

    private void LateUpdate()
    {
        OnLateUpdate?.Invoke();
    }
}