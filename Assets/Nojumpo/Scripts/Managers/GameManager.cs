using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nojumpo.Managers
{
    public class GameManager : MonoBehaviour
    {
        [Header("SINGLETON")]
        static GameManager _instance;
        public static GameManager Instance { get { return _instance; } }

        [Header("EVENTS")]
        public static Action<int, bool> RestartLevel;
        public static Action<int, bool> OnPlayerDie;
        public static Action Deadge;

        bool _isDead = false;


        // ------------------------ UNITY BUILT-IN METHODS ------------------------
        void OnEnable() {
            EventSubscriptions();
        }

        void OnDisable() {
            EventUnsubscriptions();
        }

        void Awake() {
            InitializeSingleton();
        }

        void Update() {
            if (_isDead && Input.GetKeyDown(KeyCode.Return))
            {
                RestartLevel?.Invoke(1, false);
            }
        }


        // ------------------------ CUSTOM PRIVATE METHODS ------------------------
        void InitializeSingleton() {
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

        void EventSubscriptions() {
            OnPlayerDie += SetTimeScale;
            OnPlayerDie += SetIsDead;
            RestartLevel += SetTimeScale;
            RestartLevel += ReloadScene;
            RestartLevel += SetIsDead;
            Timeline.StartTheGame += ClearItemsCollection;
            SceneManager.sceneLoaded += SetVariables;
        }

        void EventUnsubscriptions() {
            OnPlayerDie -= SetTimeScale;
            OnPlayerDie -= SetIsDead;
            RestartLevel -= SetTimeScale;
            RestartLevel -= ReloadScene;
            RestartLevel -= SetIsDead;
            Timeline.StartTheGame -= ClearItemsCollection;
            SceneManager.sceneLoaded -= SetVariables;
        }

        void SetVariables(Scene scene, LoadSceneMode loadSceneMode) {
            _isDead = false;
        }

        void SetTimeScale(int timeScale, bool isDead) {
            Time.timeScale = timeScale;
        }

        void ReloadScene(int timeScale, bool isDead) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        void ClearItemsCollection() {
            CollectedItems.ItemsCollection.Clear();
        }

        void SetIsDead(int timeScale, bool isDead) {
            _isDead = isDead;
        }


        // ------------------------ CUSTOM PUBLIC METHODS ------------------------
        public void LoadScene(int level) {
            SceneManager.LoadScene(level);
        }
    }
}
