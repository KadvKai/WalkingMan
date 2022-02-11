using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateMove : CharacterState
{
    //private float _verticalSpeed;
    //private float _horizontalSpeed;
    protected Rigidbody _characterRigidbody;
    private readonly Controller _controller;
    private const float _speedMax = 5;
    private const float _forceFactor = 500;
    private readonly Camera _mainCamera;
    private float _rotationVelocity;

    public CharacterStateMove(GameObject character) : base(character)
    {
        _characterRigidbody = character.GetComponent<Rigidbody>();
        _controller = character.GetComponent<Controller>();
        _mainCamera= UnityEngine.Camera.main;
    }

    public override void StartState()
    {
        _characterAnimator.SetTrigger("Move");
        _controller.ControllerEnable();
    }

    public override void UpdateState()
    {
        var localVelocity = _characterRigidbody.transform.InverseTransformDirection(_characterRigidbody.velocity);
        //Debug.Log(_characterRigidbody.velocity);
        //Debug.Log(_characterRigidbody.transform.position);
        _characterAnimator.SetFloat("MoveVerticalSpeed", localVelocity.z);
        _characterAnimator.SetFloat("MoveHorizontalSpeed", localVelocity.x);
    }

    public override void FixedUpdateState()
    {
        var moveDirection = _controller.GetDirection();
        if (moveDirection!=Vector2.zero)
        {
        var targetRotation =  _mainCamera.transform.eulerAngles.y;
        float rotation = Mathf.SmoothDampAngle(_characterRigidbody.transform.eulerAngles.y, targetRotation, ref _rotationVelocity, 0.2f);
        _characterRigidbody.MoveRotation(Quaternion.Euler(0.0f, rotation, 0.0f));
        }
        _characterRigidbody.AddRelativeForce(new Vector3(moveDirection.x, 0, moveDirection.y) * _forceFactor);
        if (_characterRigidbody.velocity.magnitude > _speedMax)
        {
            _characterRigidbody.velocity = _characterRigidbody.velocity.normalized * _speedMax;
        }
        //Debug.Log(_characterRigidbody.velocity.magnitude);
        //_characterRigidbody.velocity = new Vector3(moveDirection.x, 0, moveDirection.y);
    }

    private void StopState()
    {
        _controller.ControllerDisable();
    }
}
