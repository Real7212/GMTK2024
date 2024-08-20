using PrimeTween;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    [SerializeField] private Transform _playerTo;

    [SerializeField] private Size _size;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.TryGetComponent<Player>(out var player) 
            && other.TryGetComponent<ScalingObject>(out var scaling)
            && (int)_size >= (int)scaling.CurrentLabel.Size
        ) {
            Tween.Scale(player.transform, 0.3f, 1f, Ease.OutExpo);
            Tween.Position(player.transform, _playerTo.position, 2f, Ease.OutExpo)
                .OnComplete(() => { player.gameObject.SetActive(false); SceneLoader.Instance.Fade(true, true, SceneManager.GetActiveScene().buildIndex+1);}, warnIfTargetDestroyed: false);
        }
    }
}
