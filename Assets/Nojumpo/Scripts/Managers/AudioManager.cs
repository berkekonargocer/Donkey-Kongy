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

        private AudioSource _bgmAudioSource;

        #endregion



        #region Unity Methods

        #region OnEnable

        private void OnEnable() {
            GameManager.OnPlayerDie += StopBGM;
        }

        #endregion

        #region OnDisable

        private void OnDisable() {
            GameManager.OnPlayerDie -= StopBGM;
        }

        #endregion

        #region Awake 

        private void Awake() {
            InitializeSingleton();
            SetComponents();
        }

        #endregion

        #endregion


        #region Custom Private Methods

        private void InitializeSingleton() {
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

        private void SetComponents() {
            _bgmAudioSource = GetComponent<AudioSource>();
        }

        private void StopBGM(int timeScale) {
            _bgmAudioSource.Stop();
        }

        #endregion

        #region Custom Public Methods

        public void PlayAudio(AudioSource audioSource) {
            audioSource.Play();
        }

        public void PlayAudio(AudioSource audioSource, AudioClip audioClip) {
            audioSource.clip = audioClip;
            audioSource.Play();
        }

        #endregion
    }
}