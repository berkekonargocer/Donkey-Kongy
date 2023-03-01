using Nojumpo.Interfaces;
using Nojumpo.ScriptableObjects;
using UnityEngine;

namespace Nojumpo
{
    public class Player_Animations : MonoBehaviour
    {
        #region Fields

        #region Movement Settings SO
        [Header("Movement Settings SO")]

        [SerializeField] private MovementSettings _playerMovementSettings;

        #endregion

        #region Animation Settings
        [Header("Animation Settings")]

        [SerializeField] private Sprite _climbingSprite;
        [SerializeField] private Sprite _jumpingSprite;
        [SerializeField] private Sprite[] _runningSprites;

        private SpriteRenderer _playerSpriteRenderer;
        private IMoveVelocity2D _playerMoveVelocity;

        private int _spriteIndex;

        #endregion

        #endregion



        #region Unity Methods

        #region OnEnable

        private void OnEnable()
        {
            InvokeRepeating(nameof(AnimateSprite), 1.0f / 12.0f, 1.0f / 12.0f);
        }

        #endregion

        #region OnDisable

        private void OnDisable()
        {
            CancelInvoke();
        }

        #endregion

        #region Awake

        private void Awake()
        {
            SetComponents();
        }

        #endregion

        #endregion


        #region Custom Private Methods

        private void SetComponents()
        {
            _playerSpriteRenderer = GetComponent<SpriteRenderer>();
            _playerMoveVelocity = GetComponent<IMoveVelocity2D>();
        }

        private void AnimateSprite()
        {
            if (_playerMovementSettings.CollCheckSettings.IsClimbing == false)
                _playerSpriteRenderer.flipX = true;

            if (_playerMovementSettings.CollCheckSettings.IsClimbing)
            {
                _playerSpriteRenderer.sprite = _climbingSprite;
                if (_playerMoveVelocity.GetVelocity().x != 0.0f || _playerMoveVelocity.GetVelocity().y != 0.0f)
                {
                    _playerSpriteRenderer.flipX = !_playerSpriteRenderer.flipX; 
                }
            }
            else if (_playerMovementSettings.CollCheckSettings.IsGrounded != true)
            {
                _playerSpriteRenderer.sprite = _jumpingSprite;
            }
            else if (_playerMoveVelocity.GetVelocity().x != 0.0f)
            {
                _spriteIndex++;

                if (_spriteIndex >= _runningSprites.Length)
                {
                    _spriteIndex = 0;
                }

                _playerSpriteRenderer.sprite = _runningSprites[_spriteIndex];
            }
            else if (_playerMoveVelocity.GetVelocity().x == 0.0f)
            {
                _playerSpriteRenderer.sprite = _runningSprites[0];
            }
        }

        #endregion
    }
}