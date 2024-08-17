using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _jumpForce;
    [SerializeField] private LayerMask _groundLayer;
    private bool _canJump;
    [SerializeField]private Transform _feetPos;


    private void Update() {
        _canJump = Physics2D.OverlapCircle(transform.position, 2f, _groundLayer);
        
        if(Input.GetKeyDown(KeyCode.Space) && _canJump) {
            Jump();
        }
    }

    private void Jump() {
        print("!");
        _rigidbody.AddForce(Vector2.up*_jumpForce, ForceMode2D.Impulse);
    }
    
}
