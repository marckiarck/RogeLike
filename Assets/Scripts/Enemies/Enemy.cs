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

    protected AttributeSet enemyAttributes;

    public float Health { get => enemyAttributes.GetAttribute(AttributeNames.HEALTH); set => enemyAttributes.SetAttributeSafe(AttributeNames.HEALTH, value); }
    public float Speed { get => enemyAttributes.GetAttribute(AttributeNames.SPEED); set => enemyAttributes.SetAttributeSafe(AttributeNames.SPEED, value); }
    public float Damage { get => enemyAttributes.GetAttribute(AttributeNames.DAMAGE); set => enemyAttributes.SetAttributeSafe(AttributeNames.DAMAGE, value); }

    private void Awake()
    {
        enemyAttributes = gameObject.GetComponent<AttributeSet>();
        InitializeAttributes();
    }

    protected virtual void InitializeAttributes()
    {
        if (enemyAttributes == null)
        {
            enemyAttributes = gameObject.GetComponent<AttributeSet>();
        }

        Health = health;
        Speed = speed;
        Damage = damage;
    }
}
