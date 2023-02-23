using UnityEngine;
using Nojumpo.Interfaces;

namespace Nojumpo
{
    public class Collectible : MonoBehaviour, ICollectible
    {
        #region Fields

        [SerializeField] private AudioSource _collectAudioSource;
        [SerializeField] private AudioClip _collectSFXAudio;

        #endregion



        #region Unity Methods

        #region OnEnable

        private void OnEnable()
        {

        }

        #endregion

        #region OnDisable

        private void OnDisable()
        {

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



        #endregion

        #region Custom Public Methods

        public void Collect()
        {
            _collectAudioSource.clip = _collectSFXAudio;
            _collectAudioSource.Play();
            // Show object in the UI
            Destroy(gameObject);
        }

        #endregion
    }
}