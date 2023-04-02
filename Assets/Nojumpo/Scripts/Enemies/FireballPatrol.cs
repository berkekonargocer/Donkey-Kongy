using UnityEngine;

namespace Nojumpo
{
    public class FireballPatrol : MonoBehaviour
    {
        [Header("MOVEMENT SETTINGS")]
        [SerializeField] private float _moveSpeed = 2f;
        [SerializeField] private float _jumpVelocity = 12.0f;
        private bool _isMovingRight = false;
        private Rigidbody2D _fireballRigidbody2D;
        private Vector3 _movementVector = Vector3.zero;

        [Header("GROUNDED CHECK AND WALL DETECTION SETTINGS")]
        [SerializeField] private float _nextStepGroundDetectionRayDistance = 0.8f;
        [SerializeField] private float _wallDetectionRayDistance = 0.5f;
        [SerializeField] private float _isGroundedCheckRayDistance = 0.5f;
        private Transform _nextStepGroundDetectionPosition;
        private Transform _isGroundedDetectionPosition;
        private RaycastHit2D[] _nextStepGroundDetectionRay = new RaycastHit2D[2];
        private RaycastHit2D[] _wallDetectionRay = new RaycastHit2D[1];
        private RaycastHit2D[] _isGroundedDetectionSphereRay = new RaycastHit2D[1];
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private LayerMask _ignoreWallDetectionRay;


        // ------------------------ UNITY BUILT-IN METHODS ------------------------
        private void OnEnable() {
            Timeline.StartTheGame += AddRigidbody2D;
        }

        private void OnDisable() {
            Timeline.StartTheGame -= AddRigidbody2D;
        }

        private void Awake() {
            SetComponents();

            // Starts disabled and gets activated in the timeline with ActivateFireballPatrol() method
            enabled = false;
        }

        private void FixedUpdate() {
            NextStepGroundAndWallDetectionRays();
            HandleMovement();
        }


        // ------------------------ CUSTOM PRIVATE METHODS ------------------------
        private void SetComponents() {
            _nextStepGroundDetectionPosition = transform.GetChild(0).transform;
            _isGroundedDetectionPosition = transform.GetChild(1).transform;
        }

        private void AddRigidbody2D() {
            _fireballRigidbody2D = gameObject.AddComponent<Rigidbody2D>();
            _fireballRigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            _fireballRigidbody2D.gravityScale = 4.0f;
            _fireballRigidbody2D.mass = 25.0f;
        }

        private void HandleMovement() {
            _movementVector = (transform.right * _moveSpeed * Time.deltaTime);

            if (IsGrounded() == true)
            {
                HandleJump();
            }

            _fireballRigidbody2D.velocity = _movementVector;
        }

        private void HandleJump() {
            _movementVector.y = _jumpVelocity;
        }

        private bool IsGrounded() {
            int groundedHit = Physics2D.RaycastNonAlloc(_isGroundedDetectionPosition.position, Vector2.down, _isGroundedDetectionSphereRay, _isGroundedCheckRayDistance, _groundLayer);

            if (groundedHit != 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void NextStepGroundAndWallDetectionRays() {
            var wallLayerMask = ~_ignoreWallDetectionRay;

            int groundedHit = Physics2D.RaycastNonAlloc(_nextStepGroundDetectionPosition.position, Vector2.down, _nextStepGroundDetectionRay, _nextStepGroundDetectionRayDistance, _groundLayer);
            int wallHit = Physics2D.RaycastNonAlloc(transform.position, transform.right, _wallDetectionRay, _wallDetectionRayDistance, wallLayerMask);

            if (groundedHit == 0 || wallHit != 0)
            {
                if (_isMovingRight)
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    _isMovingRight = false;
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    _isMovingRight = true;
                }
            }
        }


        // ------------------------ CUSTOM PUBLIC METHODS ------------------------
        public void SetVelocitiesToZero() {
            _moveSpeed = 0;
            _jumpVelocity = 0;
        }
    }
}