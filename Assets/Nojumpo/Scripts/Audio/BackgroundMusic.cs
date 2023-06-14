using Nojumpo.Managers;
using UnityEngine;

namespace Nojumpo
{
    public class BackgroundMusic : MonoBehaviour
    {
        [Header("COMPONENTS")]
         AudioSource _bgmAudioSource;


        // ------------------------ UNITY BUILT-IN METHODS ------------------------
         void OnEnable() {
            GameManager.OnPlayerDie += StopBGM;
        }

         void OnDisable() {
            GameManager.OnPlayerDie -= StopBGM;
        }

         void Awake() {
            SetComponents();
        }


        // ------------------------ CUSTOM PRIVATE METHODS ------------------------
         void SetComponents() {
            _bgmAudioSource = GetComponent<AudioSource>();
        }

         void StopBGM(int timeScale, bool isDead) {
            _bgmAudioSource.Stop();
        }
    }
}