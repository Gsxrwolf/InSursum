using System;
using UnityEngine;

public class PCJump : MonoBehaviour
{
    [SerializeField]
    private GameInputReader _gameInput = default;

    [SerializeField]
    [Range(0.1f, 100.0f)]
    private float _defaultJumpForce = 5.0f;

    private Rigidbody _rigidbody = default;
    private float _currentJumpForce;
    private bool _jumpIsTriggered = false;
    private bool _inAir = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _currentJumpForce = _defaultJumpForce;
    }

    private void OnEnable()
    {
        _gameInput.JumpIsPerformed += OnJumpIsPerformed;
        _gameInput.JumpIsCanceled += OnJumpIsCanceled;
    }

    private void FixedUpdate()
    {
        if (_rigidbody.linearVelocity.y > 0.1f || _rigidbody.linearVelocity.y < -0.1f)
            _inAir = true;
        else
            _inAir = false;

        if (_jumpIsTriggered && !_inAir)
            Jump();
    }


    private void OnDisable()
    {
        _gameInput.JumpIsPerformed -= OnJumpIsPerformed;
        _gameInput.JumpIsCanceled -= OnJumpIsCanceled;
    }

    private void OnJumpIsPerformed()
    {
        _jumpIsTriggered = true;
    }

    private void OnJumpIsCanceled()
    {
        _jumpIsTriggered = false;
    }
    private void Jump()
    {
        _rigidbody.AddRelativeForce(Vector3.up * _currentJumpForce * _rigidbody.mass, ForceMode.Impulse);
    }
}
