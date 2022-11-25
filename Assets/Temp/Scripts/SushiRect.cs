using UnityEngine;

public class SushiRect : MonoBehaviour
{
    private void OnEnable()
    {
        GameUI.sushiRect = GetComponent<RectTransform>();
    }
}