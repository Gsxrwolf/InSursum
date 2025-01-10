using System;
using UnityEngine;

public class PCMovement : MonoBehaviour, IAmMovable
{
    [SerializeField]
    private GameInputReader _gameInput = default;

    [Space (10)]
    [SerializeField]
    private float _walkingSpeed = 1.0f;
    [SerializeField]
    private float _runningSpeed = 2.0f;

    private Rigidbody _rigidbody = default;
    private Vector3 _moveInput = Vector3.zero;
    private float _currentSpeed = 1.0f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _gameInput.MoveInputHasChanged += OnMoveInputHasChanged;
        _gameInput.SprintIsTriggered += OnSprintIsTriggered;
    }

    private void Start()
    {
        _currentSpeed = _walkingSpeed;
    }
    private void FixedUpdate()
    {
        Move();
    }

    private void OnDisable()
    {
        _gameInput.MoveInputHasChanged -= OnMoveInputHasChanged;
    }

    private void OnMoveInputHasChanged(Vector2 moveInput)
    {
        _moveInput = new Vector3(moveInput.x, 0, moveInput.y);
    }

    private void OnSprintIsTriggered(bool sprintIsTriggered)
    {
        _currentSpeed = sprintIsTriggered ? _runningSpeed : _walkingSpeed;
    }

    public void Move()
    {
        _rigidbody.AddRelativeForce(_rigidbody.mass * _moveInput * _currentSpeed / Time.fixedDeltaTime, ForceMode.Force);
    }
}
