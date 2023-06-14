using UnityEngine;

namespace Nojumpo
{
    public class FireballPatrol : MonoBehaviour
    {
        [Header("MOVEMENT SETTINGS")]
        [SerializeField] float _moveSpeed = 2f;
        [SerializeField] float _jumpVelocity = 12.0f;
        bool _isMovingRight = false;
        Rigidbody2D _fireballRigidbody2D;
        Vector3 _movementVector = Vector3.zero;

        [Header("GROUNDED CHECK AND WALL DETECTION SETTINGS")]
        [SerializeField] float _nextStepGroundDetectionRayDistance = 0.8f;
        [SerializeField] float _wallDetectionRayDistance = 0.5f;
        [SerializeField] float _isGroundedCheckRayDistance = 0.5f;
        Transform _nextStepGroundDetectionPosition;
        Transform _isGroundedDetectionPosition;
        RaycastHit2D[] _nextStepGroundDetectionRay = new RaycastHit2D[2];
        RaycastHit2D[] _wallDetectionRay = new RaycastHit2D[1];
        RaycastHit2D[] _isGroundedDetectionSphereRay = new RaycastHit2D[1];
        [SerializeField] LayerMask _groundLayer;
        [SerializeField] LayerMask _ignoreWallDetectionRay;


        // ------------------------ UNITY BUILT-IN METHODS ------------------------
        void OnEnable() {
            Timeline.StartTheGame += AddRigidbody2D;
        }

        void OnDisable() {
            Timeline.StartTheGame -= AddRigidbody2D;
        }

        void Awake() {
            SetComponents();

            // Starts disabled and gets activated in the timeline with ActivateFireballPatrol() method
            enabled = false;
        }

        void FixedUpdate() {
            NextStepGroundAndWallDetectionRays();
            HandleMovement();
        }


        // ------------------------ CUSTOM PRIVATE METHODS ------------------------
        void SetComponents() {
            _nextStepGroundDetectionPosition = transform.GetChild(0).transform;
            _isGroundedDetectionPosition = transform.GetChild(1).transform;
        }

        void AddRigidbody2D() {
            _fireballRigidbody2D = gameObject.AddComponent<Rigidbody2D>();
            _fireballRigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            _fireballRigidbody2D.gravityScale = 4.0f;
            _fireballRigidbody2D.mass = 25.0f;
        }

        void HandleMovement() {
            _movementVector = (transform.right * _moveSpeed * Time.deltaTime);

            if (IsGrounded() == true)
            {
                HandleJump();
            }

            _fireballRigidbody2D.velocity = _movementVector;
        }

        void HandleJump() {
            _movementVector.y = _jumpVelocity;
        }

        bool IsGrounded() {
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

        void NextStepGroundAndWallDetectionRays() {
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
