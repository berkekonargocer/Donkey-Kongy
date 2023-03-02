using UnityEngine;

namespace Nojumpo
{
    public class FireballPatrol : MonoBehaviour
    {
        #region Fields

        #region Movement Settings
        [Header("Movement Settings")]

        [SerializeField] private float _moveSpeed = 2f;
        [SerializeField] private float _jumpVelocity = 12.0f;

        private Rigidbody2D _fireballRigidbody2D;

        private Vector3 _movementVector = Vector3.zero;

        private bool _isMovingRight = false;

        #endregion

        #region Grounded Check and Wall Detection Settings

        [Header("Grounded Check and Wall Detection Settings")]
        [SerializeField] private float _nextStepGroundDetectionRayDistance = 0.8f;
        [SerializeField] private float _wallDetectionRayDistance = 0.5f;
        [SerializeField] private float _isGroundedCheckRayDistance = 0.5f;

        private Transform _nextStepGroundDetectionPosition;
        private Transform _isGroundedDetectionPosition;

        RaycastHit2D[] _nextStepGroundDetectionRay = new RaycastHit2D[1];
        RaycastHit2D[] _wallDetectionRay = new RaycastHit2D[1];
        RaycastHit2D[] _isGroundedDetectionSphereRay = new RaycastHit2D[1];

        #endregion

        #region Layer Settings
        [Header("Layer Settings")]

        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private LayerMask _ignoreWallDetectionRay;

        #endregion

        #endregion



        #region Unity Methods

        #region OnEnable

        private void OnEnable()
        {
            Player_Controller.OnPlayerDie += StopMoving;
        }

        #endregion

        #region OnDisable

        private void OnDisable()
        {
            Player_Controller.OnPlayerDie -= StopMoving;
        }

        #endregion

        #region Awake 

        private void Awake()
        {
            SetComponents();

            // Starts disabled and gets activated in the timeline with ActivateFireballPatrol() method
            enabled = false;
        }

        #endregion

        #region Fixed Update

        private void FixedUpdate()
        {
            NextStepGroundAndWallDetectionRays();
            HandleMovement();
        }

        #endregion

        #endregion


        #region Custom Private Methods

        private void SetComponents()
        {
            _nextStepGroundDetectionPosition = transform.GetChild(0).transform;
            _isGroundedDetectionPosition = transform.GetChild(1).transform;
        }

        private void AddRigidbody2D()
        {
            _fireballRigidbody2D = gameObject.AddComponent<Rigidbody2D>();
            _fireballRigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            _fireballRigidbody2D.gravityScale = 4.0f;
            _fireballRigidbody2D.mass = 25.0f;
        }

        private void HandleMovement()
        {
            _movementVector = (transform.right * _moveSpeed * Time.deltaTime);

            if (IsGrounded() == true)
            {
                HandleJump();
            }

            _fireballRigidbody2D.velocity = _movementVector;
        }

        private void HandleJump()
        {
            _movementVector.y = _jumpVelocity;
        }

        private bool IsGrounded()
        {

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

        private void NextStepGroundAndWallDetectionRays()
        {
            var groundLayerMask = ~0;
            var wallLayerMask = ~_ignoreWallDetectionRay;

            int groundedHit = Physics2D.RaycastNonAlloc(_nextStepGroundDetectionPosition.position, Vector2.down, _nextStepGroundDetectionRay, _nextStepGroundDetectionRayDistance, groundLayerMask);
            int wallHit = Physics2D.RaycastNonAlloc(transform.position, transform.right, _wallDetectionRay, _wallDetectionRayDistance, wallLayerMask);

            if (groundedHit != 1 || wallHit == 1)
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

        private void StopMoving()
        {
            _moveSpeed = 0.0f;
        }

        #endregion

        #region Custom Public Methods

        public void ActivateFireballPatrol()
        {
            AddRigidbody2D();
            enabled = true;
        }

        #endregion

        #region Gizmos

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(_nextStepGroundDetectionPosition.position, Vector2.down);
            Gizmos.DrawRay(transform.position, transform.right); // Not exactly the same distance! -0.5f
            Gizmos.DrawRay(_isGroundedDetectionPosition.position, new Vector2(0, -_isGroundedCheckRayDistance));
        }

        #endregion
    }
}