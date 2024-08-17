using UnityEngine;
using PrimeTween;

public class ScalingObject : MonoBehaviour
{
    public Vector2 Scale;

    private void Awake() {
        Scale = transform.localScale;
    }

    private void OnMouseEnter() {
        LabelSystem.Instance.CurrentScalingObject = this;
    }

    private void OnMouseExit() {
        LabelSystem.Instance.CurrentScalingObject = null;
    }
    public void ChangeScale(Vector2 newScale) {
        
        Tween.Scale(transform, transform.localScale, newScale, 0.5f, Ease.OutExpo, useUnscaledTime: true);
        Scale = newScale;
    }
}
