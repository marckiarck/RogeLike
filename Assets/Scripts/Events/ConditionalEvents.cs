using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionalEvents : EventInterface
{

    public override void Update()
    {
        base.Update();

        if (CheckCondition())
        {
            StartEvent();
        }
    }

    public virtual bool CheckCondition()
    {
        return false;
    }
}
