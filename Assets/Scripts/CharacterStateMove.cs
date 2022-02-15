using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateMove : CharacterState
{
    private Vector2 _moveDirection;
    private readonly Controller _controller;
    private const float _speed = 5;
    private readonly Camera _mainCamera;
    private float _rotationVelocity;
    private readonly CharacterController _�haracter�ontroller;
    private readonly float _fallTimeout = 0.15f;
    private float _fallTimeoutDelta;

    public CharacterStateMove(GameObject character) : base(character)
    {
        _controller = character.GetComponent<Controller>();
        _controller.Jump.AddListener(Jump);
        _�haracter�ontroller = character.GetComponent<CharacterController>();
        _mainCamera = UnityEngine.Camera.main;
    }

    public override void StartState()
    {
        _characterAnimator.SetTrigger("Move");
        _fallTimeoutDelta = _fallTimeout;
        _controller.ControllerEnable();
    }

    public override void UpdateState()
    {
        //Debug.Log(_�haracter�ontroller.isGrounded);
        if (_�haracter�ontroller.isGrounded)
        {
            _fallTimeoutDelta = _fallTimeout;
        }
        else
        {
            _fallTimeoutDelta -= Time.deltaTime;
        }
        if (_fallTimeoutDelta >= 0)
        {
            _moveDirection = _controller.GetDirection();
            Rotation();
            Move();
            ChangeAnimations();
        }
        else
        {
            FreeFall();
        }
    }

    private void Rotation()
    {
        if (_moveDirection != Vector2.zero)
        {
            var targetRotation = _mainCamera.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(_�haracter�ontroller.transform.eulerAngles.y, targetRotation, ref _rotationVelocity, 0.2f);
            _�haracter�ontroller.transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }
    }
    private void Move()
    {
        Vector3 targetDirection = _�haracter�ontroller.transform.TransformDirection(new Vector3(_moveDirection.x, 0, _moveDirection.y));
        _�haracter�ontroller.SimpleMove((targetDirection) * _speed);
    }

    private void Jump()
    {
        Debug.Log("��������� Jump");
        StopState();
        CharacterStateEnd.Invoke(this, CharacterStateController.State.Jump);
    }

    private void ChangeAnimations()
    {
        var characterVelocity = _�haracter�ontroller.transform.InverseTransformDirection(_�haracter�ontroller.velocity);
        _characterAnimator.SetFloat("MoveVerticalSpeed", characterVelocity.z);
        _characterAnimator.SetFloat("MoveHorizontalSpeed", characterVelocity.x);
    }

    private void FreeFall()
    {
            StopState();
            CharacterStateEnd.Invoke(this, CharacterStateController.State.FreeFall);
    }

    private void StopState()
    {
        _controller.ControllerDisable();
    }
}
