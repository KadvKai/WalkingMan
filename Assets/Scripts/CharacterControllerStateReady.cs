using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerStateReady : CharacterControllerStateIdle
{
    public CharacterControllerStateReady(GameObject character) : base(character)
    {
    }
    protected override void ChangeStateAnimations()
    {
        _characterAnimator.SetTrigger("Ready");
        _readyChangeStateAnimations = false;
    }
}
