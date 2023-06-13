using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyHandler : MonoBehaviour
{
    public delegate void ObjectDestroyedEventHandler();
    public event ObjectDestroyedEventHandler ObjectDestroyed;

    private void OnDestroy()
    {
        Debug.Log("OnDestroy");
        ObjectDestroyed?.Invoke();
    }
}
