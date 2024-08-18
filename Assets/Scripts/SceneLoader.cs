using UnityEngine;
using PrimeTween;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    [SerializeField] private RectTransform _rect;
    [SerializeField] private Vector2 _start;
    [SerializeField] private Vector2 _end;



    private IEnumerator Start() {
        yield return new WaitForSecondsRealtime(1f);
        Fade(false);
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.R)) {
            Fade(true, true);
        }
    }

    public void Fade(bool isFadeIn, bool loadScene=false) {
        Tween.UIAnchoredPosition(_rect, !isFadeIn ? _start : _end, !isFadeIn ? _end : _start, 1f, Ease.OutBounce, useUnscaledTime: true)
            .OnComplete(() => {
                if(!loadScene) return;

                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            });

        
    }
}
