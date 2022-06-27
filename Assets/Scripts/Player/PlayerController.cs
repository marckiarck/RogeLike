using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (ShootEvent))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float health;
    [SerializeField]
    private float damage;
    [SerializeField]
    private float shootCooldown;
    [SerializeField]
    private float maxBullets;
    [SerializeField]
    private float bulletSpeed;

    private Vector2 movingDirection = Vector2.zero;
    private Vector2 shootingDirection = Vector2.zero;
    private AttributeSet playerAttributes;
    private ShootEvent shootEvent;

    private Collider2D myCollider;
    private SpriteRenderer myRenderer;

    private float elapsedShootCooldown;

    public float Health { get => playerAttributes.GetAttribute(AttributeNames.HEALTH); set => playerAttributes.SetAttributeSafe(AttributeNames.HEALTH, value); }
    public float Speed { get => playerAttributes.GetAttribute(AttributeNames.SPEED); set => playerAttributes.SetAttributeSafe(AttributeNames.SPEED, value); }
    public float Damage { get => playerAttributes.GetAttribute(AttributeNames.DAMAGE); set => playerAttributes.SetAttributeSafe(AttributeNames.DAMAGE, value); }
    public float ShootCooldown { get => playerAttributes.GetAttribute(AttributeNames.SHOOT_COOLDOWN); set => playerAttributes.SetAttributeSafe(AttributeNames.SHOOT_COOLDOWN, value); }
    public float MaxBullets { get => playerAttributes.GetAttribute(AttributeNames.MAX_BULLETS); set => playerAttributes.SetAttributeSafe(AttributeNames.MAX_BULLETS, value); }
    public float BulletSpeed { get => playerAttributes.GetAttribute(AttributeNames.BULLET_SPEED); set => playerAttributes.SetAttributeSafe(AttributeNames.BULLET_SPEED, value); }

    private void Awake()
    {
        playerAttributes = gameObject.GetComponent<AttributeSet>();
        InitializeAttributes();

        shootEvent = gameObject.GetComponent<ShootEvent>();

        elapsedShootCooldown = ShootCooldown;

        myCollider = GetComponent<Collider2D>();
        myRenderer = GetComponent<SpriteRenderer>();
    }

    private void InitializeAttributes()
    {
        playerAttributes.SetAttribute(AttributeNames.HEALTH, health);
        playerAttributes.SetAttribute(AttributeNames.SPEED, speed);
        playerAttributes.SetAttribute(AttributeNames.DAMAGE, damage);
        playerAttributes.SetAttribute(AttributeNames.SHOOT_COOLDOWN, shootCooldown);
        playerAttributes.SetAttribute(AttributeNames.MAX_BULLETS, maxBullets);
        playerAttributes.SetAttribute(AttributeNames.BULLET_SPEED, bulletSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInput();
        if (playerAttributes.GetAttribute(AttributeNames.HEALTH) > 0f)
        {
            Move();
        }
        else 
        {
            GameManager.Instance.EndGame();
            this.enabled = false;
        }

        AproachShoot();
    }

    private void AproachShoot()
    {
        if (shootingDirection != Vector2.zero && elapsedShootCooldown == ShootCooldown)
        {
            shootEvent.ShootDirection = shootingDirection;
            shootEvent.StartEvent();
            elapsedShootCooldown = 0f;
        }

        elapsedShootCooldown = Mathf.Min(elapsedShootCooldown + Time.deltaTime, ShootCooldown);
    }

    private void Move()
    {

        RaycastHit2D[] hits = Physics2D.BoxCastAll(gameObject.transform.position, myCollider.bounds.size, 0f, new Vector2(movingDirection.x, 0f), (speed * Time.deltaTime) * 5f);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && (hit.collider.gameObject.tag == "Wall"))
            {
                movingDirection = new Vector2(0f, movingDirection.y);
                break;
            }
        }

        hits = Physics2D.BoxCastAll(gameObject.transform.position, myCollider.bounds.size, 0f, new Vector2(0f, movingDirection.y), (speed * Time.deltaTime) * 5f);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && (hit.collider.gameObject.tag == "Wall"))
            {
                movingDirection = new Vector2(movingDirection.x, 0f);
                break;
            }
        }

        transform.position += (Vector3)movingDirection * speed * Time.deltaTime;

    }

    private void UpdateInput()
    {
        UpdateMovingDirection();
        UpdateShootingDirection();

        if (movingDirection.x > 0f)
        {
            myRenderer.flipX = true;
        }else if (movingDirection.x < 0f)
        {
            myRenderer.flipX = false;
        }
    }

    private void UpdateShootingDirection()
    {
        shootingDirection = new Vector2(Input.GetAxisRaw("ShootX"), shootingDirection.y);
        shootingDirection = new Vector2(shootingDirection.x, Input.GetAxisRaw("ShootY"));
    }

    private void UpdateMovingDirection()
    {
        movingDirection = new Vector2(Input.GetAxis("Horizontal"), movingDirection.y);
        movingDirection = new Vector2(movingDirection.x, Input.GetAxis("Vertical"));
    }
}
