using Nojumpo.Managers;
using UnityEngine;

namespace Nojumpo
{
    public class BackgroundMusic : MonoBehaviour
    {
        [Header("COMPONENTS")]
        private AudioSource _bgmAudioSource;


        // ------------------------ UNITY BUILT-IN METHODS ------------------------
        private void OnEnable() {
            GameManager.OnPlayerDie += StopBGM;
        }

        private void OnDisable() {
            GameManager.OnPlayerDie -= StopBGM;
        }

        private void Awake() {
            SetComponents();
        }


        // ------------------------ CUSTOM PRIVATE METHODS ------------------------
        private void SetComponents() {
            _bgmAudioSource = GetComponent<AudioSource>();
        }

        private void StopBGM(int timeScale, bool isDead) {
            _bgmAudioSource.Stop();
        }
    }
}