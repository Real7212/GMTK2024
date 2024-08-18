using UnityEngine;
using UnityEngine.EventSystems;

public class Label : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private RectTransform _rect;
    [SerializeField] private Canvas _canvas;
    private bool _isDragging;
    public LabelSO LabelSettings;
    
    private Vector2 _startPos;

    private void Awake() {
        _startPos = _rect.anchoredPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        LabelSystem.Instance.CurrentLabel = this;
        _rect.position = Input.mousePosition;
        LabelsFolder.Instance.Open();
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rect.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData) {
        _rect.anchoredPosition = _startPos;
    }
}
