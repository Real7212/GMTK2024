using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public enum PlayerType {
    Circle,
    Square,
    Triangle,
    Man
}

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerType _type;
    
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _checkRadius;
    [SerializeField] private float _fallingRadius;

    [SerializeField] private LayerMask _groundLayer;
    private bool _canJump;
    private bool _isFalling;

    private bool _isJumping;
    [SerializeField] private Transform _feetPos;

    private float _moveInput;
    private bool _facingRight = true;

    [SerializeField] private ScalingObject _scaling;
    [SerializeField] private GameObject _blood;
    private string _currentState;

    private void FixedUpdate() {
        _rigidbody.velocity = new Vector2(_moveInput*_speed, _rigidbody.velocity.y);
    }

    private void Update() {
        _canJump = Physics2D.OverlapCircle(_feetPos.position, _checkRadius, _groundLayer);
        _isFalling = !Physics2D.OverlapCircle(_feetPos.position, _fallingRadius, _groundLayer);
        
        _moveInput = _type == PlayerType.Man ? Input.GetAxisRaw("Horizontal") : 0;
        if((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && _canJump) {
            Jump();
        }

        

        if(_facingRight && _moveInput < 0) Flip();
        if(!_facingRight && _moveInput > 0) Flip(); 

        if(_animator == null) return;

        _animator.speed = _scaling.CurrentSize switch {
            Size.SMALL => 2f,
            Size.MEDIUM => 1f,
            Size.BIG => 0.5f,
            _ => 1f
        };
        
        if(_currentState == GetState()) return;
        StartCoroutine(Animate(GetState()));
        
    }

    private void Jump() {
        _isJumping = true;
        _rigidbody.AddForce(Vector2.up*_jumpForce, ForceMode2D.Impulse);
    }

    private void Flip() {
        _facingRight = !_facingRight;
        transform.Rotate(0, 180, 0);
    }
    

    private string GetState() {
        if(_isJumping && _rigidbody.velocity.y>_jumpForce) return "jump";
        if(_isFalling && _rigidbody.velocity.y<_jumpForce) return "fall";

        return _moveInput == 0 ? "idle" : "walking";

    }

    private IEnumerator Animate(string state) {

        switch(state) {
            case "idle":
                _animator.CrossFade("idle", 0.1f, 0);
                yield return new WaitForSeconds(0.1f);
                _animator.CrossFade("idle", 0f, 0);
                _currentState = "idle";
                break;
            case "walking":
                _animator.CrossFade("walking", 0.1f, 0);
                yield return new WaitForSeconds(0.1f);
                _animator.CrossFade("walking", 0f, 0);
                _currentState = "walking";
                break;
            case "jump":
                _animator.CrossFade("jump", 0f, 0);
                _currentState = "jump";
                break;
            case "fall":
                _animator.CrossFade("fall", 0.1f, 0);
                yield return new WaitForSeconds(0.1f);
                _animator.CrossFade("fall", 0f, 0);
                _currentState = "fall";
                break;


        }

    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(_feetPos.position, _checkRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_feetPos.position, _fallingRadius);
    }

    public void Dead() {
        Instantiate(_blood, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
