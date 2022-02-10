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
    public CharacterStateMove(GameObject character) : base(character)
    {
        _characterRigidbody = character.GetComponent<Rigidbody>();
        _controller = character.GetComponent<Controller>();
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
        _characterAnimator.SetFloat("MoveVerticalSpeed", localVelocity.z);
        _characterAnimator.SetFloat("MoveHorizontalSpeed", localVelocity.x);
    }

    public override void FixedUpdateState()
    {
        var moveDirection = _controller.GetDirection();
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
