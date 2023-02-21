using System;
using UnityEditor;
using UnityEngine;

namespace Nojumpo
{
    public class FireballPatrol : MonoBehaviour
    {
        #region Fields

        #region Movement Settings
        [Header("Movement Settings")]

        [SerializeField] private float _moveSpeed = 2f;

        private Transform _groundDetectionPosition;
        RaycastHit[] _groundDetectionRay = new RaycastHit[1];
        RaycastHit[] _wallDetectionRay = new RaycastHit[1];

        #endregion

        #region Layer Settings
        [Header("Layer Settings")]

        [SerializeField] private LayerMask _playerLayer;

        #endregion

        #endregion



        #region Unity Methods

        #region OnEnable

        private void OnEnable()
        {

        }

        #endregion

        #region OnDisable

        private void OnDisable()
        {

        }

        #endregion

        #region Awake and Start

        private void Awake()
        {
            SetComponents();
        }

        private void Start()
        {

        }

        #endregion

        #region Update and Fixed Update

        private void Update()
        {
            Debug.DrawRay(_groundDetectionPosition.position, Vector3.down, Color.green, 1f);
            Debug.DrawLine(transform.position, transform.right + new Vector3(-0.4f, 0f, 0f), Color.red, 1f);
        }

        private void FixedUpdate()
        {
            var groundLayerMask = ~0;

            var wallLayerMask = ~_playerLayer;

            int groundedHit = Physics.RaycastNonAlloc(_groundDetectionPosition.position, Vector3.down, _groundDetectionRay, Mathf.Infinity, groundLayerMask);
            int wallHit = Physics.RaycastNonAlloc(transform.position, transform.right, _wallDetectionRay, 0.6f, wallLayerMask);

            if (groundedHit == 0 || wallHit != 0)
            {
                // turn the fireball
            }
        }

        #endregion

        #endregion


        #region Custom Private Methods

        private void SetComponents()
        {
            _groundDetectionPosition = transform.GetChild(0).transform;
        }

        #endregion

        #region Custom Public Methods



        #endregion

        #region Gizmos

        private void OnDrawGizmos()
        {
            //Gizmos.color = Color.green;
            //Gizmos.DrawRay(_groundDetectionPosition.position, Vector3.down);
            //Gizmos.DrawRay(transform.position, transform.right + new Vector3(-0.4f, 0f, 0f));
        }

        #endregion
    }
}