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

    public float ShootCooldown { get => enemyAttributes.GetAttribute(AttributeNames.SHOOT_COOLDOWN); set => enemyAttributes.SetAttributeSafe(AttributeNames.SHOOT_COOLDOWN, value); }
    public float MaxBullets { get => enemyAttributes.GetAttribute(AttributeNames.MAX_BULLETS); set => enemyAttributes.SetAttributeSafe(AttributeNames.MAX_BULLETS, value); }
    public float BulletSpeed { get => enemyAttributes.GetAttribute(AttributeNames.BULLET_SPEED); set => enemyAttributes.SetAttributeSafe(AttributeNames.BULLET_SPEED, value); }

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

        if (elapsedShootCooldown == ShootCooldown)
        {
            
            shootEvent.ShootDirection = Vector2.down;
            shootEvent.StartEvent();
            elapsedShootCooldown = 0f;
        }

        elapsedShootCooldown = Mathf.Min(elapsedShootCooldown + Time.deltaTime, ShootCooldown);
    }

}
