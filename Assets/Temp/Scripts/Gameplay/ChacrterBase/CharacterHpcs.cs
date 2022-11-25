using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public partial class Character
{
    protected HpBar _hpBar;

    protected virtual void LoadHpBar()
    {
        var key = $"hp_bar_{teamTest.ToString().ToLower()}";
        Addressables.InstantiateAsync(key, transform).Completed += OnLoadedHpBar;

        HpCurrent.onValueChanged += OnHpCurrentChanged;
        HpMax.onValueChanged += OnHpMaxChanged;
    }

    protected virtual void OnLoadedHpBar(AsyncOperationHandle<GameObject> barObject)
    {
        _hpBar = barObject.Result.GetComponent<HpBar>();
        _hpBar.transform.localPosition = ViewConfig.HpAllyOffset;

        UpdateHpBar();
    }


    protected virtual void OnHpCurrentChanged(float newHp)
    {
        if (HpCurrent.Value <= 0) StartDie();
        UpdateHpBar();
    }

    protected virtual void OnHpMaxChanged(float newHpMax)
    {
        HpCurrent.SetMax(newHpMax);
        UpdateHpBar();
    }




    protected virtual void UpdateHpBar(float unknown = default)
    {
        if (_hpBar) _hpBar.SetFill(HpCurrent.Value, HpMax.Value);
    }
}

public static class ViewConfig
{
    public static Vector2 HpAllyOffset = new Vector2(0, 0.75f);
}