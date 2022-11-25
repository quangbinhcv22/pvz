using UnityEngine;
using UnityEngine.UI;

public class DefeatPresenter : MonoBehaviour
{
    [SerializeField] private Image cover;

    public static void Defeat()
    {
        Debug.Log("Defeat!!");
    }
}