using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        GameObject player = GameManager.Instance.PlayerReferece;
        if (player)
        {
            gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, gameObject.transform.position.z);
        }
    }
}
