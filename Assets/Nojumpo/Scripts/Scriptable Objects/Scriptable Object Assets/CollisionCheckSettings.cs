using UnityEngine;

namespace Nojumpo.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewCollisionCheckSettings", menuName = "Nojumpo/Scriptable Objects/Collision Check/New Collision Check Settings")]
    public class CollisionCheckSettings : ScriptableObject
    {

#if UNITY_EDITOR

        [TextArea]
        [SerializeField]  string _developerDescription;

#endif

        [SerializeField]  float _colliderXSizeOffset = 0.1f;
        public float ColliderXSizeOffset { get { return _colliderXSizeOffset; }  set { _colliderXSizeOffset = value; } }

        [SerializeField]  float _colliderYSizeOffset = 2f;
        public float ColliderYSizeOffset { get { return _colliderYSizeOffset; }  set { _colliderYSizeOffset = value; } }

        [SerializeField]  bool _isGrounded = true;
        public bool IsGrounded { get { return _isGrounded; } set { _isGrounded = value; } }

        [SerializeField]  bool _isClimbing = false;
        public bool IsClimbing { get { return _isClimbing; } set { _isClimbing = value; } }
    }
}