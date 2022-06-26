using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBulletVariant : BulletVariant
{
    [SerializeField]
    private float speedUpgrade;

    public override void ChangePlayerAttributes()
    {
        AttributeSet playerAttributes = GameManager.Instance.GetPlayerAttributes();

        playerAttributes.SetAttributeSafe(AttributeNames.BULLET_SPEED, playerAttributes.GetAttribute(AttributeNames.BULLET_SPEED) + speedUpgrade);
    }
}
