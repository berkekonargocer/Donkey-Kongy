using Nojumpo.Managers;
using UnityEngine;

namespace Nojumpo
{
    public class BackgroundMusic : MonoBehaviour
    {
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

        private void Awake()
        {
            SetComponents();
        }

        #endregion

        #endregion


        #region Custom Private Methods

        private void SetComponents() {
            _bgmAudioSource = GetComponent<AudioSource>();
        }

        private void StopBGM(int timeScale) {
            _bgmAudioSource.Stop();
        }

        #endregion
    }
}