using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthText : MonoBehaviour
{
    private Text healthText;

    private void Awake()
    {
        healthText = GetComponent<Text>();
    }

    private void Update()
    {
        healthText.text = ((int)(GameManager.Instance.GetPlayerLife())).ToString();
    }
}
