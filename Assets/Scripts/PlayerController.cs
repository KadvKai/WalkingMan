using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController: Controller
{
    private PlayerInput _playerInput;
    private Vector2 _moveDirection;
   
    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.Move.Jump.started +=  Jump_started;
        _playerInput.Move.Jump.performed +=  Jump_performed;
        _playerInput.Move.Jump.canceled += Jump_canceled;
        _playerInput.Move.MoveTouchscreen.performed += MoveTouchscreen_performed;
    }
    public override Vector2 GetDirection()
    {
        var moveDirection = _moveDirection;
        _moveDirection = Vector2.zero;
        return moveDirection;
    }
    public override void ControllerEnable()
    {
        _playerInput.Move.Enable();
    }

    public override void ControllerDisable()
    {
        _playerInput.Move.Disable();
    }


    private void MoveTouchscreen_performed(InputAction.CallbackContext context)
    {
        _moveDirection+= context.ReadValue<Vector2>();
        Debug.Log("Move_performed " + context.ReadValue<Vector2>());
    }

 
    private void Jump_canceled(InputAction.CallbackContext context)
    {
        //Debug.Log("Jump_canceled"); 
    }


    private void Jump_performed(InputAction.CallbackContext context)
    {
        //Debug.Log("Jump_performed");
    }

    private void Jump_started(InputAction.CallbackContext context)
    {
        //Debug.Log("Jump_started"+ context); 
    }

}
