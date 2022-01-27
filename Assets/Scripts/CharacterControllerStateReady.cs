using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerStateReady : CharacterControllerStateIdle
{
    public CharacterControllerStateReady(GameObject character) : base(character)
    {
        _name = "Ready";
    }
    protected override void ChangeStateAnimations()
    {
        _characterAnimator.SetTrigger("Ready");
        _readyChangeStateAnimations = false;
    }
}
