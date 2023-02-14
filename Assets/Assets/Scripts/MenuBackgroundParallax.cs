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

        private const float BOTTOM_LIMIT_POSITION = -11.55f;
        private Vector3 _resetPosition = new Vector3(0f, 16.58f, 0f);

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
            _backgroundTransform = gameObject.transform;
        }

        private void Start()
        {

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
            _backgroundTransform.position = _resetPosition;
        }

        #endregion

        #region Custom Public Methods



        #endregion
    }
}