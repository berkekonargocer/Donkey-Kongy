using UnityEngine;
using DG.Tweening;
using Nojumpo.Managers;

namespace Nojumpo
{
    public class PlayButton : ButtonBase
    {

        #region Custom Public Methods

        public void Play() {
            GameManager gameManager = FindObjectOfType<GameManager>();
            gameManager.LoadScene(1);
        }

        #endregion
    }
}