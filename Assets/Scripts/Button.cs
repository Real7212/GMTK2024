using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private bool _isPressing;
    private string _currentState;

    [SerializeField] private List<UnityEvent> OnButtonPressed;
    [SerializeField] private List<UnityEvent> OnButtonUnpressed;

    private void Update() {
        if(_currentState == GetState()) return;

        StartCoroutine(Animate(GetState()));
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            _isPressing = true;
            foreach(var e in OnButtonPressed) {
                e?.Invoke();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            _isPressing = false;
            foreach(var e in OnButtonUnpressed) {
                e?.Invoke();
            }
        }
    }

    private string GetState() {
        if(_isPressing) return "pressed";
        else return "idle";
    }

    private IEnumerator Animate(string state) {

        switch(state) {
            case "idle":
                _animator.CrossFade("idle", 0.1f, 0);
                yield return new WaitForSeconds(0.1f);
                _animator.CrossFade("idle", 0f, 0);
                _currentState = "idle";
                break;
            case "pressed":
                _animator.CrossFade("pressed", 0.1f, 0);
                yield return new WaitForSeconds(0.1f);
                _animator.CrossFade("pressed", 0f, 0);
                _currentState = "pressed";
                break;

        }
    }
}
