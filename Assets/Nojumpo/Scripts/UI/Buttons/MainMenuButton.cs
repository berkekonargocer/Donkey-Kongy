using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nojumpo.Buttons
{
    public class MainMenuButton : ButtonBase
    {
        #region Custom Public Methods

        public void MainMenu() {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene(0);
        }

        #endregion
    }
}