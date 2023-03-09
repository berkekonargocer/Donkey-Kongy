using UnityEngine;
using DG.Tweening;
using Nojumpo.Managers;

namespace Nojumpo.Buttons
{
    public class PlayButton : ButtonBase
    {

        #region Custom Public Methods

        public void Play() {
            GameManager gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
            gameManager.LoadScene(1);
        }

        #endregion
    }
}