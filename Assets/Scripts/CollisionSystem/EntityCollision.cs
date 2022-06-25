using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Collider2D))]
[RequireComponent(typeof (Rigidbody2D))]
[RequireComponent(typeof (CollisionEvent))]
public class EntityCollision : MonoBehaviour
{

    [SerializeField]
    private string tagFilter;

    [SerializeField]
    private AudioClip collisionSound;

    private CollisionEvent collisonEvent;

    private void Start()
    {
        collisonEvent = gameObject.GetComponent<CollisionEvent>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == tagFilter)
        {
            collisonEvent.CollidedObject = collision.gameObject;
            collisonEvent.CollidingObject = gameObject;

            if (collisionSound)
            {
                GameManager.Instance.PlayGameSound(collisionSound);
            }

            collisonEvent.StartEvent();
        }
    }
}
