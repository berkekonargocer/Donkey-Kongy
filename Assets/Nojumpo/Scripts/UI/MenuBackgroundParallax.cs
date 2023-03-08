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

        [SerializeField] private float _scrollingSpeed = 2.75f;

        private const float BOTTOM_LIMIT_POSITION = -11.55f;
        private const float Y_POSITION_FOR_RESET = 16.58f;

        private Vector3 _scrollDirection = Vector3.down;
        private Vector3 _positionAfterReset = new Vector3(0f, Y_POSITION_FOR_RESET, 0f);

        #endregion

        #endregion



        #region Unity Methods

        #region Awake

        private void Awake() {
            SetComponents();
            SetScrollingSpeed();
        }

        #endregion

        #region Update

        private void Update() {
            VerticalScroll();

            if (_backgroundTransform.position.y < BOTTOM_LIMIT_POSITION)
            {
                ResetPosition();
            }
        }

        #endregion

        #endregion


        #region Custom Private Methods

        private void SetComponents() {
            _backgroundTransform = gameObject.transform;
        }

        private void VerticalScroll() {
            _backgroundTransform.position += _scrollDirection * Time.deltaTime;
        }

        private void SetScrollingSpeed() {
            _scrollDirection = new Vector3(0, -1 * _scrollingSpeed, 0);
        }

        private void ResetPosition() {
            _backgroundTransform.position = _positionAfterReset;
        }

        #endregion
    }
}