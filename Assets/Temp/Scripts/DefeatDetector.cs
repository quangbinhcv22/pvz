using UnityEngine;

public class DefeatDetector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        DefeatPresenter.Defeat();
    }
}