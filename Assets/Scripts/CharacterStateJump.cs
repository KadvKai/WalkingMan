using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateJump : CharacterState
{
    public float JumpHeight = 1.2f;
    private readonly CharacterController _�haracter�ontroller;
    private Vector3 _speed;
    private float _verticalVelocity;
    public CharacterStateJump(GameObject character) : base(character)
    {
        _�haracter�ontroller = character.GetComponent<CharacterController>();
    }
    public override void StateStart()
    {
        _speed = _�haracter�ontroller.velocity;
        _verticalVelocity = Mathf.Sqrt(JumpHeight * 2 * 9.81f);
        //Debug.Log("�������� Jump");
        _characterAnimator.SetBool("Jump", true);
    }
    public override void StateUpdate()
    {
        if (_verticalVelocity > 0)
        {
            _�haracter�ontroller.Move((_speed + new Vector3(0.0f, _verticalVelocity, 0.0f)) * Time.deltaTime);
            _verticalVelocity -= 9.81f * Time.deltaTime;
        }
        else
        {
            CharacterStateEnd.Invoke(this, CharacterStateController.State.FreeFall);
        }
    }

    public override void StateEnd()
    {
        _characterAnimator.SetBool("Jump", false); 
    }
}
