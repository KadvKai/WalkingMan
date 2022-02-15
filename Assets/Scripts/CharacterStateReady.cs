using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateReady : CharacterStateIdle
{
    public CharacterStateReady(GameObject character) : base(character)
    {
    }
    protected override void ChangeAnimations()
    {
        _characterAnimator.SetTrigger("Ready");
        _readyChangeStateAnimations = false;
    }
}
