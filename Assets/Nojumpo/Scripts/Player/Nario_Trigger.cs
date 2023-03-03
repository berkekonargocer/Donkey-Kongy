using Nojumpo.Interfaces;
using UnityEngine;

namespace Nojumpo
{
    public class Nario_Trigger : MonoBehaviour
    {
        #region Unity Methods

        #region Trigger Enter

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out ICollectable collectable))
            {
                collectable.Collect();
            }
            if (collision.gameObject.layer == LayerMask.NameToLayer("Deadly"))
            {
                Player_Controller.OnPlayerDie.Invoke();
            }
        }

        #endregion

        #endregion
    }
}