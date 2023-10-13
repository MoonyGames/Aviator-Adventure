using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Control : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Plane Plane = default;


    protected abstract void StartAction();
    protected abstract void StopAction();

    public void OnPointerDown(PointerEventData eventData)
    {
        StartAction();       
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        StopAction();
    }
}
