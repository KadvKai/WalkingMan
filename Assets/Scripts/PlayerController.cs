using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(VirtualCameraController))]
public class PlayerController: Controller
{
    private PlayerInput _playerInput;
    private Vector2 _moveDirectionCurrent;
    private float  _dpi;
    private VirtualCameraController _virtualCamera;

    private void Awake()
    {
        _dpi = Screen.dpi;
        _playerInput = new PlayerInput();
        _playerInput.Move.Jump.started +=  Jump_started;
        _playerInput.Move.MoveTouchscreen.performed += MoveTouchscreen_performed;
        _playerInput.Move.MoveTouchscreen.canceled += MoveTouchscreen_canceled;
        _playerInput.Move.MoveKeyboard.performed += MoveKeyboard_performed;
        _playerInput.Move.MoveKeyboard.canceled += MoveKeyboard_canceled;
        _playerInput.Look.Look.performed += Look_performed;
        _virtualCamera = GetComponent<VirtualCameraController>();
    }

#if !UNITY_ANDROID
    private void OnApplicationFocus(bool focus)
    {
            Cursor.lockState = true ? CursorLockMode.Locked : CursorLockMode.None;
    }
#endif
    private void Start()
    {
        _playerInput.Enable();
    }
    private void Look_performed(InputAction.CallbackContext context)
    {
        _virtualCamera.CameraRotation(context.ReadValue<Vector2>());
    }
   

    public override Vector2 GetDirection()
    {
        return _moveDirectionCurrent;
    }
    public override void ControllerEnable()
    {
        //enabled=true;
        //_playerInput.Move.Enable();

        //InputSystem.EnableDevice(UnityEngine.InputSystem.Gyroscope.current);
    }

    public override void ControllerDisable()
    {
        //enabled = false;
        //_playerInput.Move.Disable();
        //InputSystem.DisableDevice(UnityEngine.InputSystem.Gyroscope.current);

    }


    private void MoveTouchscreen_performed(InputAction.CallbackContext context)
    {
        _moveDirectionCurrent = context.ReadValue<Vector2>() / (_dpi*Time.deltaTime);
    }
    private void MoveTouchscreen_canceled(InputAction.CallbackContext context)
    {
        _moveDirectionCurrent = Vector2.zero;
    }
    private void MoveKeyboard_performed(InputAction.CallbackContext context)
    {
        _moveDirectionCurrent = context.ReadValue<Vector2>();
    }
    private void MoveKeyboard_canceled(InputAction.CallbackContext context)
    {
        _moveDirectionCurrent = Vector2.zero;
    }

    private void Jump_started(InputAction.CallbackContext context)
    {
        Jump.Invoke();
    }

}
