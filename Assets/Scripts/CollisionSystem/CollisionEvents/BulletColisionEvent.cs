using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletColisionEvent : HelthReduceCollisionEvent
{
    private ShootEvent shooterEvent;

    [SerializeField]
    private AudioClip destructionSound;

    private AudioSource audioSource;

    public ShootEvent ShooterEvent { get => shooterEvent; set => shooterEvent = value; }

    public void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        AttributeSet gameObjectAttributes = GetComponent<AttributeSet>();
        gameObjectAttributes.SetAttribute(AttributeNames.DAMAGE, shooterEvent.GameObjectAttributes.GetAttribute(AttributeNames.DAMAGE));
    }

    protected override void UpdateEvent()
    {

        shooterEvent.DespawnBullet(gameObject);

        base.UpdateEvent();
    }
}
