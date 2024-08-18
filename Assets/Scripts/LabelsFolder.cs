using PrimeTween;
using UnityEngine;
using UnityEngine.EventSystems;

public class LabelsFolder : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public static LabelsFolder Instance { get; private set; }

    [SerializeField] private RectTransform _rect;
    [SerializeField] private Vector2 _idlePosition;
    [SerializeField] private Vector2 _pointerPosition;
    [SerializeField] private Vector2 _endPosition;


    private bool _isOpened;

    
    private void Awake() {
        if(Instance == null) Instance = this;
        else Destroy(Instance);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(_isOpened) return;
        Hover(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(_isOpened) return;
        Hover(false);
    }

    public void Hover(bool isHover) {
        
        Tween.UIAnchoredPosition(_rect, !isHover ? _pointerPosition : _idlePosition, !isHover ? _idlePosition : _pointerPosition, 0.5f, Ease.OutExpo, useUnscaledTime: true);
    }

    public void Open() {
        _isOpened = !_isOpened;
        Tween.UIAnchoredPosition(_rect, !_isOpened ? _endPosition : _idlePosition, !_isOpened ? _idlePosition : _endPosition, 0.5f, Ease.OutExpo, useUnscaledTime: true);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Open();   
    }
}
