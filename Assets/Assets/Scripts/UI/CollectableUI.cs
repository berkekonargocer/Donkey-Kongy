using Nojumpo.Helper;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Nojumpo
{
    public class CollectableUI : MonoBehaviour
    {
        #region Fields

        #region Components
        [Header("Components")]

        [SerializeField] private Image _collectibleUIImage;

        private RectTransform _collectibleRectTransform;

        #endregion

        #region Collect Animation Settings
        [Header("Collect Animation Settings")]

        [SerializeField] private float _animationSpeed = 50.0f;
        [SerializeField] private float _endAnimationAfterSeconds = 2.0f;
        private Vector3 _smoothDampVelocity = Vector3.zero;

        #endregion

        #endregion



        #region Unity Methods

        #region OnEnable

        private void OnEnable()
        {
            Collectable.CollectEvent += PlayCollectAnimation;
        }

        #endregion

        #region OnDisable

        private void OnDisable()
        {
            Collectable.CollectEvent -= PlayCollectAnimation;
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

        #region Update

        private void Update()
        {

        }

        #endregion

        #endregion


        #region Custom Private Methods

        private void SetComponents()
        {
            _collectibleRectTransform = GetComponent<RectTransform>();
        }
        private void PlayCollectAnimation(GameObject gameObject)
        {
            Vector2 UIPosition = Helpers.GetWorldPositionOfCanvasElement(_collectibleRectTransform);
            gameObject.transform.position = UIPosition;
            //gameObject.transform.position = Vector3.SmoothDamp(gameObject.transform.position, UIPosition, ref _smoothDampVelocity, _animationSpeed * Time.deltaTime);

            FinishTheAnimation(gameObject, true);
        }

        private void ShowUISprite(bool setActive)
        {
            _collectibleUIImage.gameObject.SetActive(setActive);
        }

        private void DestroyCollectedObject(GameObject gameObject)
        {
            Destroy(gameObject);
        }

        private IEnumerator FinishTheAnimation(GameObject gameObject, bool setActive)
        {
            yield return new WaitForSeconds(_endAnimationAfterSeconds);

            ShowUISprite(setActive);

            DestroyCollectedObject(gameObject);
        }
        #endregion

        #region Custom Public Methods


        #endregion
    }
}