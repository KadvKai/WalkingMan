using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class CharacterControllerState
{
    protected Animator _characterAnimator;
    //protected Rigidbody _characterRigidbody;
    protected float _timeChangeStateAnimations;
    protected bool _readyChangeStateAnimations=true;
    //protected int _quantityAnimations=1;
    public UnityEvent<CharacterControllerState> CharacterStateEnd = new UnityEvent<CharacterControllerState>();

    public CharacterControllerState(GameObject character)
    {
        _characterAnimator = character.GetComponent<Animator>();
    }

    public virtual void StartState()
    {
        _timeChangeStateAnimations = Random.Range(0, 5);
    }

    public virtual void UpdateState()
    {
        if (_readyChangeStateAnimations)
        {
            if (_timeChangeStateAnimations < 0)
            {
                ChangeStateAnimations();
            }
            else
            {
                _timeChangeStateAnimations -= Time.deltaTime;
            }
        }
    }

    protected virtual void ChangeStateAnimations() { }
    /*protected void EndState()
    {
        CharacterStateEnd.Invoke(this);
    }*/

}
