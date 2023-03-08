using Nojumpo.ScriptableObjects;
using UnityEngine;

namespace Nojumpo
{
    public class Player_CheckCollision : MonoBehaviour
    {
        #region Fields

        #region Collision Check Settings

        [Header("Collision Check Settings")]

        [SerializeField] private CollisionCheckSettings _playerCollisionCheckSettings;

        private LayerMask _groundLayer;
        private LayerMask _ladderLayer;
        private LayerMask _playerLayer;
        private CapsuleCollider2D _playerCollider2D;

        Collider2D[] _collisionCheckResults = new Collider2D[4];

        private bool _isGrounded = true;

        #endregion

        #region Animator Settings

        private Animator _playerAnimator;

        #endregion

        #endregion



        #region Unity Methods

        #region Awake

        private void Awake() {
            SetComponents();
        }

        #endregion

        #region Fixed Update

        private void FixedUpdate() {
            CheckCollision();
        }

        #endregion

        #endregion


        #region Custom Private Methods

        private void SetComponents() {
            _playerCollider2D = GetComponent<CapsuleCollider2D>();
            _playerAnimator = GetComponent<Animator>();
            _groundLayer = LayerMask.NameToLayer("Ground");
            _ladderLayer = LayerMask.NameToLayer("Ladder");
            _playerLayer = LayerMask.NameToLayer("Player");
        }

        private void CheckCollision() {
            _playerCollisionCheckSettings.IsClimbing = false;
            _isGrounded = false;

            Vector2 collisionBoxSize = _playerCollider2D.bounds.size;
            collisionBoxSize.x /= _playerCollisionCheckSettings.ColliderXSizeOffset;
            collisionBoxSize.y += _playerCollisionCheckSettings.ColliderYSizeOffset;

            int ignoreSelfLayer = _playerLayer.value;
            int collisionCheckHits = Physics2D.OverlapBoxNonAlloc(transform.position, collisionBoxSize, 0.0f, _collisionCheckResults, ~ignoreSelfLayer);

            for (int i = 0; i < collisionCheckHits; i++)
            {
                GameObject hit = _collisionCheckResults[i].gameObject;

                if (hit.layer == _groundLayer)
                {
                    _isGrounded = hit.transform.position.y < transform.position.y + (-0.3f);
                    Physics2D.IgnoreCollision(_playerCollider2D, _collisionCheckResults[i], !_isGrounded);
                }
                else if (hit.layer == _ladderLayer)
                {
                    _playerCollisionCheckSettings.IsClimbing = true;
                }
            }

            _playerCollisionCheckSettings.IsGrounded = _isGrounded;
            _playerAnimator.SetBool("IsClimbing", _playerCollisionCheckSettings.IsClimbing);
            _playerAnimator.SetBool("IsGrounded", _playerCollisionCheckSettings.IsGrounded);
        }

        #endregion
    }
}