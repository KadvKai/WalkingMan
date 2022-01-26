using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class CharacterControllerState
{
    protected Animator _characterAnimator;
    protected Rigidbody _characterRigidbody;
    protected float _timeChangeState;
    protected int _stateAnimations;
    protected int _newStateAnimations;
    protected int _quantityAnimations;
    public UnityEvent<CharacterControllerState> CharacterStateEnd=new UnityEvent<CharacterControllerState>();

    public  CharacterControllerState(GameObject character)
    {
        _characterAnimator = character.GetComponent<Animator>();
        _characterRigidbody = character.GetComponent<Rigidbody>();
    }

    public virtual void StartState()
    {
        Debug.Log("virtual");
        _stateAnimations = 0;
        _timeChangeState = Random.Range(10, 15);
    }
    public virtual void UpdateState()
    {
        if (_timeChangeState < 0)
        {
            do
            {
                _newStateAnimations = Random.Range(0, _quantityAnimations);
            }
            while (_newStateAnimations == _stateAnimations);
            _stateAnimations = _newStateAnimations;
            _characterAnimator.SetInteger("StateAnimations", _stateAnimations);
            _timeChangeState = Random.Range(10, 15);
        }
        else
        {
            _timeChangeState -= Time.deltaTime;
        }
    }
    protected void EndState()
    {
        CharacterStateEnd.Invoke(this);
    }

}
