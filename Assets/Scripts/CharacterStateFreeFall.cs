using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateFreeFall : CharacterState
{
    private readonly CharacterController _Òharacter—ontroller;
    private Vector3 _speed;
    public CharacterStateFreeFall(GameObject character) : base(character)
    {
        _Òharacter—ontroller = character.GetComponent<CharacterController>();
    }


    public override void StateStart()
    {
        _speed= _Òharacter—ontroller.velocity;
        //Debug.Log("¿ÌËÏ‡ˆËˇ FreeFall");
        _characterAnimator.SetBool("FreeFall", true);
    }

    public override void StateUpdate()
    {
        //Debug.Log("isGrounded ‚ FreeFall=" + _Òharacter—ontroller.isGrounded);
        if (_Òharacter—ontroller.isGrounded)
        {
            CharacterStateEnd.Invoke(this, CharacterStateController.State.Move);
        }
        else 
        {
            _Òharacter—ontroller.SimpleMove(_speed);
        }
    }
    public override void StateEnd()
    {
        _characterAnimator.SetBool("FreeFall", false);
    }
    
}
