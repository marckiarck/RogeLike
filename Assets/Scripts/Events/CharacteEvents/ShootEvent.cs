using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(AttributeSet))]
public class ShootEvent : EventInterface
{
    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private AudioClip shootSound;

    private AttributeSet gameObjectAttributes;

    private List<GameObject> spawnedBullets;
    private List<GameObject> despawnedBullets;

    private AudioSource audioSource;

    private Collider2D gameObjectCollider;

    private Vector2 shootDirection;

    public Vector2 ShootDirection { get => shootDirection; set => shootDirection = value; }
    public AttributeSet GameObjectAttributes { get => gameObjectAttributes;}

    public void Awake()
    {
        spawnedBullets = new List<GameObject>();
        despawnedBullets = new List<GameObject>();
        audioSource = GetComponent<AudioSource>();
        gameObjectCollider = GetComponent<Collider2D>();
    }

    public void Start()
    {
        gameObjectAttributes = gameObject.GetComponent<AttributeSet>();

        for (int i = 0; i < gameObjectAttributes.GetAttribute(AttributeNames.MAX_BULLETS); ++i)
        {
            GameObject createdBullet = Instantiate<GameObject>(bulletPrefab);
            createdBullet.GetComponent<BulletColisionEvent>().ShooterEvent = this;
            createdBullet.SetActive(false);
            despawnedBullets.Add(createdBullet);
        }
    }

    protected override void UpdateCollision()
    {
        if (despawnedBullets.Count != 0)
        {
            GameObject spawnedBullet = despawnedBullets[0];
            spawnedBullet.transform.position = gameObject.transform.position;
            spawnedBullet.SetActive(true);

            CostomizableMoveBehaviour moveBehaviour = spawnedBullet.GetComponent<CostomizableMoveBehaviour>();
            moveBehaviour.SetMoveDirection(ShootDirection);
            moveBehaviour.SetMoveSpeed(gameObjectAttributes.GetAttribute(AttributeNames.BULLET_SPEED));

            despawnedBullets.RemoveAt(0);
            spawnedBullets.Add(spawnedBullet);
        }
        else
        {
            GameObject spawnedBullet = spawnedBullets[0];
            spawnedBullet.transform.position = gameObject.transform.position;

            CostomizableMoveBehaviour moveBehaviour = spawnedBullet.GetComponent<CostomizableMoveBehaviour>();
            moveBehaviour.SetMoveDirection(ShootDirection);

            spawnedBullets.RemoveAt(0);
            spawnedBullets.Add(spawnedBullet);
        }

        audioSource.clip = shootSound;
        audioSource.Play();

        eventActivated = false;
    }

    public void DespawnBullet(GameObject despawnedBullet)
    {
        for (int i = 0; i < spawnedBullets.Count; ++i)
        {
            if (spawnedBullets[i] == despawnedBullet)
            {
                despawnedBullet.SetActive(false);
                despawnedBullets.Add(despawnedBullet);
                spawnedBullets.RemoveAt(i);
            }
        }
    }
}
