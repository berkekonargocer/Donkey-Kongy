using UnityEngine;
using DG.Tweening;

namespace Nojumpo
{
    public class PlayButton : MonoBehaviour
    {
        #region Field

        #region Animation Settings

        [SerializeField] private float _animationDuration;
        [SerializeField] private float _endScale;

        private Vector3 _initialScaleVector;
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
            _initialScaleVector = transform.localScale;
            _endScaleVector = Vector3.one * _endScale;
        }

        #endregion

        #region Custom Public Methods

        public void ShrinkTheButton()
        {
            transform.DOScale(_endScaleVector, _animationDuration);
        }

        public void ResetButtonScale()
        {
            transform.DOScale(_initialScaleVector, _animationDuration);
        }

        #endregion
    }
}