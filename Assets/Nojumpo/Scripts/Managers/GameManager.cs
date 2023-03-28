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

        public static Action<int, bool> RestartLevel;
        public static Action<int, bool> OnPlayerDie;
        public static Action Deadge;

        #endregion

        #region State Bools

        private bool _isDead = false;

        #endregion

        #endregion



        #region Unity Methods

        #region OnEnable

        private void OnEnable() {
            EventSubscriptions();
        }


        #endregion

        #region OnDisable

        private void OnDisable() {
            EventUnsubscriptions();
        }


        #endregion

        #region Awake

        private void Awake() {
            InitializeSingleton();
        }

        #endregion

        #region Update

        private void Update() {
            if (_isDead && Input.GetKeyDown(KeyCode.Return))
            {
                RestartLevel?.Invoke(1, false);
            }
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

        private void EventSubscriptions() {
            OnPlayerDie += SetTimeScale;
            OnPlayerDie += SetIsDead;
            RestartLevel += SetTimeScale;
            RestartLevel += ReloadScene;
            RestartLevel += SetIsDead;
            Timeline.StartTheGame += ClearItemsCollection;
            SceneManager.sceneLoaded += SetVariables;
        }

        private void EventUnsubscriptions() {
            OnPlayerDie -= SetTimeScale;
            OnPlayerDie -= SetIsDead;
            RestartLevel -= SetTimeScale;
            RestartLevel -= ReloadScene;
            RestartLevel -= SetIsDead;
            Timeline.StartTheGame -= ClearItemsCollection;
            SceneManager.sceneLoaded -= SetVariables;
        }

        private void SetVariables(Scene scene, LoadSceneMode loadSceneMode) {
            _isDead = false;
        }

        private void SetTimeScale(int timeScale, bool isDead) {
            Time.timeScale = timeScale;
        }

        private void ReloadScene(int timeScale, bool isDead) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void ClearItemsCollection() {
            CollectedItems.ItemsCollection.Clear();
        }

        private void SetIsDead(int timeScale, bool isDead) {
            _isDead = isDead;
        }

        #endregion

        #region Custom Public Methods

        public void LoadScene(int level) {
            SceneManager.LoadScene(level);
        }

        #endregion
    }
}