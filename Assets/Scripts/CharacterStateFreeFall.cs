using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateFreeFall : CharacterState
{
    private readonly CharacterController _�haracter�ontroller;
    private Vector3 _speed;
    public CharacterStateFreeFall(GameObject character) : base(character)
    {
        _�haracter�ontroller = character.GetComponent<CharacterController>();
    }


    public override void StateStart()
    {
        _speed= _�haracter�ontroller.velocity;
        //Debug.Log("�������� FreeFall");
        _characterAnimator.SetBool("FreeFall", true);
    }

    public override void StateUpdate()
    {
        //Debug.Log("isGrounded � FreeFall=" + _�haracter�ontroller.isGrounded);
        if (_�haracter�ontroller.isGrounded)
        {
            CharacterStateEnd.Invoke(this, CharacterStateController.State.Move);
        }
        else 
        {
            _�haracter�ontroller.SimpleMove(_speed);
        }
    }
    public override void StateEnd()
    {
        _characterAnimator.SetBool("FreeFall", false);
    }
    
}
