using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nojumpo.Managers
{
    public class LevelManager : MonoBehaviour
    {
        #region Instance

        private static LevelManager _instance;

        public static LevelManager Instance { get { return _instance; } }

        #endregion

        #region Fields



        #endregion



        #region Unity Methods

        #region Awake and Start

        private void Awake()
        {
            InitializeSingleton();
        }

        private void Start()
        {

        }

        #endregion

        #endregion

        #region Custom Private Methods

        private void InitializeSingleton()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        #endregion

        #region Custom Public Methods

        public void LoadLevel(int level)
        {
            SceneManager.LoadScene(level);
        }

        #endregion
    }
}