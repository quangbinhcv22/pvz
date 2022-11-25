using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SpeedTimeButton : MonoBehaviour
{
    [SerializeField] private Text text;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }


    private void OnEnable()
    {
        SetView((int)GameTime.speedTime);
    }

    private void OnClick()
    {
        GameTime.ChangeSpeed();
        SetView((int)GameTime.speedTime);
    }


    private void SetView(int speed)
    {
        text.text = $"x{speed}";
    }
}