using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionEvent : EventInterface
{
    private GameObject collidedObject;

    public GameObject CollidedObject { get => collidedObject; set => collidedObject = value; }

    protected override void UpdateCollisionEvent()
    {
        print("Executing a collision Event");

        AttributeSet collidedObjectAttributes = collidedObject.GetComponent<AttributeSet>();
        if (collidedObjectAttributes != null)
        {
            float currentHealth = collidedObjectAttributes.GetAttribute(AttributeNames.HEALTH);
            collidedObjectAttributes.SetAttributeSafe(AttributeNames.HEALTH, currentHealth - 1f);
        }

        eventActivated = false;
    }
}
