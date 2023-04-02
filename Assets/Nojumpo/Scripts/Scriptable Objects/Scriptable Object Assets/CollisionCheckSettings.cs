using UnityEngine;

namespace Nojumpo.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewCollisionCheckSettings", menuName = "Nojumpo/Scriptable Objects/Collision Check/New Collision Check Settings")]
    public class CollisionCheckSettings : ScriptableObject
    {

#if UNITY_EDITOR

        [TextArea]
        [SerializeField] private string _developerDescription;

#endif

        [SerializeField] private float _colliderXSizeOffset = 0.1f;
        public float ColliderXSizeOffset { get { return _colliderXSizeOffset; } private set { _colliderXSizeOffset = value; } }

        [SerializeField] private float _colliderYSizeOffset = 2f;
        public float ColliderYSizeOffset { get { return _colliderYSizeOffset; } private set { _colliderYSizeOffset = value; } }

        [SerializeField] private bool _isGrounded = true;
        public bool IsGrounded { get { return _isGrounded; } set { _isGrounded = value; } }

        [SerializeField] private bool _isClimbing = false;
        public bool IsClimbing { get { return _isClimbing; } set { _isClimbing = value; } }
    }
}