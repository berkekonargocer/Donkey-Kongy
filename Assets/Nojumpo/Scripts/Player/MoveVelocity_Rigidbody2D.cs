using Nojumpo.Interfaces;
using UnityEngine;

namespace Nojumpo
{
    public class MoveVelocity_Rigidbody2D : MonoBehaviour, IMoveVelocity2D
    {
        [Header("COMPONENTS")]
        private Rigidbody2D _rigidbody2D;
        private Animator _animator;

        [Header("MOVE VELOCITY SETTINGS")]
        private Vector2 _moveVelocity = Vector2.zero;


        // ------------------------ UNITY BUILT-IN METHODS ------------------------
        private void Awake() {
            SetComponents();
        }

        private void FixedUpdate() {
            ApplyPlayerMovement();
        }


        // ------------------------ CUSTOM PRIVATE METHODS ------------------------
        private void SetComponents() {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        private void ApplyPlayerMovement() {
            _rigidbody2D.MovePosition(_rigidbody2D.position + _moveVelocity * Time.fixedDeltaTime);
            _animator.SetFloat("MoveX", _moveVelocity.x);
            _animator.SetFloat("MoveY", _moveVelocity.y);
        }


        // ------------------------ CUSTOM PUBLIC METHODS ------------------------
        public Vector2 GetVelocity() {
            return _moveVelocity;
        }

        public void SetVelocity(Vector2 moveVelocity) {
            _moveVelocity = moveVelocity;
        }

        public void VelocityPlusEquals(Vector2 moveVelocity) {
            _moveVelocity += moveVelocity;
        }

        public void SetVelocityX(float moveVelocityX) {
            _moveVelocity.x = moveVelocityX;
        }

        public void SetVelocityY(float moveVelocityY) {
            _moveVelocity.y = moveVelocityY;
        }

        public void MultiplyVelocityX(float moveVelocityX) {
            _moveVelocity.x *= moveVelocityX;
        }

        public void MultiplyVelocityY(float moveVelocityY) {
            _moveVelocity.y *= moveVelocityY;
        }
    }
}