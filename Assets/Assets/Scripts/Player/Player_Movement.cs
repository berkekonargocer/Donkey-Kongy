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

        [SerializeField] private LayerMask _groundLayer;

        private Rigidbody2D _playerRigidbody2D;

        Vector2 _movementVector = Vector2.zero;

        #region Jump Settings

        public bool IsJumping { get; private set; } = false;

        public float JumpTimeRemaining { get; private set; } = 0.0f;

        #endregion

        #region Grounded Check Settings

        public bool IsGrounded { get; private set; } = true;

        private Transform[] _groundedCheckRaycastPositions = new Transform[3];

        RaycastHit2D[] _groundedResults = new RaycastHit2D[3];

        private int _groundedRayHits;

        private Vector3 currentVelocityReference = Vector3.zero;

        #endregion

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
            SetComponents();
        }

        private void Start()
        {

        }

        #endregion

        #region Update

        private void FixedUpdate()
        {
            IsGroundedCheck();
            HandlePlayerMovement();
        }

        #endregion

        #endregion


        #region Custom Private Methods

        private void SetComponents()
        {
            _playerRigidbody2D = GetComponent<Rigidbody2D>();

            for (int i = 0; i < _groundedCheckRaycastPositions.Length; i++)
            {
                _groundedCheckRaycastPositions[i] = transform.GetChild(0).GetChild(i).transform;
            }
        }

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

            if (!IsGrounded)
            {
                HandleGravity();
            }

            HandleJump();

            _playerRigidbody2D.velocity = Vector3.MoveTowards(_playerRigidbody2D.velocity, _movementVector, _playerMovementSettings.MovementAcceleration);
        }

        private void HandleJump()
        {
            bool triggeredJumpThisFrame = false;

            if (_jumpInput)
            {
                _jumpInput = false;

                bool triggerJump = true;

                if (!IsGrounded && !IsJumping)
                    triggerJump = false;

                if (triggerJump)
                {
                    triggeredJumpThisFrame = true;
                    JumpTimeRemaining += _playerMovementSettings.JumpTime;
                    IsJumping = true;
                }
            }

            if (IsJumping)
            {
                if (!triggeredJumpThisFrame)
                {
                    JumpTimeRemaining -= Time.deltaTime;
                }

                if (JumpTimeRemaining <= 0)
                {
                    IsJumping = false;
                }
                else
                {
                    _movementVector.y = _playerMovementSettings.JumpVelocity;
                }
            }
        }

        private void IsGroundedCheck()
        {
            if (JumpTimeRemaining > 0)
            {
                IsGrounded = false;
            }

            for (int i = 0; i < _groundedCheckRaycastPositions.Length; i++)
            {
                _groundedRayHits = Physics2D.RaycastNonAlloc(_groundedCheckRaycastPositions[i].position, Vector2.down, _groundedResults, 0.025f, _groundLayer);
            }

            if (_groundedRayHits >= 1)
            {
                IsGrounded = true;
                JumpTimeRemaining = 0.0f;
            }
            else
            {
                IsGrounded = false;
            }
        }

        protected void HandleGravity()
        {
            Vector2 gravity = Vector2.down * _playerMovementSettings.Gravity;
            _movementVector += gravity;
        }

        #endregion

        #region Custom Public Methods



        #endregion

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;

            for (int i = 0; i < _groundedCheckRaycastPositions.Length; i++)
            {
                Gizmos.DrawRay(_groundedCheckRaycastPositions[i].position, new Vector2(0, -0.025f));
            }
        }
    }
}