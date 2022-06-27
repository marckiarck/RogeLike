using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.gameOverPanel = this.gameObject;
        this.gameObject.SetActive(false);
    }

    public void Load(string level)
    {
        SceneManager.LoadScene(level);
    }
}
