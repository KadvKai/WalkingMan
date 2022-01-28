using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerStateMove : CharacterControllerState
{
    private float _verticalSpeed;
    private float _horizontalSpeed;
    protected Rigidbody _characterRigidbody;
    public CharacterControllerStateMove(GameObject character) : base(character)
    {
        _characterRigidbody = character.GetComponent<Rigidbody>();
    }

    public override void StartState()
    {
        _characterAnimator.SetTrigger("Move"); ;
    }

    public override void UpdateState()
    {
        _characterAnimator.SetFloat("MoveVerticalSpeed", _verticalSpeed);
        _characterAnimator.SetFloat("MoveHorizontalSpeed", _horizontalSpeed);
    }

    public void SetVectorMove(Vector2 direction) { }
}
