using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEvent : ConditionalEvents
{
    private AttributeSet gameObjectAttributes;

    [SerializeField]
    private AudioClip deathSound;
    private AudioSource audioSource;
    void Start()
    {
        //Searches gameObjectAttributes on Start beacuse the attribute set may be created in an Awake
        gameObjectAttributes = gameObject.GetComponent<AttributeSet>();
        audioSource = gameObject.GetComponent<AudioSource>();
        
    }

    protected override void UpdateCollision()
    {
        if (audioSource.isPlaying == false)
        {
            audioSource.PlayOneShot(deathSound);
        }

        Destroy(gameObject, deathSound.length);
        eventActivated = false;
    }

    public override bool CheckCondition()
    {
        float health = gameObjectAttributes.GetAttribute(AttributeNames.HEALTH);

        if (health <= 0f)
        {
            return true;
        }

        return false;
    }
}
