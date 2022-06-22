using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class GameManager : TemporalSingleton<GameManager>
{
    [SerializeField]
    private GameObject playerPrefab;
    private GameObject playerReferece;

    public static float score = 0f;

    public GameObject PlayerReferece { get => playerReferece;}

    public override void Awake()
    {
        base.Awake();

        playerReferece = Instantiate(playerPrefab);
        playerReferece.transform.position = Vector2.zero;

        score = 0f;
    }
}
