using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Collider2D))]
[RequireComponent(typeof (Rigidbody2D))]
public class EntityCollision : MonoBehaviour
{

    [SerializeField]
    private LayerMask layer;

    private CollisionEvent collisonEvent;

    private void Awake()
    {
        collisonEvent = gameObject.AddComponent<CollisionEvent>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            collisonEvent.CollidedObject = collision.gameObject;
            collisonEvent.StartCollisionEvent();
        }
    }
}
