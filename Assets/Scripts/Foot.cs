using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class Foot : MonoBehaviour
{

    public UnityEvent FootStep=new UnityEvent();

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<TerrainCollider>()!=null)
        {
        FootStep.Invoke();
        }
    }
}
