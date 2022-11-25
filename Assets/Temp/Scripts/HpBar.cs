using UnityEngine;

public class HpBar : MonoBehaviour
{
    [SerializeField] private GameObject container;
    [SerializeField] private Transform fillTransform;

    public void SetFill(float current, float max)
    {
        var percent = current / max;
        var validValue = percent is > 0 and < 1;

        container.gameObject.SetActive(validValue);

        fillTransform.localPosition = new Vector3(percent - 1f, default);
    }
}