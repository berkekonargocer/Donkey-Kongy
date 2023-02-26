using System.Collections.Generic;
using UnityEngine;

namespace Nojumpo.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewPlayerMovementSettings", menuName = "Nojumpo/Scriptable Objects/Player/New Player Movement Settings")]
    public class PlayerMovementSettings : ScriptableObject
    {
        #region Fields

    #if UNITY_EDITOR

        [TextArea]
        [SerializeField] private string _developerDescription;

#endif

        [SerializeField] private float _movementSpeed = 15.0f;
        public float MovementSpeed { get { return _movementSpeed; } private set { _movementSpeed = value; } }

        [SerializeField] private float _jumpVelocity = 10.0f;
        public float JumpVelocity { get { return _jumpVelocity;} private set { _jumpVelocity = value; } }

        [SerializeField] private float _movementAcceleration = 5.0f;
        public float MovementAcceleration { get { return _movementAcceleration; } private set { _movementAcceleration = value; } }

        [SerializeField] private float _jumpTime = 2.5f;
        public float JumpTime { get { return _jumpTime; } private set { _jumpTime = value; } }

        [SerializeField] private float _gravity = 13.9f;
        public float Gravity { get { return _gravity;} private set { _gravity = value; } }

        #endregion
    }
}