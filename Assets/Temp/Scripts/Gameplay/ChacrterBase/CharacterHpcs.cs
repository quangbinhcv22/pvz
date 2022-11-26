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
    }

    protected virtual void OnLoadedHpBar(AsyncOperationHandle<GameObject> barObject)
    {
        _hpBar = barObject.Result.GetComponent<HpBar>();
        _hpBar.transform.localPosition = ViewConfig.HpAllyOffset;

        UpdateHpBar();
    }


    protected virtual void UpdateHpBar()
    {
        if (_hpBar) _hpBar.SetFill( Stats[StatType.HealthCurrent].RuntimeBaseValue, Stats[StatType.HealthMax].Value);
    }
}

public static class ViewConfig
{
    public static Vector2 HpAllyOffset = new Vector2(0, 0.75f);
}