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

        [SerializeField] private PlayerMovementSettings _playerMovementSettings;

        Vector2 _movementVector = Vector2.zero;

        public bool IsGrounded { get; private set; }

        #endregion

        #region Inputs

        private Vector2 _moveInput = Vector2.zero;

        private bool _jumpInput = false;
        private bool _skipCutsceneInput = false;

        #endregion


        #endregion



        #region Unity Methods

        #region OnEnable

        private void OnEnable()
        {

        }

        #endregion

        #region OnDisable

        private void OnDisable()
        {

        }

        #endregion

        #region Awake and Start

        private void Awake()
        {

        }

        private void Start()
        {

        }

        #endregion

        #region Update

        private void FixedUpdate()
        {
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

        private void OnSkip(InputValue inputValue)
        {
            _skipCutsceneInput = inputValue.isPressed;
        }

        #endregion

        private void HandlePlayerMovement()
        {
            _movementVector = (transform.right * _moveInput.x).normalized;
            _movementVector *= _playerMovementSettings.MovementSpeed;

            HandleJump();

            // character controller move
        }

        private void HandleJump()
        {

        }

        #endregion

        #region Custom Public Methods



        #endregion
    }
}