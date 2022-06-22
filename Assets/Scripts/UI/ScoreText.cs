using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    private Text scoreText;

    private void Awake()
    {
        scoreText = GetComponent<Text>();
    }

    private void Update()
    {
        scoreText.text = ((int)(GameManager.score)).ToString();
    }
}
