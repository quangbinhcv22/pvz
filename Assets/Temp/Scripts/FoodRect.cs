using UnityEngine;

public class FoodRect : MonoBehaviour
{
    private void OnEnable()
    {
        GameUI.foodRect = GetComponent<RectTransform>();
    }
}