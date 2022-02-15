using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Controller : MonoBehaviour
{
    public UnityEvent Jump = new UnityEvent();
    public abstract Vector2 GetDirection();
    public abstract void ControllerEnable();
    public abstract void ControllerDisable();
}
