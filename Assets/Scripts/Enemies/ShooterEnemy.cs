using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemy : Enemy
{
    [SerializeField]
    private float shootCooldown;
    [SerializeField]
    private float maxBullets;
    [SerializeField]
    private float bulletSpeed;

    private bool shootAbble = true;
    private float elapsedShootCooldown;
    private ShootEvent shootEvent;

    private GameObject player;
    private Collider2D myCollider;

    public float ShootCooldown { get => enemyAttributes.GetAttribute(AttributeNames.SHOOT_COOLDOWN); set => enemyAttributes.SetAttributeSafe(AttributeNames.SHOOT_COOLDOWN, value); }
    public float MaxBullets { get => enemyAttributes.GetAttribute(AttributeNames.MAX_BULLETS); set => enemyAttributes.SetAttributeSafe(AttributeNames.MAX_BULLETS, value); }
    public float BulletSpeed { get => enemyAttributes.GetAttribute(AttributeNames.BULLET_SPEED); set => enemyAttributes.SetAttributeSafe(AttributeNames.BULLET_SPEED, value); }

    public override void Awake()
    {
        base.Awake();

        player = GameManager.Instance.PlayerReferece;
        myCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        AproachShoot();

    }

    protected override void InitializeAttributes()
    {
        base.InitializeAttributes();

        enemyAttributes.SetAttribute(AttributeNames.SHOOT_COOLDOWN, shootCooldown);
        enemyAttributes.SetAttribute(AttributeNames.MAX_BULLETS, maxBullets);
        enemyAttributes.SetAttribute(AttributeNames.BULLET_SPEED, bulletSpeed);
    }

    private void AproachShoot()
    {

        if (shootEvent == null)
        {
            shootEvent = gameObject.GetComponent<ShootEvent>();
        }

        Vector2 distanceToPlayer;
        bool hittedPlayer = false;
        if (player)
        {
            distanceToPlayer = (GameManager.Instance.PlayerReferece.transform.position - gameObject.transform.position);
            RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position + (Vector3)distanceToPlayer.normalized * myCollider.bounds.size.magnitude * 1.1f, distanceToPlayer.normalized, 25f);
            if (hit.collider && hit.collider.gameObject.tag == "Player")
            {
                hittedPlayer = true;
            }
        }

        if (elapsedShootCooldown == ShootCooldown && hittedPlayer)
        {

            if (GameManager.Instance.PlayerReferece == null)
            {
                return;
            }

            shootEvent.ShootDirection = (GameManager.Instance.PlayerReferece.transform.position - gameObject.transform.position).normalized;
            shootEvent.StartEvent();
            elapsedShootCooldown = 0f;
        }

        elapsedShootCooldown = Mathf.Min(elapsedShootCooldown + Time.deltaTime, ShootCooldown);
    }

}
