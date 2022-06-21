using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEvent : ConditionalEvents
{
    private AttributeSet gameObjectAttributes;

    void Start()
    {
        //Searches gameObjectAttributes on Start beacuse the attribute set may be created in an Awake
        gameObjectAttributes = gameObject.GetComponent<AttributeSet>();
        
    }

    protected override void UpdateCollisionEvent()
    {

        Destroy(gameObject);
        eventActivated = false;
    }

    public override bool CheckCondition()
    {
        float health = gameObjectAttributes.GetAttribute(AttributeNames.HEALTH);

        if (health <= 0f)
        {
            return true;
        }

        return false;
    }
}
