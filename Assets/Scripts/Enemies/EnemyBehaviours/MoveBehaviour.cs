using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBehaviour : EnemyBehaviourInterface
{
    [SerializeField]
    protected float moveSpeed = 0f;
    protected Vector2 moveDirection = Vector2.right;

    private void Start()
    {
        AttributeSet gameObjectAttributes = gameObject.GetComponent<AttributeSet>();
        if (gameObjectAttributes != null)
        {
            float speedAttribute = gameObjectAttributes.GetAttribute(AttributeNames.SPEED);

            if (speedAttribute != 0f)
            {
                moveSpeed = speedAttribute;
            }
        }
    }

    protected override void ExecuteBehaviour()
    {
        Move();
    }

    protected void Move()
    {
        transform.position += (Vector3)moveDirection.normalized * moveSpeed * Time.deltaTime;
    }
}
