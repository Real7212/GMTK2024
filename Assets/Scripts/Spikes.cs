using UnityEngine;

public class Spikes : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D other) {
        if(!other.TryGetComponent<Player>(out var player)) return;
        player.Dead();
    }
}
