using UnityEngine;

namespace Nojumpo.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewPlayerMovementSettings", menuName = "Nojumpo/Scriptable Objects/Player/New Player Movement Settings")]
    public class MovementSettings : ScriptableObject
    {

#if UNITY_EDITOR

        [TextArea]
        [SerializeField] string _developerDescription;

#endif

        [SerializeField] float _movementSpeed = 15.0f;
        public float MovementSpeed { get { return _movementSpeed; } set { _movementSpeed = value; } }

        [SerializeField] float _jumpVelocity = 10.0f;
        public float JumpVelocity { get { return _jumpVelocity; } set { _jumpVelocity = value; } }

        [SerializeField] float _climbingSpeedOffset = 0.5f;
        public float ClimbingSpeedOffset { get { return _climbingSpeedOffset; } set { _climbingSpeedOffset = value; } }

        [SerializeField] CollisionCheckSettings _collisionCheckSettings;
        public CollisionCheckSettings CollCheckSettings { get { return _collisionCheckSettings; } }


    }
}
