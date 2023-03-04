using UnityEngine;
using UnityEngine.Pool;

namespace Nojumpo
{
    public class Barrel : MonoBehaviour
    {
        #region Fields

        #region Object Pooling

        private IObjectPool<Barrel> _barrelPool;

        #endregion

        #endregion



        #region Unity Methods

        #region OnBecameInvisible

        private void OnBecameInvisible()
        {
            _barrelPool?.Release(this);
        }

        #endregion

        #endregion

        #region Custom Public Methods

        public void SetPool(IObjectPool<Barrel> barrelPool)
        {
            _barrelPool = barrelPool;
        }

        #endregion
    }
}