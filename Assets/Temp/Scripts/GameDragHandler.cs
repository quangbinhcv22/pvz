using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameDragHandler : MonoBehaviour
{
    private static GameDragHandler instance;


    [SerializeField] private CellIndexer indexer;


    private GameObject _target;


    private static DragTarget _dragTarget;
    private static object _data;

    private static Action<bool> EndCallback;

    private static bool _isDragging;

    private void OnEnable()
    {
        instance = this;
    }


    private static void OnUpdate()
    {
        Drag();

        if (!_isDragging && Input.GetMouseButtonUp(0))
        {
            EndDrag();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            BoardLogic.PowerFor(BoardSetup.CursorIndex);
        }
    }


    public static void StarDrag(DragTarget dragTarget, object data, bool isDrag, Action<bool> successCallback)
    {
        EndCallback = successCallback;

        _isDragging = isDrag;

        _dragTarget = dragTarget;
        _data = data;

        MonoLifeCycle.StartCoroutine(ListenEnd());
    }

    private static IEnumerator ListenEnd()
    {
        yield return new WaitForEndOfFrame();

        MonoLifeCycle.OnUpdate -= OnUpdate;
        MonoLifeCycle.OnUpdate += OnUpdate;
    }


    public static void Drag()
    {
        BoardSetup.UpdateCursorIndex();

        var inBoard = BoardSetup.CursorInRegion(BoardSetup.CursorIndex);

        instance.indexer.gameObject.SetActive(inBoard);

        if (inBoard)
        {
            var canPlace = BoardLogic.CanPlace(BoardSetup.CursorIndex, (string)_data);
            instance.indexer.SetState(canPlace).SetSize(BoardSetup.Size)
                .SetPosition(BoardSetup.GetPosition(BoardSetup.CursorIndex));
        }
    }


    public static void EndDrag()
    {
        Drag();

        switch (_dragTarget)
        {
            case DragTarget.Character:
                var success = BoardLogic.Place(BoardSetup.CursorIndex, (string)_data);
                EndCallback?.Invoke(success);
                break;
        }

        instance.indexer.gameObject.SetActive(false);

        MonoLifeCycle.OnUpdate -= OnUpdate;
    }

    public static void Cancel()
    {
        EndCallback?.Invoke(false);
        MonoLifeCycle.OnUpdate -= OnUpdate;
    }


    public static void RequestEndFromDrag()
    {
        if (_isDragging) EndDrag();
    }
}


public enum DragTarget
{
    Unset = 0,
    Character = 1,
    Sushi = 2,
    Remove = 3,
}