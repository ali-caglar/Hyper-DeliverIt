using UnityEngine;
using Lean.Touch;
using PathCreation;

public class MovementController : MonoBehaviour
{
    [SerializeField] private PathCreator _pathCreator;
    [SerializeField] private EndOfPathInstruction _endOfPathInstruction;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _deceleration;

    private bool _isTouching;
    private float _currentSpeed;
    private float _distanceTravelled;

    private void OnEnable()
    {
        LeanTouch.OnFingerOld += StartMoving;
        LeanTouch.OnFingerUp += StopMoving;
    }

    private void OnDisable()
    {
        LeanTouch.OnFingerOld -= StartMoving;
        LeanTouch.OnFingerUp -= StopMoving;
    }

    private void Update()
    {
        if (_pathCreator == null) return;

        if (_isTouching)
        {
            Accelerate();
        }
        else
        {
            Decelerate();
        }

        if (_currentSpeed <= 0) return;

        _distanceTravelled += _currentSpeed * Time.deltaTime;
        transform.position = _pathCreator.path.GetPointAtDistance(_distanceTravelled, _endOfPathInstruction);
        transform.rotation = _pathCreator.path.GetRotationAtDistance(_distanceTravelled, _endOfPathInstruction);
    }

    private void Accelerate()
    {
        if (_currentSpeed < _maxSpeed)
        {
            _currentSpeed += _acceleration * Time.deltaTime;
        }

        _currentSpeed = Mathf.Clamp(_currentSpeed, 0, _maxSpeed);
    }

    private void Decelerate()
    {
        if (_currentSpeed > 0)
        {
            _currentSpeed -= _deceleration * Time.deltaTime;
        }

        _currentSpeed = Mathf.Clamp(_currentSpeed, 0, _maxSpeed);
    }

    private void StartMoving(LeanFinger finger)
    {
        _isTouching = true;
    }

    private void StopMoving(LeanFinger finger)
    {
        _isTouching = false;
    }
}