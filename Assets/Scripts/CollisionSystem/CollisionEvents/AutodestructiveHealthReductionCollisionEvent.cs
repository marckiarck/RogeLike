using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutodestructiveHealthReductionCollisionEvent : HelthReduceCollisionEvent
{
    protected override void UpdateCollisionEvent()
    {
        base.UpdateCollisionEvent();
        Destroy(gameObject);
    }
}
