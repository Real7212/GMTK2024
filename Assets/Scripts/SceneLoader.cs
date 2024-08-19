using UnityEngine;
using PrimeTween;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance {get;private set;}
    [SerializeField] private RectTransform _rect;
    [SerializeField] private Vector2 _start;
    [SerializeField] private Vector2 _end;

    private bool _isFading = true;

    private void Awake() {
        if(Instance == null) Instance = this;
        else Destroy(Instance);
    }

    private void Start() {
        Fade(false);
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.R)) {
            Fade(true, true);
        }
    }

    public void Fade(bool isFadeIn, bool loadScene=false, int toLoad = -1) {
        Tween.UIAnchoredPosition(_rect, !isFadeIn ? _start : _end, !isFadeIn ? _end : _start, 1f, Ease.OutBounce, useUnscaledTime: true)
            .OnComplete(() => {
                if(!loadScene) return;
                if(toLoad == -1) toLoad = SceneManager.GetActiveScene().buildIndex;

                SceneManager.LoadScene(toLoad);
            });

        
    }

    public void Die() {
        StartCoroutine(Dead());
    }

    private IEnumerator Dead() {
        yield return new WaitForSeconds(1.5f);
        Fade(true, true);

    }
}
