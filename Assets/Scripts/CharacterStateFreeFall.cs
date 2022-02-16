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


    public override void StartState()
    {
        _speed= _�haracter�ontroller.velocity;
        //Debug.Log("�������� FreeFall");
        _characterAnimator.SetTrigger("FreeFall");
    }

    public override void UpdateState()
    {
        //Debug.Log("isGrounded � FreeFall=" + _�haracter�ontroller.isGrounded);
        if (_�haracter�ontroller.isGrounded)
        {
            //Debug.Log("�������� Falling");
            _characterAnimator.SetTrigger("Falling");
            CharacterStateEnd.Invoke(this, CharacterStateController.State.Move);
        }
        else 
        {
            _�haracter�ontroller.SimpleMove(_speed);
        }
    }
    
}
