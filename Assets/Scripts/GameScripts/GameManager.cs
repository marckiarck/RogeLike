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

    public static float score = 0f;

    public GameObject PlayerReferece { get => playerReferece;}

    public override void Awake()
    {
        base.Awake();

        playerReferece = Instantiate(playerPrefab);
        playerReferece.transform.position = Vector2.zero;

        playerAttributes = playerReferece.GetComponent<AttributeSet>();

        score = 0f;
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
}
