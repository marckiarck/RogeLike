using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    [SerializeField] GameObject particles;

    private void OnDestroy()
    {
        GameObject p = Instantiate(particles, this.transform.position, Quaternion.identity);
        Destroy(p, 2);
        GameManager.score += 10;
    }
}
