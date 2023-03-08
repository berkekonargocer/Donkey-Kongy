using UnityEngine;
using DG.Tweening;
using Nojumpo.Managers;

namespace Nojumpo
{
    public class GameOverPanel : MonoBehaviour
    {
        #region Unity Methods

        #region OnEnable

        private void OnEnable() {
            GameManager.Deadge += ScaleUpAnimation;
        }

        #endregion

        #region OnDisable

        private void OnDisable() {
            GameManager.Deadge -= ScaleUpAnimation;
        }

        #endregion

        #endregion


        #region Custom Private Methods

        private void ScaleUpAnimation() {
            transform.DOScale(Vector3.one, 1.5f).SetUpdate(true);
        }

        #endregion
    }
}