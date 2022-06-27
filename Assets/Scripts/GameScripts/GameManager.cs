using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class GameManager : TemporalSingleton<GameManager>
{
    [SerializeField]
    private GameObject playerPrefab;
    private GameObject playerReferece;
    private AttributeSet playerAttributes;
    private AudioSource gameAudioSource;

    public GameObject gameOverPanel;
    public static float score = 0f;

    public GameObject PlayerReferece { get => playerReferece;}

    public override void Awake()
    {
        base.Awake();

        playerReferece = Instantiate(playerPrefab);
        playerReferece.transform.position = new Vector2(12.5f, 12.5f);

        playerAttributes = playerReferece.GetComponent<AttributeSet>();

        gameAudioSource = gameObject.AddComponent<AudioSource>();

        score = 0f;

        Enemy.currentSpeedUpgrade = 0f;
        ShooterEnemy.currentBulletSpeedUpgrade = 0f;
        ShooterEnemy.currentShootCooldownUpgrade = 0f;
    }


    public void EndGame()
    {
        if(gameOverPanel!=null)
        gameOverPanel.SetActive(true);
    }

    public AttributeSet GetPlayerAttributes()
    {
        return playerAttributes;
    }

    public float GetPlayerLife()
    {
        if (playerAttributes)
        {
            return playerAttributes.GetAttribute(AttributeNames.HEALTH);
        }
        else
        {
            return 0f;
        }
    }

    public void PlayGameSound(AudioClip clip)
    {
        gameAudioSource.PlayOneShot(clip);
    }
}
