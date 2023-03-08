using Nojumpo.Interfaces;
using UnityEngine;
using Nojumpo.Managers;

namespace Nojumpo
{
    public class Player_Collision : MonoBehaviour
    {
        #region Fields

        #region Audios

        [Header("Hiy By Deadly Object SFX")]
        [SerializeField] private AudioSource _deadlyObjectHitSFXSource;

        #endregion

        #endregion



        #region Unity Methods

        #region Collision Enter

        private void OnCollisionEnter2D(Collision2D collision) {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Deadly"))
            {
                CinemachineCamera cinemachineCamera = FindObjectOfType<CinemachineCamera>();
                cinemachineCamera.ShakeCamera(3f, 1.0f);

                _deadlyObjectHitSFXSource.Play();
                GameManager.OnPlayerDie.Invoke(0);
            }
        }

        #endregion

        #region Trigger Enter

        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.gameObject.TryGetComponent(out ICollectable collectable))
            {
                collectable.Collect();
            }
        }

        #endregion

        #endregion
    }
}