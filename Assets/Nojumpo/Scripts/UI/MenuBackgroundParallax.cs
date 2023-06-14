using UnityEngine;

namespace Nojumpo
{
    public class MenuBackgroundParallax : MonoBehaviour
    {
        [Header("COMPONENTS")]
         Transform _backgroundTransform;

        [Header("PARALLAX SETTINGS")]
        [SerializeField]  float _scrollingSpeed = 2.75f;
         const float BOTTOM_LIMIT_POSITION = -11.55f;
         const float Y_POSITION_FOR_RESET = 16.58f;
         Vector3 _scrollDirection = Vector3.down;
         Vector3 _positionAfterReset = new Vector3(0f, Y_POSITION_FOR_RESET, 0f);


        // ------------------------ UNITY BUILT-IN METHODS ------------------------
         void Awake() {
            SetComponents();
            SetScrollingSpeed();
        }

         void Update() {
            VerticalScroll();

            if (_backgroundTransform.position.y < BOTTOM_LIMIT_POSITION)
            {
                ResetPosition();
            }
        }


        // ------------------------ CUSTOM PRIVATE METHODS ------------------------
         void SetComponents() {
            _backgroundTransform = gameObject.transform;
        }

         void VerticalScroll() {
            _backgroundTransform.position += _scrollDirection * Time.deltaTime;
        }

         void SetScrollingSpeed() {
            _scrollDirection = new Vector3(0, -1 * _scrollingSpeed, 0);
        }

         void ResetPosition() {
            _backgroundTransform.position = _positionAfterReset;
        }
    }
}