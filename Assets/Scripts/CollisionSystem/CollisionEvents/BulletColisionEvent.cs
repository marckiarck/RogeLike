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
        gameObjectAttributes.SetAttributeSafe(AttributeNames.DAMAGE, shooterEvent.GameObjectAttributes.GetAttribute(AttributeNames.DAMAGE));
    }

    protected override void UpdateCollision()
    {

        shooterEvent.DespawnBullet(gameObject);

        if (destructionSound != null)
        {
            audioSource.clip = destructionSound;
            audioSource.Play();
        }

        base.UpdateCollision();
    }
}
