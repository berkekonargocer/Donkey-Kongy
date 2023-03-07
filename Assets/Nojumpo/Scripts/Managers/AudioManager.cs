using System;
using UnityEngine;

namespace Nojumpo.Managers
{
    public class AudioManager : MonoBehaviour
    {
        #region Instance

        private static AudioManager _instance;

        public static AudioManager Instance { get { return _instance; } }

        #endregion

        #region Fields



        #endregion



        #region Unity Methods

        #region Awake 

        private void Awake()
        {
            InitializeSingleton();
        }

        #endregion

        #endregion


        #region Custom Private Methods

        private void InitializeSingleton()
        {
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

        #endregion


        #region Custom Public Methods
        public void PlayAudio(AudioSource audioSource)
        {
            audioSource.Play();
        }

        public void PlayAudio(AudioSource audioSource, AudioClip audioClip)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }

        #endregion
    }
}