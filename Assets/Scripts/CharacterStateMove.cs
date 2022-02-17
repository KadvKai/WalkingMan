using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateMove : CharacterState
{
    private Vector2 _moveDirection;
    private readonly Controller _controller;
    private const float _accelerationMoveDirection=3;
    private const float _speedForward = 2f;
    private const float _speedSide = 2f;
    private const float _speedBack = 1.2f;
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
        //Debug.Log("�������� Move");
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
            _moveDirection = Vector2.MoveTowards(_moveDirection, _controller.GetDirection(),_accelerationMoveDirection*Time.deltaTime);
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
        Vector3 targetDirection = _�haracter�ontroller.transform.TransformDirection(new Vector3(_moveDirection.x *_speedSide, 0, _moveDirection.y > 0 ? _moveDirection.y * _speedForward : _moveDirection.y * _speedBack)) ;
        _�haracter�ontroller.SimpleMove((targetDirection));
        //Debug.Log(_�haracter�ontroller.transform.InverseTransformDirection(_�haracter�ontroller.velocity));
    }

    private void Jump()
    {
        //Debug.Log("��������� Jump");
        StopState();
        CharacterStateEnd.Invoke(this, CharacterStateController.State.Jump);
    }

    private void ChangeAnimations()
    {
        var characterVelocity = _�haracter�ontroller.transform.InverseTransformDirection(_�haracter�ontroller.velocity);
        Debug.Log("VerticalSpeed=" + characterVelocity.z+ "  HorizontalSpeed=" + characterVelocity.x);
        _characterAnimator.SetFloat("MoveVerticalSpeed", (float)Math.Round(characterVelocity.z, 1)+1 );
        _characterAnimator.SetFloat("MoveHorizontalSpeed", (float)Math.Round(characterVelocity.x, 1));
    }

    private void FreeFall()
    {
        //Debug.Log("�������� Falling");
        //_characterAnimator.SetTrigger("Falling");
        StopState();
            CharacterStateEnd.Invoke(this, CharacterStateController.State.FreeFall);
    }

    private void StopState()
    {
        _controller.ControllerDisable();
    }
}
