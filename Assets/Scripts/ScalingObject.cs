using UnityEngine;
using PrimeTween;
using System.Diagnostics;

public enum Size {
    SMALL = 0,
    MEDIUM = 1,
    BIG = 2
}



public class ScalingObject : MonoBehaviour
{
    public Vector2 _previousScale;
    public Vector2 Scale;
    public LabelSO CurrentLabel;
    
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private SpriteRenderer _shadow;
    [SerializeField] private SpriteRenderer _outlineRenderer;
    
    [Header("Sprites")]
    [SerializeField] private Sprite _normal;
    [SerializeField] private Sprite _big;
    [SerializeField] private Sprite _small;

    [Header("Sprites of Outline")]
    [SerializeField] private Sprite _normalOutline;
    [SerializeField] private Sprite _bigOutline;
    [SerializeField] private Sprite _smallOutline;

    [Header("Outline")]
    [SerializeField] private GameObject _outline;
    
    [Header("Shape")]
    [SerializeField] private bool _isShaping;
    [SerializeField] private Transform _shapes;
    [SerializeField] private GameObject _normalShape;
    [SerializeField] private GameObject _circle;
    [SerializeField] private GameObject _box;
    [SerializeField] private GameObject _triangle;

    


    private void Awake() {
        Scale = transform.localScale;
    }

    private void OnMouseEnter() {

        if(LabelSystem.Instance.CurrentLabel == null) return;

        LabelSystem.Instance.CurrentScalingObject = this;
        if(_outline != null) _outline.SetActive(true);
        
    }

    private void OnMouseExit() {
        LabelSystem.Instance.CurrentScalingObject = null;
        if(_outline != null) _outline.SetActive(false);
    }
    public void ChangeScale(LabelSO labelSO) {
        _previousScale = Scale;

        if(_renderer != null) {
            Sequence.Create(useUnscaledTime: true)
                .Chain(Tween.Color(_renderer, Color.white, Color.black, 0.3f, Ease.OutExpo).OnComplete(() => {
                    CurrentLabel = labelSO;
                    if(_renderer == null) return;
                    
                    var sprite = labelSO.Size switch
                    {
                        Size.SMALL => _small,
                        Size.MEDIUM => _normal,
                        Size.BIG => _big,
                        _ => _normal
                    };
                    
                    _renderer.sprite = sprite;
                    if(_shadow != null) _shadow.sprite = sprite;
                    if(_outlineRenderer != null) {

                        var outline = labelSO.Size switch
                        {
                            Size.SMALL => _smallOutline,
                            Size.MEDIUM => _normalOutline,
                            Size.BIG => _bigOutline,
                            _ => _normalOutline
                        };
                        _outlineRenderer.sprite = outline;

                    }
                    
                }))
                .Chain(Tween.Scale(transform, transform.localScale, labelSO.NewScale, 0.5f, Ease.OutExpo))
                .OnComplete(() => {Tween.Color(_renderer, Color.black, Color.white, 0.3f, Ease.OutExpo);});
            
        } else {
            CurrentLabel = labelSO;
            Tween.Scale(transform, transform.localScale, labelSO.NewScale, 0.5f, Ease.OutExpo);
        }

        
        Scale = labelSO.NewScale;
    }

    public void ChangeShape(LabelSO labelSO) {
        Sequence.Create(useUnscaledTime: true)
                .Chain(Tween.Color(_renderer, Color.white, Color.black, 0.3f, Ease.OutExpo).OnComplete(() => {
                    CurrentLabel = labelSO;
                    switch(CurrentLabel.Shape) {
                        case ShapeType.Circle:
                            _circle.transform.parent = transform.parent;
                            _shapes.parent = _circle.transform;
                            transform.parent = _shapes;
                            _circle.SetActive(true);
                            break;
                        case ShapeType.Square:
                            _box.transform.parent = transform.parent;
                            _shapes.parent = _box.transform;
                            transform.parent = _shapes;
                            _box.SetActive(true);
                            break;
                        case ShapeType.Triangle:
                            _triangle.transform.parent = transform.parent;
                            _shapes.parent = _triangle.transform;
                            transform.parent = _shapes;
                            _triangle.SetActive(true);
                            break;
                        case ShapeType.Normal:
                            _normalShape.transform.parent = transform.parent;
                            _shapes.parent = _normalShape.transform;
                            transform.parent = _shapes;
                            _normalShape.SetActive(true);
                            break;
                    }

                    gameObject.SetActive(false);
                }))
                .Chain(Tween.Color(_renderer, Color.black, Color.white, 0.3f, Ease.OutExpo));
            
    }

    public bool IsShapeActive(ShapeType type) {
        return type switch
        {
            ShapeType.Circle => _circle != null,
            ShapeType.Square => _box != null,
            ShapeType.Triangle => _triangle != null,
            ShapeType.Normal => _normalShape != null,
            _ => true,
        };
    }

    public void ChangeScale(int sqrScale) {
        _previousScale = Scale;


        Scale = new Vector2(sqrScale, sqrScale);
    }

    public void Return() {
        Tween.Scale(transform, transform.localScale, _previousScale, 0.5f, Ease.OutExpo, useUnscaledTime: true);
        Scale = _previousScale;
    }
}
