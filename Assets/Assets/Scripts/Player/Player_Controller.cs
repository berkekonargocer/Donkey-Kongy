using Nojumpo.Interfaces;
using Nojumpo.ScriptableObjects;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Nojumpo
{
    public class Player_Controller : MonoBehaviour
    {
        #region Fields

        #region Events

        public static Action OnPlayerDie;

        #endregion

        #region Movement Settings
        [Header("Movement Settings")]

        [SerializeField] private MovementSettings _playerMovementSettings;

        private IMoveVelocity2D _playerMoveVelocity;
        private CollisionCheckSettings _playerCollisionCheckSettings;
        private Rigidbody2D _playerRigidbody2D;

        #endregion

        #region Animation Components

        private Animator _playerAnimator;

        #endregion

        #region Inputs

        private Vector2 _moveInput = Vector2.zero;

        private bool _jumpInput = false;

        #endregion

        #endregion



        #region Unity Methods

        #region OnEnable

        private void OnEnable()
        {
            OnPlayerDie += PlayDyingAnimation;
        }

        #endregion

        #region OnDisable

        private void OnDisable()
        {
            OnPlayerDie -= PlayDyingAnimation;
        }

        #endregion

        #region Awake

        private void Awake()
        {
            SetComponents();
        }

        #endregion

        #region Update

        private void Update()
        {
            HandlePlayerJump();
            HandlePlayerMovement();
        }

        #endregion

        #endregion


        #region Custom Private Methods

        #region Input Methods

        private void OnMove(InputValue inputValue)
        {
            _moveInput = inputValue.Get<Vector2>();
        }

        private void OnJump(InputValue inputValue)
        {
            _jumpInput = inputValue.isPressed;
        }

        #endregion

        private void SetComponents()
        {
            _playerMoveVelocity = GetComponent<IMoveVelocity2D>();
            _playerRigidbody2D = GetComponent<Rigidbody2D>();
            _playerAnimator = GetComponent<Animator>();
            _playerCollisionCheckSettings = _playerMovementSettings.CollCheckSettings;
        }

        private void HandlePlayerMovement()
        {
            _playerMoveVelocity.SetVelocityX(_moveInput.x);
            _playerMoveVelocity.MultiplyVelocityX(_playerMovementSettings.MovementSpeed);

            if (_playerCollisionCheckSettings.IsClimbing)
            {
                _playerMoveVelocity.SetVelocityY(_moveInput.y);
                _playerMoveVelocity.MultiplyVelocityY(_playerMovementSettings.MovementSpeed * _playerMovementSettings.ClimbingSpeedOffset);
            }

            if (_playerMoveVelocity.GetVelocity().x > 0)
            {
                transform.eulerAngles = Vector3.zero;
            }
            else if (_playerMoveVelocity.GetVelocity().x < 0)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }

        private void HandlePlayerJump()
        {
            if (_playerCollisionCheckSettings.IsClimbing)
            {
                _playerRigidbody2D.gravityScale = 0.0f;
                return;
            }

            _playerRigidbody2D.gravityScale = 15.0f;

            if (!_playerCollisionCheckSettings.IsGrounded)
            {
                ApplyGravity();
                return;
            }

            if (_playerCollisionCheckSettings.IsGrounded && _jumpInput)
            {
                _jumpInput = false;
                _playerMoveVelocity.SetVelocity(Vector2.up * _playerMovementSettings.JumpVelocity);
            }

            if (_playerCollisionCheckSettings.IsGrounded)
            {
                _playerMoveVelocity.SetVelocityY(Mathf.Max(_playerMoveVelocity.GetVelocity().y, -0.5f));
            }
        }

        private void ApplyGravity()
        {
            _playerMoveVelocity.VelocityPlusEquals(Physics2D.gravity * 2.25f * Time.deltaTime);
            _jumpInput = false;
        }

        private void PlayDyingAnimation()
        {
            _playerAnimator.SetBool("IsDead", true);
        }

        #endregion
    }
}