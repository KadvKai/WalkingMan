using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateFreeFall : CharacterState
{
    private readonly CharacterController _ñharacterÑontroller;
    private Vector3 _speed;
    public CharacterStateFreeFall(GameObject character) : base(character)
    {
        _ñharacterÑontroller = character.GetComponent<CharacterController>();
    }


    public override void StartState()
    {
        _speed= _ñharacterÑontroller.velocity;
        // Ïåğåõîä íà àíèìàöèş ïàäåíèÿ
    }

    public override void UpdateState()
    {
        //Debug.Log("isGrounded â FreeFall=" + _ñharacterÑontroller.isGrounded);
        if (_ñharacterÑontroller.isGrounded)
        {
            // Àíèìàöèÿ ïğèçåìëåíèÿ;
            CharacterStateEnd.Invoke(this, CharacterStateController.State.Move);
        }
        else 
        {
            _ñharacterÑontroller.SimpleMove(_speed);
        }
    }
    
}
