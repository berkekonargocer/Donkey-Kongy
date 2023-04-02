using DG.Tweening;
using UnityEngine;

namespace Nojumpo.Buttons
{
    public class ButtonBase : MonoBehaviour
    {
        [Header("ANIMATION SETTINGS")]
        [SerializeField] private float _animationDuration;
        [SerializeField] private float _endScale;
        private Vector3 _initialScaleVector;
        private Vector3 _endScaleVector;


        // ------------------------ UNITY BUILT-IN METHODS ------------------------
        private void Awake() {
            SetComponents();
        }


        // ------------------------ CUSTOM PRIVATE METHODS ------------------------
        private void SetComponents() {
            _initialScaleVector = transform.localScale;
            _endScaleVector = Vector3.one * _endScale;
        }


        // ------------------------ CUSTOM PUBLIC METHODS ------------------------
        public void ShrinkTheButton() {
            transform.DOScale(_endScaleVector, _animationDuration);
        }

        public void ResetButtonScale() {
            transform.DOScale(_initialScaleVector, _animationDuration);
        }
    }
}