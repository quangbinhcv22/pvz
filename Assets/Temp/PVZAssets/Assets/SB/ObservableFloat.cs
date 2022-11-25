using System;
using UnityEngine;

[Serializable]
public sealed class ObservableFloat
{
    private float field;

    public float Value
    {
        get => field;
        set
        {
            if (field == value) return;

            field = value;

            onValueChanged?.Invoke(field);
        }
    }

    public Action<float> onValueChanged;

    public ObservableFloat(float value = 0f)
    {
        Value = value;
    }
}

[Serializable]
public sealed class ObservableLimitFloat
{
    private float field;

    public float Value
    {
        get => field;
        set
        {
            field = Mathf.Min(value, _max);
            onValueChanged?.Invoke(field);
        }
    }


    private float _max;
    
    public void SetMax(float max)
    {
        _max = max;
    }

    public Action<float> onValueChanged;

    public ObservableLimitFloat(float value = 0f)
    {
        Value = value;
    }
}