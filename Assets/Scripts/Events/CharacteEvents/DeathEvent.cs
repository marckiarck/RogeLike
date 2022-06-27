using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEvent : ConditionalEvents
{
    protected AttributeSet gameObjectAttributes;

    [SerializeField] GameObject particles;
    bool aux = false;

    [SerializeField]
    private AudioClip deathSound;
    private AudioSource audioSource;
    void Start()
    {
        //Searches gameObjectAttributes on Start beacuse the attribute set may be created in an Awake
        gameObjectAttributes = gameObject.GetComponent<AttributeSet>();
        audioSource = gameObject.GetComponent<AudioSource>();
        
    }

    protected override void UpdateEvent()
    {
        if (audioSource.isPlaying == false)
        {
            audioSource.PlayOneShot(deathSound);

            Collider2D myCollider = gameObject.GetComponent<Collider2D>();
            myCollider.enabled = false;
        }

        if (particles != null && !aux)
        {
            print("a");
            GameObject p = Instantiate(particles, this.transform.position, Quaternion.identity);
            Destroy(p, 2);
            aux = true;
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
