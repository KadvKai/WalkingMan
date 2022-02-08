using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateMove : CharacterState
{
    private float _verticalSpeed;
    private float _horizontalSpeed;
    protected Rigidbody _characterRigidbody;
    private readonly Controller _controller;
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
        _characterAnimator.SetFloat("MoveVerticalSpeed", _verticalSpeed);
        _characterAnimator.SetFloat("MoveHorizontalSpeed", _horizontalSpeed);
    }

    public override void FixedUpdateState()
    {
        var moveDirection = _controller.GetDirection();
        _characterRigidbody.velocity = new Vector3(moveDirection.x, 0, moveDirection.y);
    }

    private void StopState()
    {
        _controller.ControllerDisable();
    }

}
