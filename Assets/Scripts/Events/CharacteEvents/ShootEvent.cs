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

    private Vector2 shootDirection;

    public Vector2 ShootDirection { get => shootDirection; set => shootDirection = value; }

    public void Awake()
    {
        spawnedBullets = new List<GameObject>();
        despawnedBullets = new List<GameObject>();
        audioSource = GetComponent<AudioSource>();
    }

    public void Start()
    {
        gameObjectAttributes = gameObject.GetComponent<AttributeSet>();

        for (int i = 0; i < gameObjectAttributes.GetAttribute(AttributeNames.MAX_BULLETS); ++i)
        {
            GameObject createdBullet = Instantiate<GameObject>(bulletPrefab);
            createdBullet.SetActive(false);
            despawnedBullets.Add(createdBullet);
        }
    }

    protected override void UpdateCollisionEvent()
    {
        if (despawnedBullets.Count != 0)
        {
            GameObject spawnedBullet = despawnedBullets[0];
            spawnedBullet.transform.position = gameObject.transform.position;
            spawnedBullet.SetActive(true);

            CostomizableMoveBehaviour moveBehaviour = spawnedBullet.GetComponent<CostomizableMoveBehaviour>();
            moveBehaviour.SetMoveDirection(ShootDirection);

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
}
