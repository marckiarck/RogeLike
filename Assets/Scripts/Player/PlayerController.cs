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

    private Vector2 movingDirection = Vector2.zero;
    private Vector2 shootingDirection = Vector2.zero;
    private AttributeSet playerAttributes;
    private ShootEvent shootEvent;

    private float elapsedShootCooldown;

    public float Health { get => playerAttributes.GetAttribute(AttributeNames.HEALTH); set => playerAttributes.SetAttributeSafe(AttributeNames.HEALTH, value); }
    public float Speed { get => playerAttributes.GetAttribute(AttributeNames.SPEED); set => playerAttributes.SetAttributeSafe(AttributeNames.SPEED, value); }
    public float Damage { get => playerAttributes.GetAttribute(AttributeNames.DAMAGE); set => playerAttributes.SetAttributeSafe(AttributeNames.DAMAGE, value); }
    public float ShootCooldown { get => playerAttributes.GetAttribute(AttributeNames.SHOOT_COOLDOWN); set => playerAttributes.SetAttributeSafe(AttributeNames.SHOOT_COOLDOWN, value); }
    public float MaxBullets { get => playerAttributes.GetAttribute(AttributeNames.MAX_BULLETS); set => playerAttributes.SetAttributeSafe(AttributeNames.MAX_BULLETS, value); }

    private void Awake()
    {
        playerAttributes = gameObject.GetComponent<AttributeSet>();
        InitializeAttributes();

        shootEvent = gameObject.GetComponent<ShootEvent>();

        elapsedShootCooldown = ShootCooldown;
    }

    private void InitializeAttributes()
    {
        Health = health;
        Speed = speed;
        Damage = damage;
        playerAttributes.SetAttribute(AttributeNames.SHOOT_COOLDOWN, shootCooldown);
        playerAttributes.SetAttribute(AttributeNames.MAX_BULLETS, maxBullets);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInput();
        Move();
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
        transform.position += (Vector3)movingDirection * speed * Time.deltaTime;
    }

    private void UpdateInput()
    {
        UpdateMovingDirection();
        UpdateShootingDirection();
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
