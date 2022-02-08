using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    public abstract Vector2 GetDirection();
    public abstract void ControllerEnable();
    public abstract void ControllerDisable();
}
