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
        [SerializeField] MovementSettings _playerMovementSettings;
        CollisionCheckSettings _playerCollisionCheckSettings;
        Rigidbody2D _playerRigidbody2D;
        Animator _playerAnimator;
        IMoveVelocity2D _playerMoveVelocity;

        [Header("AUDIO SETTINGS")]
        [SerializeField] AudioSource _jumpSFXSource;
        AudioSource _playerAudioSource;

        [Header("INPUT SETTINGS")]
        Vector2 _moveInput = Vector2.zero;
        bool _jumpInput = false;


        // --------------------- UNITY BUILT-IN METHODS ---------------------
        void OnEnable() {
            EventSubscriptions();
        }

        void OnDisable() {
            EventUnsubscriptions();
        }

        void Awake() {
            SetComponents();
        }

        void Update() {
            HandlePlayerJump();
            HandlePlayerMovement();
        }


        // ------------------------ INPUT METHODS ------------------------
        void OnMove(InputValue inputValue) {
            _moveInput = inputValue.Get<Vector2>();
        }

        void OnJump(InputValue inputValue) {
            _jumpInput = inputValue.isPressed;
        }


        // ------------------------ CUSTOM PRIVATE METHODS ------------------------
        void SetComponents() {
            _playerMoveVelocity = GetComponent<IMoveVelocity2D>();
            _playerRigidbody2D = GetComponent<Rigidbody2D>();
            _playerAnimator = GetComponent<Animator>();
            _playerAudioSource = GetComponent<AudioSource>();
            _playerCollisionCheckSettings = _playerMovementSettings.CollCheckSettings;
        }

        void EventSubscriptions() {
            GameManager.OnPlayerDie += PlayDyingAnimation;
            GameManager.OnPlayerDie += DisableController;
        }

        void EventUnsubscriptions() {
            GameManager.OnPlayerDie -= PlayDyingAnimation;
            GameManager.OnPlayerDie -= DisableController;
        }

        void HandlePlayerMovement() {
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

        void HandlePlayerJump() {
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

        void ApplyGravity() {
            _playerMoveVelocity.VelocityPlusEquals(Physics2D.gravity * 2.25f * Time.deltaTime);
            _jumpInput = false;
        }

        void PlayAudio(AudioClip audio) {
            _playerAudioSource.clip = audio;
            _playerAudioSource.Play();
        }

        void PlayDyingAnimation(int timeScale, bool isDead) {
            _playerAnimator.SetBool("IsDead", true);
        }

        void InvokeDeadge() {
            GameManager.Deadge?.Invoke();
        }

        void DisableController(int timeScale, bool isDead) {
            enabled = false;
        }

    }
}
