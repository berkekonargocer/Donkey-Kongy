using Nojumpo.Interfaces;
using UnityEngine;
using Nojumpo.Managers;

namespace Nojumpo
{
    public class Player_Collision : MonoBehaviour
    {
        [Header("COMPONENTS")]
        [SerializeField]  AudioSource _deadlyObjectHitSFXSource;


        // ------------------------ UNITY BUILT-IN METHODS ------------------------
         void OnCollisionEnter2D(Collision2D collision) {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Deadly"))
            {
                CinemachineCamera cinemachineCamera = GameObject.Find("Cinemachine Virtual Camera 1").GetComponent<CinemachineCamera>();
                cinemachineCamera.ShakeCamera(3f, 1.0f);

                _deadlyObjectHitSFXSource.Play();
                GameManager.OnPlayerDie.Invoke(0, true);
            }
        }

         void OnTriggerEnter2D(Collider2D collision) {
            if (collision.gameObject.TryGetComponent(out ICollectable collectable))
            {
                collectable.Collect();
            }
        }
    }
}