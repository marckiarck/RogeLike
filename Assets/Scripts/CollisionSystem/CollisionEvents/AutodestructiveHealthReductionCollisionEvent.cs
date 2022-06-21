using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutodestructiveHealthReductionCollisionEvent : HelthReduceCollisionEvent
{
    [SerializeField]
    private AudioClip destructionSound;

    private AudioSource audioSource;

    public void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    protected override void UpdateCollisionEvent()
    {
        base.UpdateCollisionEvent();

        if (destructionSound != null)
        {
            audioSource.clip = destructionSound;
            audioSource.Play();
        }

        Destroy(gameObject);
    }
}
