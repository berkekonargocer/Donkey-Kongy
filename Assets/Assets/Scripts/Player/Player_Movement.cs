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

        #region Grounded Check Settings

        public bool IsGrounded { get; private set; } = true;

        private Transform[] _groundedCheckRaycastPositions = new Transform[3];

        RaycastHit2D[] _groundedResults = new RaycastHit2D[3];

        private int _groundedRayHits;

        #endregion

        #endregion

        #region Inputs

        private Vector2 _moveInput = Vector2.zero;

        private bool _jumpInput = false;
        private bool _skipCutsceneInput = false;

        #endregion

        #endregion



        #region Unity Methods

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

        private void FixedUpdate()
        {
            IsGroundedCheck();
            ApplyPlayerMovement();
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

        private void ApplyPlayerMovement()
        {
            _playerRigidbody2D.MovePosition(_playerRigidbody2D.position + _movementVector * Time.fixedDeltaTime);
        }

        private void HandlePlayerMovement()
        {
            _movementVector.x = _moveInput.x;
            _movementVector.x *= _playerMovementSettings.MovementSpeed;

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
            if (IsGrounded && _jumpInput)
            {
                _jumpInput = false;
                _movementVector = Vector2.up * _playerMovementSettings.JumpVelocity;
            }
            else
            {
                _movementVector += Physics2D.gravity * 2 * Time.deltaTime;
            }

            if (IsGrounded)
            {
                _movementVector.y = Mathf.Max(_movementVector.y, -1f);
            }
        }

        private void IsGroundedCheck()
        {
            for (int i = 0; i < _groundedCheckRaycastPositions.Length; i++)
            {
                _groundedRayHits = Physics2D.RaycastNonAlloc(_groundedCheckRaycastPositions[i].position, Vector2.down, _groundedResults, 0.025f, _groundLayer);
            }

            if (_groundedRayHits >= 1)
            {
                IsGrounded = true;
            }
            else
            {
                IsGrounded = false;
            }
        }

        #endregion

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;

            #region Grounded Check

            for (int i = 0; i < _groundedCheckRaycastPositions.Length; i++)
            {
                Gizmos.DrawRay(_groundedCheckRaycastPositions[i].position, new Vector2(0, -0.025f));
            }

            #endregion
        }
    }
}