using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateIdle : CharacterState
{
    protected int _quantityAnimations;
    protected float _timeChangeStateAnimations;
    protected float _timeChangeTriggerAnimations;
    protected bool _readyChangeStateAnimations = true;
    public CharacterStateIdle(GameObject character) : base(character)
    {
        _quantityAnimations = 2;
    }


    public override void StartState()
    {
        _timeChangeStateAnimations = Random.Range(0, 5);
        _timeChangeTriggerAnimations = Random.Range(5, 10);
    }

    public override void UpdateState()
    {
        if (_readyChangeStateAnimations)
        {
            if (_timeChangeStateAnimations < 0)
            {
                ChangeStateAnimations();
                _timeChangeStateAnimations = Random.Range(10, 15);
            }
            else
            {
                _timeChangeStateAnimations -= Time.deltaTime;
            }
        }
        if (_timeChangeTriggerAnimations < 0)
        {
            _characterAnimator.SetTrigger("Stretching");
            _timeChangeTriggerAnimations = Random.Range(15, 20);
        }
        else
        {
            _timeChangeTriggerAnimations -= Time.deltaTime;
        }
    }
    public override void FixedUpdateState()
    {
    }

    protected virtual  void ChangeStateAnimations()
    {
        _characterAnimator.SetInteger("StateAnimations", Random.Range(0, _quantityAnimations));
        
    }
}
