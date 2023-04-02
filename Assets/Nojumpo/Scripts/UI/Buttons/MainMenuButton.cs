using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nojumpo.Buttons
{
    public class MainMenuButton : ButtonBase
    {
        public void MainMenu() {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene(0);
        }
    }
}