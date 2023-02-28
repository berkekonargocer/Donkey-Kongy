using Nojumpo.ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Nojumpo
{
    public class Player_Movement : MonoBehaviour
    {
        #region Fields

        #region Movement Settings
        [Header("Movement Settings")]

        [SerializeField] private MovementSettings _playerMovementSettings;

        private CollisionCheckSettings _playerCollisionCheckSettings;
        private Rigidbody2D _playerRigidbody2D;

        Vector2 _movementVector = Vector2.zero;

        #endregion

        #region Inputs

        private Vector2 _moveInput = Vector2.zero;

        private bool _jumpInput = false;

        #endregion

        #endregion



        #region Unity Methods

        #region Awake

        private void Awake()
        {
            SetComponents();
        }

        #endregion

        #region Update and Fixed Update

        private void Update()
        {
            HandlePlayerJump();
            HandlePlayerMovement();

        }

        private void FixedUpdate()
        {
            ApplyPlayerMovement();
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
            _playerCollisionCheckSettings = _playerMovementSettings.CollCheckSettings;
            _playerRigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void HandlePlayerMovement()
        {
            _movementVector.x = _moveInput.x;
            _movementVector.x *= _playerMovementSettings.MovementSpeed;

            if (_playerCollisionCheckSettings.IsClimbing)
            {
                _movementVector.y = _moveInput.y;
                _movementVector.y *= _playerMovementSettings.MovementSpeed;
            }

            if (_movementVector.x > 0)
            {
                transform.eulerAngles = Vector3.zero;
            }
            else if (_movementVector.x < 0)
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
                _movementVector = Vector2.up * _playerMovementSettings.JumpVelocity;
            }

            if (_playerCollisionCheckSettings.IsGrounded)
            {
                _movementVector.y = Mathf.Max(_movementVector.y, -0.5f);
            }
        }

        private void ApplyPlayerMovement()
        {
            _playerRigidbody2D.MovePosition(_playerRigidbody2D.position + _movementVector * Time.fixedDeltaTime);
        }

        private void ApplyGravity()
        {
            _movementVector += Physics2D.gravity * 2.25f * Time.deltaTime;
            _jumpInput = false;
        }

        #endregion
    }
}