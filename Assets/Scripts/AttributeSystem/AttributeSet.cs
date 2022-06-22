using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AttributeNames
{
    public static string HEALTH = "Health";
    public static string DAMAGE = "Damage";
    public static string SPEED = "Speed";
    public static string SHOOT_COOLDOWN = "ShootCooldown";
    public static string MAX_BULLETS = "MaxBullets";
    public static string BULLET_SPEED = "BulletSpeed";
}

public class AttributeSet : MonoBehaviour
{
    [SerializeField]
    private Dictionary<string, float> attributes = null;

    private void Awake()
    {
        if (attributes == null)
        {
            attributes = new Dictionary<string, float>();
        }
    }

    public float GetAttribute(string attributeName)
    {
        if (attributes.ContainsKey(attributeName) == false)
        {
            return 0f;
        }
        return attributes[attributeName];
    }

    public void SetAttribute(string attributeName, float value)
    {
        if (attributes == null)
        {
            attributes = new Dictionary<string, float>();
        }

        attributes[attributeName] = value;
    }

    public void SetAttributeSafe(string attributeName, float value)
    {
        if (attributes == null)
        {
            attributes = new Dictionary<string, float>();
        }

        if (attributes.ContainsKey(attributeName))
        {
            attributes[attributeName] = value;
        }
    }
}
