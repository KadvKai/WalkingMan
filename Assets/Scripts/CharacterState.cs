using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class CharacterState
{
    protected Animator _characterAnimator;
    //protected Rigidbody _characterRigidbody;
    //protected float _timeChangeStateAnimations;
    //protected bool _readyChangeStateAnimations=true;
    //protected int _quantityAnimations=1;
    public UnityEvent<CharacterState> CharacterStateEnd = new UnityEvent<CharacterState>();

    public CharacterState(GameObject character)
    {
        _characterAnimator = character.GetComponent<Animator>();
    }

    public abstract void StartState();
    
    public abstract void UpdateState();

    public abstract void FixedUpdateState();
    

}
