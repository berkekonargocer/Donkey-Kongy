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

        #region Momentum
        [Header("Momentum")]

        [SerializeField] private float _barrellRollVelocity;

        #endregion

        #endregion



        #region Unity Methods

        #region Awake

        private void Awake() {
            SetComponents();
        }

        #endregion

        #region OnBecameInvisible

        private void OnBecameInvisible() {
            _barrelPool?.Release(this);
        }

        #endregion

        #region OnCollisionEnter2D

        private void OnCollisionEnter2D(Collision2D collision) {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                ApplyMomentum(collision.gameObject.transform.right, _barrellRollVelocity);
            }
        }

        #endregion

        #endregion

        #region Custom Private Methods

        private void SetComponents() {
            _barrelRigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void ApplyMomentum(Vector3 direction, float velocity) {
            _barrelRigidbody2D.velocity = direction * velocity;
        }

        #endregion

        #region Custom Public Methods

        public void SetPool(IObjectPool<Barrel> barrelPool) {
            _barrelPool = barrelPool;
        }

        #endregion
    }
}