using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class CharacterControllerState
{
    protected Animator _characterAnimator;
    //protected Rigidbody _characterRigidbody;
    //protected float _timeChangeStateAnimations;
    //protected bool _readyChangeStateAnimations=true;
    //protected int _quantityAnimations=1;
    public UnityEvent<CharacterControllerState> CharacterStateEnd = new UnityEvent<CharacterControllerState>();

    public CharacterControllerState(GameObject character)
    {
        _characterAnimator = character.GetComponent<Animator>();
    }

    public abstract void StartState();
    

    public abstract void UpdateState();
    

}
