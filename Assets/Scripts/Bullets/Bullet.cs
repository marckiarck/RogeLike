using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float health;
    [SerializeField]
    private float damage;

    private AttributeSet enemyAttributes;

    public float Health { get => enemyAttributes.GetAttribute(AttributeNames.HEALTH); set => enemyAttributes.SetAttributeSafe(AttributeNames.HEALTH, value); }
    public float Speed { get => enemyAttributes.GetAttribute(AttributeNames.SPEED); set => enemyAttributes.SetAttributeSafe(AttributeNames.SPEED, value); }
    public float Damage { get => enemyAttributes.GetAttribute(AttributeNames.DAMAGE); set => enemyAttributes.SetAttributeSafe(AttributeNames.DAMAGE, value); }

    private void Awake()
    {
        enemyAttributes = gameObject.AddComponent<AttributeSet>();
        InitializeAttributes();
    }

    private void InitializeAttributes()
    {
        enemyAttributes.SetAttribute(AttributeNames.HEALTH, health);
        enemyAttributes.SetAttribute(AttributeNames.SPEED, speed);
        enemyAttributes.SetAttribute(AttributeNames.DAMAGE, damage);
    }
}
