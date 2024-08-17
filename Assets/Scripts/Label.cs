using UnityEngine;
using UnityEngine.EventSystems;

public class Label : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] private RectTransform _rect;
    [SerializeField] private Canvas _canvas;
    private bool _isDragging;
    public LabelSO LabelSettings;
    
    private Vector2 _startPos;

    private void Awake() {
        _startPos = _rect.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        LabelSystem.Instance.CurrentLabel = this;
        _rect.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData) {
        _rect.anchoredPosition = _startPos;
    }
}
