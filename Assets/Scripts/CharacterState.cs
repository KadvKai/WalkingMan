using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class CharacterState
{
    protected Animator _characterAnimator;
   
    public UnityEvent<CharacterState, CharacterStateController.State> CharacterStateEnd = new UnityEvent<CharacterState, CharacterStateController.State>();

    public CharacterState(GameObject character)
    {
        _characterAnimator = character.GetComponent<Animator>();
    }

  
    public abstract void StateStart();
    
    public abstract void StateUpdate();

    public abstract void StateEnd();

}
