using UnityEngine;
using DG.Tweening;
using Nojumpo.Managers;

namespace Nojumpo
{
    public class GameOverPanel : MonoBehaviour
    {
        #region Fields

        private CanvasGroup _canvasGroup;

        #endregion

        #region Unity Methods

        #region OnEnable

        private void OnEnable() {
            GameManager.Deadge += ScaleUpAnimation;
            GameManager.Deadge += CanvasAnimation;
        }

        #endregion

        #region OnDisable

        private void OnDisable() {
            GameManager.Deadge -= ScaleUpAnimation;
            GameManager.Deadge -= CanvasAnimation;
        }

        #endregion

        #region Awake

        private void Awake() {
            SetComponents();
        }

        #endregion

        #endregion


        #region Custom Private Methods

        private void SetComponents() {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        private void ScaleUpAnimation() {
            transform.DOScale(Vector3.one, 1.5f).SetUpdate(true);
        }

        private void CanvasAnimation() {
            _canvasGroup.DOFade(1, 2.5f).SetUpdate(true);
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }

        #endregion
    }
}