using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerStateIdle : CharacterControllerState
{
    public CharacterControllerStateIdle(GameObject character) : base(character)
    {
        _quantityAnimations = 4;
    }
}
