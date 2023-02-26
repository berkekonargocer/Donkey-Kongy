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
        private CapsuleCollider2D _playerCollider2D;

        Collider2D[] _collisionCheckResults = new Collider2D[4];

        #endregion

        #endregion



        #region Unity Methods

        #region Awake

        private void Awake()
        {
            SetComponents();
        }

        #endregion

        #region Fixed Update

        private void FixedUpdate()
        {
            CheckCollision();
        }

        #endregion

        #endregion


        #region Custom Private Methods

        private void SetComponents()
        {
            _playerCollider2D = GetComponent<CapsuleCollider2D>();
            _groundLayer = LayerMask.NameToLayer("Ground");
        }

        private void CheckCollision()
        {
            Vector2 collisionBoxSize = _playerCollider2D.bounds.size;
            collisionBoxSize.x /= _playerCollisionCheckSettings.ColliderXSizeOffset;
            collisionBoxSize.y += _playerCollisionCheckSettings.ColliderYSizeOffset;

            int collisionCheckHits = Physics2D.OverlapBoxNonAlloc(transform.position, collisionBoxSize, 0.0f, _collisionCheckResults);

            for (int i = 0; i < collisionCheckHits; i++)
            {
                GameObject hit = _collisionCheckResults[i].gameObject;

                if (hit.layer == _groundLayer && hit.transform.position.y < transform.position.y)
                {
                    _playerCollisionCheckSettings.IsGrounded = true;
                }
                else
                {
                    _playerCollisionCheckSettings.IsGrounded = false;
                }
            }
        }

        #endregion
    }
}