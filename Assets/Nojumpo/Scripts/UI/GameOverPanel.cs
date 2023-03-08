using UnityEngine;
using DG.Tweening;
using Nojumpo.Managers;

namespace Nojumpo
{
    public class GameOverPanel : MonoBehaviour
    {
        #region Fields



        #endregion



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

        #region Awake and Start

        private void Awake() {

        }

        private void Start() {

        }

        #endregion

        #region Update

        private void Update() {

        }

        #endregion

        #endregion


        #region Custom Private Methods

        private void ScaleUpAnimation() {
            transform.DOScale(Vector3.one, 2f).SetUpdate(true);
        }

        #endregion

        #region Custom Public Methods



        #endregion
    }
}