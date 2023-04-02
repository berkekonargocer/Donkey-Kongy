using Nojumpo.Managers;
using UnityEngine;

namespace Nojumpo.Buttons
{
    public class PlayButton : ButtonBase
    {
        public void Play() {
            GameManager gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
            gameManager.LoadScene(1);
        }
    }
}