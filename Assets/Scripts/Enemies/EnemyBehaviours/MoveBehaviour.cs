using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBehaviour : EnemyBehaviourInterface
{
    [SerializeField]
    protected float moveSpeed = 0f;
    protected Vector2 moveDirection = Vector2.right;

    protected override void ExecuteBehaviour()
    {
        transform.position += (Vector3)moveDirection * moveSpeed * Time.deltaTime;
    }

}
