using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletUpgrade : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> bulletUpgradesPrefabs;
    private GameObject selectedBulletupgrade;

    private SpriteRenderer upgradeSprite;

    private void Awake()
    {
        int upgradeIndex = (int)Random.Range(0f, bulletUpgradesPrefabs.Count);
        selectedBulletupgrade = bulletUpgradesPrefabs[upgradeIndex];

        upgradeSprite = gameObject.GetComponent<SpriteRenderer>();

        upgradeSprite.sprite = selectedBulletupgrade.GetComponent<SpriteRenderer>().sprite;
    }
}
