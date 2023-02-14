using UnityEngine;

namespace Nojumpo
{
    public class MenuBackgroundParallax : MonoBehaviour
    {
        #region Fields

        #region Components

        private Transform _backgroundTransform;

        #endregion

        #region Scrolling Settings

        [Header("Scrolling Settings")]

        [SerializeField] private float _currentScrollingSpeed;
        private float _lastScrollingSpeed;

        private Vector3 _scrollDirection = Vector3.down;
        private Vector3 _positionAfterReset = new Vector3(0f, 16.58f, 0f);

        private const float BOTTOM_LIMIT_POSITION = -11.55f;

        #endregion

        #endregion



        #region Unity Methods

        #region Awake

        private void Awake()
        {
            _backgroundTransform = gameObject.transform;
        }

        #endregion

        #region Update

        private void Update()
        {
            if (_currentScrollingSpeed != _lastScrollingSpeed)
            {
                SetScrollingSpeed();
            }

            VerticalScroll();

            if (_backgroundTransform.position.y < BOTTOM_LIMIT_POSITION)
            {
                ResetPosition();
            }
        }

        #endregion

        #endregion


        #region Custom Private Methods

        private void VerticalScroll()
        {
            _backgroundTransform.position += _scrollDirection * Time.deltaTime;
        }

        private void SetScrollingSpeed()
        {
            _scrollDirection = new Vector3(0, -1 * _currentScrollingSpeed, 0);
            _lastScrollingSpeed = _currentScrollingSpeed;
        }

        private void ResetPosition()
        {
            _backgroundTransform.position = _positionAfterReset;
        }

        #endregion
    }
}