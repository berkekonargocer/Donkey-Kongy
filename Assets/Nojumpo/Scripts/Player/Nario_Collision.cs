using Nojumpo.Interfaces;
using UnityEngine;
using Nojumpo.Managers;

namespace Nojumpo
{
    public class Nario_Collision : MonoBehaviour
    {
        #region Fields

        #region Audios

        [Header("Hiy By Deadly Object SFX")]
        [SerializeField] private AudioSource _deadlyObjectHitSFXSource;

        #endregion

        #endregion



        #region Unity Methods

        #region Collision Enter

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Deadly"))
            {
                _deadlyObjectHitSFXSource.Play();
                CinemachineCameraManager.Instance.ShakeCamera(1.0f, 0.2f);
                Player_Controller.OnPlayerDie.Invoke();
            }
        }

        #endregion

        #region Trigger Enter

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out ICollectable collectable))
            {
                collectable.Collect();
            }
        }

        #endregion

        #endregion
    }
}