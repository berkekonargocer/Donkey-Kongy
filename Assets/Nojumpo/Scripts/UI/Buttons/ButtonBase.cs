using DG.Tweening;
using UnityEngine;

namespace Nojumpo.Buttons
{
    public class ButtonBase : MonoBehaviour
    {
        [Header("ANIMATION SETTINGS")]
        [SerializeField] float _animationDuration;
        [SerializeField] float _endScale;
        Vector3 _initialScaleVector;
        Vector3 _endScaleVector;


        // ------------------------ UNITY BUILT-IN METHODS ------------------------
        void Awake() {
            SetComponents();
        }


        // ------------------------ CUSTOM PRIVATE METHODS ------------------------
        void SetComponents() {
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
