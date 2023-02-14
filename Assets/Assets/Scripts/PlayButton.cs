using UnityEngine;

namespace Nojumpo
{
    public class PlayButton : MonoBehaviour
    {
        #region Fields

        #region Components

        private Transform _playButton;

        #endregion

        #region Animation Settings

        [SerializeField] private float _animationSpeed;
        [SerializeField] private float _endScale;
        private Vector3 _endScaleVector;

        #endregion

        #endregion



        #region Unity Methods

        #region Awake

        private void Awake()
        {
            SetComponents();
        }

        #endregion

        #endregion


        #region Custom Private Methods

        private void SetComponents()
        {
            _playButton = GetComponent<Transform>();
            _endScaleVector = Vector3.one * _endScale;
        }

        #endregion

        #region Custom Public Methods

        public void ShrinkTheButton()
        {

            //_playButton.transform.localScale = 
        }

        public void ResetButtonScale()
        {

        }

        #endregion
    }
}