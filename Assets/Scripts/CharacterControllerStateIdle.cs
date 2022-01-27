using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerStateIdle : CharacterControllerState
{
    protected int _quantityAnimations;
    protected float _timeChangeStateAnimations;
    protected float _timeChangeTriggerAnimations;
    protected bool _readyChangeStateAnimations = true;
    protected string _name;
    public CharacterControllerStateIdle(GameObject character) : base(character)
    {
        _quantityAnimations = 2;
        _name = "Idle";
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
            Debug.Log("SetTrigger  "+_name);
            _characterAnimator.SetTrigger("Stretching");
            _timeChangeTriggerAnimations = Random.Range(15, 20);
        }
        else
        {
            _timeChangeTriggerAnimations -= Time.deltaTime;
        }
    }

    protected virtual  void ChangeStateAnimations()
    {
        _characterAnimator.SetInteger("StateAnimations", Random.Range(0, _quantityAnimations));
        
    }
}
