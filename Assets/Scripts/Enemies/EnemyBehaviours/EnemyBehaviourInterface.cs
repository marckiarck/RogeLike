using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviourInterface : MonoBehaviour
{
    protected AttributeSet enemyAttributes;

    public virtual void Start()
    {
        enemyAttributes = gameObject.GetComponent<AttributeSet>();
    }

    void Update()
    {
        if (enemyAttributes.GetAttribute(AttributeNames.ACTIVATION_DELAY) <= 0f)
        {
            ExecuteBehaviour();
        }
        
    }

    protected virtual void ExecuteBehaviour()
    {
        print("Behaviour interface");
    }
}
