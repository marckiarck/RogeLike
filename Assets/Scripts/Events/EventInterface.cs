using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventInterface : MonoBehaviour
{
    protected bool eventActivated = false;

    public void StartEvent()
    {
        eventActivated = true;
    }

    public virtual void Update()
    {
        if (eventActivated)
        {
            UpdateCollisionEvent();
        }
    }

    protected virtual void UpdateCollisionEvent() {}
}
