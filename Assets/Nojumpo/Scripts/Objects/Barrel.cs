using UnityEngine;
using UnityEngine.Pool;

namespace Nojumpo
{
    public class Barrel : MonoBehaviour
    {
        [Header("OBJECT POOLING")]
        private IObjectPool<Barrel> _barrelPool;

        [Header("COMPONENTS")]
        private Rigidbody2D _barrelRigidbody2D;

        [Header("BARREL ROLL SETTINGS")]
        [SerializeField] private float _barrellRollVelocity;


        // ------------------------ UNITY BUILT-IN METHODS ------------------------
        private void Awake() {
            SetComponents();
        }

        private void OnBecameInvisible() {
            _barrelPool?.Release(this);
        }

        private void OnCollisionEnter2D(Collision2D collision) {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                ApplyMomentum(collision.gameObject.transform.right, _barrellRollVelocity);
            }
        }


        // ------------------------ CUSTOM PRIVATE METHODS ------------------------
        private void SetComponents() {
            _barrelRigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void ApplyMomentum(Vector3 direction, float velocity) {
            _barrelRigidbody2D.velocity = direction * velocity;
        }


        // ------------------------ CUSTOM PUBLIC METHODS ------------------------
        public void SetPool(IObjectPool<Barrel> barrelPool) {
            _barrelPool = barrelPool;
        }
    }
}