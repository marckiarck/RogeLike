using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelthReduceCollisionEvent : CollisionEvent
{
    protected override void UpdateCollision()
    {
        AttributeSet collidedAttributes = CollidedObject.GetComponent<AttributeSet>();
        AttributeSet collidingAttributes = CollidingObject.GetComponent<AttributeSet>();

        float damageApplied = collidingAttributes.GetAttribute(AttributeNames.DAMAGE);

        float reducedHealth = collidedAttributes.GetAttribute(AttributeNames.HEALTH) - damageApplied;

        reducedHealth = Mathf.Max(reducedHealth, 0f);

        collidedAttributes.SetAttributeSafe(AttributeNames.HEALTH, reducedHealth);

        eventActivated = false;
    }
}
