using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController: MonoBehaviour
{
    private PlayerInput _playerInput;
    private Rigidbody _rigidbody;
    private Vector2 _moveDirection;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.Enable();
        _playerInput.Player.Jump.started +=  Jump_started;
        _playerInput.Player.Jump.performed +=  Jump_performed;
        _playerInput.Player.Jump.canceled += Jump_canceled;
        _playerInput.Player.Move.started += Move_started;
        _playerInput.Player.Move.performed += Move_performed;
        _playerInput.Player.Move.canceled += Move_canceled;
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void Move_started(InputAction.CallbackContext context)
    {
        _moveDirection = context.ReadValue<Vector2>();
        _rigidbody.velocity = new Vector3(_moveDirection.x,0, _moveDirection.y);
        //_rigidbody.velocity = context;
        Debug.Log("Move_started" + context);
    }
    private void Move_performed(InputAction.CallbackContext context)
    {
        Debug.Log("Move_performed" + context.ReadValue<Vector2>());
    }

    private void Move_canceled(InputAction.CallbackContext context)
    {
        Debug.Log("Move_canceled" + context); 
    }



    private void Jump_canceled(InputAction.CallbackContext context)
    {
        Debug.Log("Jump_canceled"); 
    }


    private void Jump_performed(InputAction.CallbackContext context)
    {
        Debug.Log("Jump_performed");
    }

    private void Jump_started(InputAction.CallbackContext context)
    {
        Debug.Log("Jump_started"+ context); 
    }
}
