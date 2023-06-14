using UnityEngine;

namespace Nojumpo.Managers
{
    public class AudioManager : MonoBehaviour
    {
        [Header("SINGLETON")]
         static AudioManager _instance;
        public static AudioManager Instance { get { return _instance; } }


        // ------------------------ UNITY BUILT-IN METHODS ------------------------
         void Awake() {
            InitializeSingleton();
        }


        // ------------------------ CUSTOM PRIVATE METHODS ------------------------
         void InitializeSingleton() {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }


        // ------------------------ CUSTOM PUBLIC METHODS ------------------------
        public void PlayAudio(AudioSource audioSource) {
            audioSource.Play();
        }

        public void PlayAudio(AudioSource audioSource, AudioClip audioClip) {
            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }
}