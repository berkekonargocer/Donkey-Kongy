using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nojumpo.Managers
{
    public class GameManager : MonoBehaviour
    {
        #region Instance

        private static GameManager _instance;

        public static GameManager Instance { get { return _instance; } }

        #endregion

        #region Fields

        #region Events

        public static Action<int> RestartLevel;
        public static Action<int> OnPlayerDie;
        public static Action Deadge;

        #endregion

        #endregion



        #region Unity Methods

        #region OnEnable

        private void OnEnable() {
            OnPlayerDie += SetTimeScale;
            RestartLevel += SetTimeScale;
            RestartLevel += ReloadScene;
            Timeline.StartTheGame += ClearItemsCollection;
        }

        #endregion

        #region OnDisable

        private void OnDisable() {
            OnPlayerDie -= SetTimeScale;
            RestartLevel -= SetTimeScale;
            RestartLevel -= ReloadScene;
            Timeline.StartTheGame -= ClearItemsCollection;
        }

        #endregion

        #region Awake

        private void Awake() {
            InitializeSingleton();
        }

        #endregion

        #endregion


        #region Custom Private Methods

        private void InitializeSingleton() {
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

        private void SetTimeScale(int timeScale) {
            Time.timeScale = timeScale;
        }

        private void ReloadScene(int timeScale) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void ClearItemsCollection() {
            CollectedItems.ItemsCollection.Clear();
        }

        #endregion

        #region Custom Public Methods

        public void LoadScene(int level) {
            SceneManager.LoadScene(level);
        }

        #endregion
    }
}