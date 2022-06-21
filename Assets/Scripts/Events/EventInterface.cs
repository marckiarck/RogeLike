using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventInterface : MonoBehaviour
{
    protected bool eventActivated = false;

    public void StartCollisionEvent()
    {
        eventActivated = true;
    }

    public void Update()
    {
        if (eventActivated)
        {
            UpdateCollisionEvent();
        }
    }

    protected virtual void UpdateCollisionEvent() {}
}
