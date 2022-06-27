using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsUpgrade : MonoBehaviour
{
    [SerializeField]
    private float healthUpgrade;
    [SerializeField]
    private float shootColdownUpgrade;
    [SerializeField]
    private float bulletSpeedUpgrade;
    [SerializeField]
    private AudioClip upgradeSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            AttributeSet playerAttributes = collision.gameObject.GetComponent<AttributeSet>();

            switch ((int)Random.Range(0f, 3f))
            {
                case 0:
                    playerAttributes.SetAttributeSafe(AttributeNames.HEALTH, playerAttributes.GetAttribute(AttributeNames.HEALTH) + healthUpgrade);
                    break;

                case 1:
                    playerAttributes.SetAttributeSafe(AttributeNames.HEALTH, playerAttributes.GetAttribute(AttributeNames.HEALTH) + 1f);
                    playerAttributes.SetAttributeSafe(AttributeNames.SHOOT_COOLDOWN, Mathf.Max(playerAttributes.GetAttribute(AttributeNames.SHOOT_COOLDOWN) - shootColdownUpgrade, 0.1f));
                    break;

                case 2:
                    playerAttributes.SetAttributeSafe(AttributeNames.HEALTH, playerAttributes.GetAttribute(AttributeNames.HEALTH) + 1f);
                    playerAttributes.SetAttributeSafe(AttributeNames.BULLET_SPEED, Mathf.Min(playerAttributes.GetAttribute(AttributeNames.BULLET_SPEED) + bulletSpeedUpgrade, 25f));
                    break;
            }
            
        }

        GameManager.Instance.PlayGameSound(upgradeSound);
        Destroy(gameObject);
    }
}
