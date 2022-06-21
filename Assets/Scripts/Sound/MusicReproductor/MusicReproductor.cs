using UnityEngine;

namespace Platformer.Sound.MusicReproductor
{
    /// <summary>
    /// Plays a son in loop
    /// </summary>
    public class MusicReproductor : MonoBehaviour
    {
        protected AudioSource audioSource;

        protected virtual void Start()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.Play();
        }
    }
}

