using Nojumpo.Interfaces;
using UnityEngine;

namespace Nojumpo
{
    public class Nario_Trigger : MonoBehaviour
    {
        #region Unity Methods

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out ICollectable collectable))
            {
                collectable.Collect();
            }
        }

        #endregion
    }
}