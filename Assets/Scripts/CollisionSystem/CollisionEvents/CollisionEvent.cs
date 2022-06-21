using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionEvent : EventInterface
{
    private GameObject collidedObject;
    private GameObject collidingObject;

    public GameObject CollidedObject { get => collidedObject; set => collidedObject = value; }
    public GameObject CollidingObject { get => collidingObject; set => collidingObject = value; }
}
