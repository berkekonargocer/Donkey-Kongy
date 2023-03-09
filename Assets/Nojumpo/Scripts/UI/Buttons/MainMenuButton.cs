using UnityEngine.SceneManagement;

namespace Nojumpo.Buttons
{
    public class MainMenuButton : ButtonBase
    {
        #region Custom Public Methods

        public void MainMenu() {
            SceneManager.LoadScene(0);
        }

        #endregion
    }
}