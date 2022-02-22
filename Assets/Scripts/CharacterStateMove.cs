using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateMove : CharacterState
{
    private Vector2 _moveDirection;
    private readonly Controller _controller;
    private const float _accelerationMoveDirection=3;
    private const float _speedForward = 5f;
    private const float _speedSide = 3f;
    private const float _speedBack = 3f;
    private readonly Camera _mainCamera;
    private float _rotationVelocity;
    private readonly CharacterController _�haracter�ontroller;
    private readonly float _fallTimeout = 0.15f;
    private float _fallTimeoutDelta;
    private const float _maxAngleCameraRotation = 45;
    private const float _speedRotation = 2;
    private bool _turn;
    private Vector3 _targetRotation;
    private const float _stepRotationBehindCamera = 60;
    public CharacterStateMove(GameObject character) : base(character)
    {
        _controller = character.GetComponent<Controller>();
        _controller.Jump.AddListener(Jump);
        _�haracter�ontroller = character.GetComponent<CharacterController>();
        _mainCamera = UnityEngine.Camera.main;
    }

    public override void StateStart()
    {
        //Debug.Log("�������� Move");
        _characterAnimator.SetBool("Move", true);
        _fallTimeoutDelta = _fallTimeout;
        _controller.ControllerEnable();
    }

    public override void StateUpdate()
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
            if (_turn==true) Turn();
            if (_moveDirection == Vector2.zero) RotationBehindCamera();
            if (_moveDirection != Vector2.zero && _turn == false)  Rotation();
            if (_turn == false)  Move();
        }
        else
        {
            FreeFall();
        }
    }

    public override void StateEnd()
    {
        _characterAnimator.SetBool("Move", false);
        _controller.ControllerDisable();
    }


    private void RotationBehindCamera()
    {
        var mainCameraOnPlane = Vector3.ProjectOnPlane(_mainCamera.transform.forward, _�haracter�ontroller.transform.up);
        var cameraRotation = Vector3.SignedAngle(_�haracter�ontroller.transform.forward, mainCameraOnPlane, _�haracter�ontroller.transform.up);
        //Debug.Log("EulerAngles=" + _mainCamera.transform.rotation.eulerAngles);
        if (cameraRotation > _maxAngleCameraRotation)
        {
            _characterAnimator.SetInteger("Rotation", 1);
            _targetRotation = Vector3.Lerp(_�haracter�ontroller.transform.forward, _�haracter�ontroller.transform.right, _stepRotationBehindCamera / 90);
            _turn = true;
        }
        else if (cameraRotation < -_maxAngleCameraRotation)
        {
            _characterAnimator.SetInteger("Rotation", -1);
            _targetRotation = Vector3.Lerp(_�haracter�ontroller.transform.forward, -_�haracter�ontroller.transform.right, _stepRotationBehindCamera / 90);
            _turn = true;
        }
        else
        {
            _characterAnimator.SetInteger("Rotation", 0);
        }

    }
    private void Rotation()
    {
            var targetRotation = _mainCamera.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(_�haracter�ontroller.transform.eulerAngles.y, targetRotation, ref _rotationVelocity, 0.2f);
            _�haracter�ontroller.transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
    }
    private void Move()
    {
        
        Vector3 targetDirection = _�haracter�ontroller.transform.TransformDirection(new Vector3(_moveDirection.x *_speedSide, 0, _moveDirection.y > 0 ? _moveDirection.y * _speedForward : _moveDirection.y * _speedBack)) ;
        _�haracter�ontroller.SimpleMove((targetDirection));
        //Debug.Log("Move" + _�haracter�ontroller.velocity);
            ChangeAnimations();

    }

    private void Jump()
    {
        //Debug.Log("��������� Jump");
        //Debug.Log("MoveJump" + _�haracter�ontroller.velocity);
        StateEnd();
        CharacterStateEnd.Invoke(this, CharacterStateController.State.Jump);
    }

    private void ChangeAnimations()
    {
        var characterVelocity = _�haracter�ontroller.transform.InverseTransformDirection(_�haracter�ontroller.velocity);
        //Debug.Log("VerticalSpeed=" + characterVelocity.z+ "  HorizontalSpeed=" + characterVelocity.x);
        _characterAnimator.SetFloat("MoveVerticalSpeed", (float)Math.Round(characterVelocity.z, 1) );
        _characterAnimator.SetFloat("MoveHorizontalSpeed", (float)Math.Round(characterVelocity.x, 1));
    }

    private void FreeFall()
    {
        //Debug.Log("�������� Falling");
        StateEnd();
            CharacterStateEnd.Invoke(this, CharacterStateController.State.FreeFall);
    }
    private void Turn()
    {
            if (Vector3.Angle(_�haracter�ontroller.transform.forward, _targetRotation) <1)
            {
                _�haracter�ontroller.transform.forward = _targetRotation;
                _turn = false;
            }
            if (Vector3.Angle(_�haracter�ontroller.transform.forward, Vector3.ProjectOnPlane(_mainCamera.transform.forward, _�haracter�ontroller.transform.up).normalized) < 1)
            {
                _�haracter�ontroller.transform.forward = Vector3.ProjectOnPlane(_mainCamera.transform.forward, _�haracter�ontroller.transform.up).normalized;
                _turn = false;
            }
            _�haracter�ontroller.transform.forward = Vector3.RotateTowards(_�haracter�ontroller.transform.forward, _targetRotation, _speedRotation * Time.deltaTime, 0.0f);
    }
}
