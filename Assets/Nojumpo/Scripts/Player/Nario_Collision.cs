using Nojumpo.Interfaces;
using UnityEngine;

namespace Nojumpo
{
    public class Nario_Collision : MonoBehaviour
    {
        #region Unity Methods

        #region Collision Enter

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Deadly"))
            {
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