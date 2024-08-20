using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


public class Player : MonoBehaviour
{

    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Animator _animator;
    
    private float _speed;
    [SerializeField] private float _normalSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private AudioSource _jumpSound;
    [SerializeField] private float _checkRadius;
    [SerializeField] private float _fallingRadius;

    [SerializeField] private LayerMask _groundLayer;
    private bool _canJump;
    private bool _isStarted;

    private bool _isJumping;
    [SerializeField] private Transform _feetPos;

    private float _moveInput;
    private bool _facingRight = true;

    [SerializeField] private ScalingObject _scaling;

    [SerializeField] private GameObject _blood;
    private string _currentState;
    private float _lockedTill;

    private bool _isFinished;
    [SerializeField] private AudioClip _stepClip;
    private Coroutine _stepsCoroutine;

    private void FixedUpdate() {
        _rigidbody.velocity = new Vector2(_moveInput*_speed, _rigidbody.velocity.y);
    }

    private void Update() {

        _canJump = _scaling.CurrentShape == ShapeType.Normal ? 
            Physics2D.Raycast(_feetPos.position, Vector2.down, _scaling.CurrentLabel.Size == Size.BIG ? _checkRadius + 0.5f : _checkRadius, _groundLayer) : 
            Physics2D.OverlapCircle(_feetPos.position, _checkRadius, _groundLayer);

        if(_canJump && !_isStarted) _isStarted = true;

        if(!_isFinished && _isStarted) {
            
            _moveInput = Input.GetAxisRaw("Horizontal");
            if((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && _canJump) {
                Jump();
            }

            

            if(_facingRight && _moveInput < 0) Flip();
            if(!_facingRight && _moveInput > 0) Flip(); 

            _speed = _scaling.CurrentLabel.Size switch {
                Size.SMALL => _normalSpeed * 1.25f,
                Size.MEDIUM => _normalSpeed * 1f,
                Size.BIG => _normalSpeed * 0.75f,
                _ => _normalSpeed * 1f
            };

            if(_animator == null) return;

            _animator.speed = _scaling.CurrentLabel.Size switch {
                Size.SMALL => 2f,
                Size.MEDIUM => 1f,
                Size.BIG => 0.5f,
                _ => 1f
            };


        }

        if(_animator == null) return;
        if(_currentState == GetState()) return;
        StartCoroutine(Animate(GetState()));
        



        
    }

    private void Jump() {
        _isJumping = true;
        _rigidbody.AddForce(Vector2.up*_jumpForce, ForceMode2D.Impulse);
        _jumpSound.Play();
    }

    private void Flip() {
        _facingRight = !_facingRight;
        transform.Rotate(0, 180, 0);
    }
    

    private string GetState() {
        
        if(_isFinished) return "finish";

        if(_canJump){
            if(_isJumping) return "land";

            return _moveInput == 0 ? "idle" : "walking";
        }

        if(_rigidbody.velocity.y < 0) return "fall";
        if (_rigidbody.velocity.y > 0) return "jump";
        
        
        return "land";
    }

    private IEnumerator Animate(string state) {

        // switch(state) {
        //     case "idle":
        //         _animator.CrossFade("idle", 0.1f, 0);
        //         yield return new WaitForSeconds(0.1f);
        //         _animator.CrossFade("idle", 0f, 0);
        //         _currentState = "idle";
        //         break;
        //     case "walking":
                
        //     case 
        // }
        _animator.CrossFade(state, 0.1f, 0);
        yield return new WaitForSeconds(0.1f);
        _animator.CrossFade(state, 0f, 0);
        _currentState = state;
    }


    private void OnDrawGizmosSelected() {
        Gizmos.DrawRay(_feetPos.position, Vector2.down*_checkRadius);

    }

    public void Dead() {
        Instantiate(_blood, transform.position, Quaternion.identity);
        Music.Instance.Dead();
        SceneLoader.Instance.Die();
        Destroy(gameObject);
        
    }

    public void Land() {
        _isJumping = false;
    }

    public void PlayStepSound() {
        AudioSource.PlayClipAtPoint(_stepClip, new Vector3(0, 0, -10), 0.5f);
    }

    public void Finish() {
        _isFinished = true;
        _rigidbody.isKinematic = true;
    }
}
