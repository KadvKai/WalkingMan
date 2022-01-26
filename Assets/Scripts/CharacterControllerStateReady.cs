using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerStateReady : CharacterControllerState
{
    public CharacterControllerStateReady(GameObject character) : base(character)
    {
    }
    public override void StartState()
    {
        Debug.Log("�������� Ready");
        _characterAnimator.SetTrigger("Ready");
    }
    public override void UpdateState() { }
}
