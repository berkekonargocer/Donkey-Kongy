using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nojumpo.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewLevelManager", menuName = "Nojumpo/Scriptable Objects/Managers/New Level Manager")]
    public class LevelManager : ScriptableObject
    {

        #region Custom Public Methods

        public void LoadLevel(int level) {
            SceneManager.LoadScene(level);
        }

        public void RestartLevel() {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        #endregion
    }
}