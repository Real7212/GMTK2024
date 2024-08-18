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
    public Size CurrentSize;
    
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


    


    private void Awake() {
        Scale = transform.localScale;
    }

    private void OnMouseEnter() {

        if(LabelSystem.Instance.CurrentLabel == null) return;

        LabelSystem.Instance.CurrentScalingObject = this;
        _outline.SetActive(true);
    }

    private void OnMouseExit() {
        LabelSystem.Instance.CurrentScalingObject = null;
        _outline.SetActive(false);
    }
    public void ChangeScale(LabelSO labelSO) {
        _previousScale = Scale;

        Sequence.Create(useUnscaledTime: true)
            .Chain(Tween.Color(_renderer, Color.white, Color.black, 0.3f, Ease.OutExpo).OnComplete(() => {
                var sprite = labelSO.Size switch
                {
                    Size.SMALL => _small,
                    Size.MEDIUM => _normal,
                    Size.BIG => _big,
                    _ => _normal
                };
                CurrentSize = labelSO.Size;
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
        
        
        Scale = labelSO.NewScale;
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
