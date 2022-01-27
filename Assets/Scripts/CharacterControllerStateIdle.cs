using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerStateIdle : CharacterControllerState
{
    private readonly int _quantityAnimations;
    public CharacterControllerStateIdle(GameObject character) : base(character)
    {
        _quantityAnimations = 4;
    }

    protected override void ChangeStateAnimations()
    {
        _characterAnimator.SetInteger("StateAnimations", Random.Range(0, _quantityAnimations));
        _timeChangeStateAnimations = Random.Range(10, 15);
    }
}
