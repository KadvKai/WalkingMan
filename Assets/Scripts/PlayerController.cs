using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(VirtualCameraTarget))]
public class PlayerController: Controller
{
    private PlayerInput _playerInput;
    private Vector2 _moveDirectionCurrent;
    private float  _dpi;
    private VirtualCameraTarget _virtualCamera;

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
        _virtualCamera = GetComponent<VirtualCameraTarget>();
    }

#if !UNITY_ANDROID
    private void OnApplicationFocus(bool focus)
    {
            Cursor.lockState = true ? CursorLockMode.Locked : CursorLockMode.None;
    }
#endif
    /*private void Update()
    {
        Debug.Log("Update");
    }*/

    private void Look_performed(InputAction.CallbackContext context)
    {
        //Debug.Log("Look_performed " + context.ReadValue<Vector2>());
        _virtualCamera.CameraRotation(context.ReadValue<Vector2>());
    }
   

    public override Vector2 GetDirection()
    {
        //Debug.Log("GetDirection moveDirection*100=" + _moveDirectionCurrent * 100);
        return _moveDirectionCurrent;
    }
    public override void ControllerEnable()
    {
        //enabled=true;
        _playerInput.Enable();

        //InputSystem.EnableDevice(UnityEngine.InputSystem.Gyroscope.current);
    }

    public override void ControllerDisable()
    {
        //enabled = false;
        _playerInput.Disable();
        //InputSystem.DisableDevice(UnityEngine.InputSystem.Gyroscope.current);

    }


    private void MoveTouchscreen_performed(InputAction.CallbackContext context)
    {
        _moveDirectionCurrent = context.ReadValue<Vector2>() / (_dpi*Time.deltaTime);
        //Debug.Log("MoveTouchscreen_performed " + context);
    }
    private void MoveTouchscreen_canceled(InputAction.CallbackContext context)
    {
        _moveDirectionCurrent = Vector2.zero;
        //Debug.Log("MoveTouchscreen_canceled " + context);
    }
    private void MoveKeyboard_performed(InputAction.CallbackContext context)
    {
        _moveDirectionCurrent = context.ReadValue<Vector2>();
        //Debug.Log("MoveKeyboard_performed " + context.ReadValue<Vector2>());
        //Debug.Log("MoveKeyboard_performed " + context);
    }
    private void MoveKeyboard_canceled(InputAction.CallbackContext context)
    {
        _moveDirectionCurrent = Vector2.zero;
        //Debug.Log("MoveKeyboard_canceled " + context.ReadValue<Vector2>());
        //Debug.Log("MoveKeyboard_canceled " + context);
    }

    private void Jump_started(InputAction.CallbackContext context)
    {
        //Debug.Log("Jump_started"); 
        Jump.Invoke();
    }

}
