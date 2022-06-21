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
}

public class AttributeSet : MonoBehaviour
{
    [SerializeField]
    private Dictionary<string, float> attributes;

    private void Awake()
    {
        attributes = new Dictionary<string, float>();
        InitializeAttributes();
    }

    public void InitializeAttributes()
    {
        attributes[AttributeNames.HEALTH] = 5;
        attributes[AttributeNames.DAMAGE] = 1;
        attributes[AttributeNames.SPEED] = 2;
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
        attributes[attributeName] = value;
    }

    public void SetAttributeSafe(string attributeName, float value)
    {
        if (attributes.ContainsKey(attributeName))
        {
            attributes[attributeName] = value;
        }
    }
}
