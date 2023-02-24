using UnityEngine;
using Nojumpo.Interfaces;
using System;

namespace Nojumpo
{
    public class Collectable : MonoBehaviour, ICollectable
    {
        #region Fields

        #region Collect Event

        public static Action<GameObject> CollectEvent;

        #endregion

        #region Sound Effects Settings
        [Header("Sound Effects Settings")]

        [SerializeField] private AudioSource _collectAudioSource;
        [SerializeField] private AudioClip _collectSFXAudio;

        #endregion


        #endregion



        #region Unity Methods

        #region OnEnable

        private void OnEnable()
        {
            CollectEvent += PlayCollectAudio;

        }

        #endregion

        #region OnDisable

        private void OnDisable()
        {
            CollectEvent -= PlayCollectAudio;
        }

        #endregion

        #region Awake and Start

        private void Awake()
        {

        }

        private void Start()
        {

        }

        #endregion

        #region Update

        private void Update()
        {

        }

        #endregion

        #endregion


        #region Custom Private Methods

        private void PlayCollectAudio(GameObject gameObject)
        {
            _collectAudioSource.clip = _collectSFXAudio;
            _collectAudioSource.Play();
        }

        #endregion

        #region Custom Public Methods

        public void Collect()
        {
            CollectEvent.Invoke(gameObject);
        }

        #endregion
    }
}