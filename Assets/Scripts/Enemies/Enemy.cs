using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (DeathEvent))]
public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float health;
    [SerializeField]
    private float damage;
    [SerializeField]
    private float enemyActivationDelay;
    [SerializeField]
    private float upgradeTime;
    [SerializeField]
    private float speedUpgrade;

    private float updateElepsedTime;
    public static float currentSpeedUpgrade = 0f;

    protected AttributeSet enemyAttributes;

    public float Health { get => enemyAttributes.GetAttribute(AttributeNames.HEALTH); set => enemyAttributes.SetAttributeSafe(AttributeNames.HEALTH, value); }
    public float Speed { get => enemyAttributes.GetAttribute(AttributeNames.SPEED); set => enemyAttributes.SetAttributeSafe(AttributeNames.SPEED, value); }
    public float Damage { get => enemyAttributes.GetAttribute(AttributeNames.DAMAGE); set => enemyAttributes.SetAttributeSafe(AttributeNames.DAMAGE, value); }

    public virtual void Awake()
    {
        enemyAttributes = gameObject.GetComponent<AttributeSet>();
        InitializeAttributes();

        updateElepsedTime = 0f;
    }

    private void Start()
    {
        SeekBehaviour seekBehaviour = gameObject.GetComponent<SeekBehaviour>();
        if (seekBehaviour)
        {
            seekBehaviour.Objetive = GameManager.Instance.PlayerReferece;
        }
    }

    protected virtual void UpgradeAttributes()
    {
        currentSpeedUpgrade += speedUpgrade;
    }

    public virtual void Update()
    {
        enemyAttributes.SetAttributeSafe(AttributeNames.ACTIVATION_DELAY, enemyAttributes.GetAttribute(AttributeNames.ACTIVATION_DELAY) - Time.deltaTime);

        if (updateElepsedTime >= upgradeTime)
        {
            UpgradeAttributes();
            updateElepsedTime -= upgradeTime;
        }

        updateElepsedTime += Time.deltaTime;
    }

    protected virtual void InitializeAttributes()
    {
        if (enemyAttributes == null)
        {
            enemyAttributes = gameObject.GetComponent<AttributeSet>();
        }

        enemyAttributes.SetAttribute(AttributeNames.HEALTH, health);
        enemyAttributes.SetAttribute(AttributeNames.SPEED, speed + currentSpeedUpgrade);
        enemyAttributes.SetAttribute(AttributeNames.DAMAGE, damage);
        enemyAttributes.SetAttribute(AttributeNames.ACTIVATION_DELAY, enemyActivationDelay);
    }
}
