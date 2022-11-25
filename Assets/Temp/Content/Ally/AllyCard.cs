using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.EventSystems;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class AllyCard : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler,
    IPointerDownHandler
{
    private static AllyCard _selectCard;

    public static AllyCard SelectCard
    {
        get => _selectCard;
        set
        {
            _selectCard = value;
            OnSelectCard?.Invoke(_selectCard);
        }
    }

    private static Action<AllyCard> OnSelectCard;


    [SerializeField] private Image image;

    [Space] [SerializeField] private Text priceText;
    [SerializeField] private Image roleImage;

    [Space] [SerializeField] private Image cooldownFill;
    [SerializeField] private GameObject decorCooldown;
    [SerializeField] private GameObject selectedSignal;


    [Space] public CharacterCardData testData;


    private CharacterCardData _data;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetData(testData);
        }
    }


    public void SetData(CharacterCardData data)
    {
        _data = data;

        Addressables.LoadAssetAsync<Sprite>($"icon_{data.name}").Completed += OnImageLoaded;
        Addressables.LoadAssetAsync<Sprite>($"icon_role_{data.role}").Completed += OnRoleLoaded;

        priceText.text = data.price.ToString("N0");
    }


    private void OnEnable()
    {
        OnSelectCard += OnSelectedCard;
    }

    private void OnDisable()
    {
        OnSelectCard -= OnSelectedCard;
    }

    private void OnSelectedCard(AllyCard selectedCard)
    {
        selectedSignal.gameObject.SetActive(selectedCard == this);
    }


    private readonly OneEventTimeline cooldownThread = new();
    private bool _isCooldown;

    public void StartCooldown()
    {
        _isCooldown = true;

        cooldownThread.SetSeconds(_data.cooldown).SetCallback(OnCooldownComplete).Restart();
        decorCooldown.gameObject.SetActive(true);

        MonoLifeCycle.OnLateUpdate -= UpdateFillCooldown;
        MonoLifeCycle.OnLateUpdate += UpdateFillCooldown;
    }

    private void UpdateFillCooldown()
    {
        var fillAmount = 1 - cooldownThread.ElapsedSeconds / _data.cooldown;
        cooldownFill.fillAmount = fillAmount;
    }

    private void OnCooldownComplete()
    {
        _isCooldown = false;

        decorCooldown.gameObject.SetActive(false);
    }


    private void OnImageLoaded(AsyncOperationHandle<Sprite> operation)
    {
        if (operation.IsValid()) image.sprite = operation.Result;
    }

    private void OnRoleLoaded(AsyncOperationHandle<Sprite> operation)
    {
        if (operation.IsValid()) roleImage.sprite = operation.Result;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        if (_isCooldown) return;

        if (AllyCard.SelectCard == this)
        {
            SelectCard = null;
            // GameDragHandler.Cancel();
        }
        else
        {
            SelectCard = this;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isCooldown) return;
        if (AllyCard.SelectCard != this) return;

        GameDragHandler.StarDrag(DragTarget.Character, _data.name, false, OnSuccessCallback);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_isCooldown) return;
        if (AllyCard.SelectCard != this) return;

        GameDragHandler.StarDrag(DragTarget.Character, _data.name, true, OnSuccessCallback);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_isCooldown) return;
        if (AllyCard.SelectCard != this) return;

        foreach (var hovered in eventData.hovered)
        {
            if (hovered == gameObject)
            {
                OnPointerClick(eventData);
                return;
            }
        }

        GameDragHandler.RequestEndFromDrag();
    }

    public void OnDrag(PointerEventData eventData)
    {
    }


    private void OnSuccessCallback(bool success)
    {
        SelectCard = null;

        if (success)
        {
            StartCooldown();
        }
    }
}

[Serializable]
public class CharacterCardData
{
    public string name;
    public int price;
    public string role;
    public float cooldown;
}