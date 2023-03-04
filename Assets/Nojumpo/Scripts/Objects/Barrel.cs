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

        #region Components

        private Rigidbody2D _barrelRigidbody2D;

        #endregion

        #region First Momentum
        [Header("First Momentum")]

        [SerializeField] private float _firstMomentum;

        #endregion

        #endregion



        #region Unity Methods

        #region Awake

        private void Awake()
        {
            SetComponents();
        }

        #endregion

        #region OnBecameInvisible

        private void OnBecameInvisible()
        {
            _barrelPool?.Release(this);
        }

        private void OnBecameVisible()
        {
            ApplyMomentum();
        }

        #endregion

        #endregion

        #region Custom Private Methods

        private void SetComponents()
        {
            _barrelRigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void ApplyMomentum()
        {
            _barrelRigidbody2D.AddForce(Vector2.right * _firstMomentum);
        }

        #endregion

        #region Custom Public Methods

        public void SetPool(IObjectPool<Barrel> barrelPool)
        {
            _barrelPool = barrelPool;
        }

        #endregion
    }
}