using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer.Sound
{
    /// <summary>
    /// Class that contains the methods called by buttons of the main menu
    /// </summary>
    public class MenuManager : MonoBehaviour
    {
        private const float WAIT_BEFORE_CHANGE_SCENE = 1f;
        private string sceneName;
        public AudioClip changeSceneAudio;
        private AudioSource audioSource;

        [SerializeField] GameObject menuPanel;
        [SerializeField] GameObject optionsPanel;
        [SerializeField] GameObject controlsPanel;

        private void Awake()
        {
            sceneName = null;
            audioSource = GetComponent<AudioSource>();
        }

        /// <summary>
        /// If there is a scene waiting to be changed and changeSceneAudio has been repreduce changes scene
        /// </summary>
        private void Update()
        {
            if(sceneName != null)
            {
                if(audioSource.isPlaying == false)
                {
                    SceneManager.LoadScene(sceneName);
                }
            }
        }

        public void Options(bool active)
        {
            menuPanel.SetActive(!active);
            optionsPanel.SetActive(active);
        }

        public void Controls(bool active)
        {
            menuPanel.SetActive(!active);
            controlsPanel.SetActive(active);
        }

        /// <summary>
        /// Tells MenuMaanager which scene change to
        /// </summary>
        /// <param name="scene"></param>
        public void LoadScene(string scene)
        {
            sceneName = scene;
            audioSource.PlayOneShot(changeSceneAudio);
        }

        public void QuitAplication()
        {
            Application.Quit();
        }
    }
}

