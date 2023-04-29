using Nojumpo.Interfaces;
using Nojumpo.Managers;
using Nojumpo.ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Nojumpo
{
    public class Player_Controller : MonoBehaviour
    {
        [Header("MOVEMENT SETTINGS")]
        [SerializeField] private MovementSettings _playerMovementSettings;
        private CollisionCheckSettings _playerCollisionCheckSettings;
        private Rigidbody2D _playerRigidbody2D;
        private Animator _playerAnimator;
        private IMoveVelocity2D _playerMoveVelocity;

        [Header("AUDIO SETTINGS")]
        [SerializeField] private AudioSource _jumpSFXSource;
        private AudioSource _playerAudioSource;

        [Header("INPUT SETTINGS")]
        private Vector2 _moveInput = Vector2.zero;
        private bool _jumpInput = false;


        // --------------------- UNITY BUILT-IN METHODS ---------------------
        private void OnEnable() {
            EventSubscriptions();
        }

        private void OnDisable() {
            EventUnsubscriptions();
        }

        private void Awake() {
            SetComponents();
        }

        private void Update() {
            HandlePlayerJump();
            HandlePlayerMovement();
        }


        // ------------------------ INPUT METHODS ------------------------
        private void OnMove(InputValue inputValue) {
            _moveInput = inputValue.Get<Vector2>();
        }

        private void OnJump(InputValue inputValue) {
            _jumpInput = inputValue.isPressed;
        }


        // ------------------------ CUSTOM PRIVATE METHODS ------------------------
        private void SetComponents() {
            _playerMoveVelocity = GetComponent<IMoveVelocity2D>();
            _playerRigidbody2D = GetComponent<Rigidbody2D>();
            _playerAnimator = GetComponent<Animator>();
            _playerAudioSource = GetComponent<AudioSource>();
            _playerCollisionCheckSettings = _playerMovementSettings.CollCheckSettings;
        }

        private void EventSubscriptions() {
            GameManager.OnPlayerDie += PlayDyingAnimation;
            GameManager.OnPlayerDie += DisableController;
        }

        private void EventUnsubscriptions() {
            GameManager.OnPlayerDie -= PlayDyingAnimation;
            GameManager.OnPlayerDie -= DisableController;
        }

        private void HandlePlayerMovement() {
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

        private void HandlePlayerJump() {
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
                _jumpSFXSource.Play();
                _playerMoveVelocity.SetVelocity(Vector2.up * _playerMovementSettings.JumpVelocity);
            }

            if (_playerCollisionCheckSettings.IsGrounded)
            {
                _playerMoveVelocity.SetVelocityY(Mathf.Max(_playerMoveVelocity.GetVelocity().y, -0.5f));
            }
        }

        private void ApplyGravity() {
            _playerMoveVelocity.VelocityPlusEquals(Physics2D.gravity * 2.25f * Time.deltaTime);
            _jumpInput = false;
        }

        private void PlayAudio(AudioClip audio) {
            _playerAudioSource.clip = audio;
            _playerAudioSource.Play();
        }

        private void PlayDyingAnimation(int timeScale, bool isDead) {
            _playerAnimator.SetBool("IsDead", true);
        }

        private void InvokeDeadge() {
            GameManager.Deadge?.Invoke();
        }

        private void DisableController(int timeScale, bool isDead) {
            enabled = false;
        }

    }
}
