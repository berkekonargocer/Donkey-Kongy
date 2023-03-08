using System;
using UnityEngine;
using UnityEngine.Playables;

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

        public static Action StartTheGame;
        public static Action<int> RestartLevel;
        public static Action<int> OnPlayerDie;
        public static Action Deadge;

        #endregion

        #region Cutscene Skip Settings

        private PlayableDirector _currentDirector;
        private bool _sceneSkipped = false;
        private float _timeToSkipTo;

        #endregion

        #endregion



        #region Unity Methods

        #region OnEnable

        private void OnEnable() {
            OnPlayerDie += SetTimeScale;
            RestartLevel += SetTimeScale;
        }

        #endregion

        #region OnDisable

        private void OnDisable() {
            OnPlayerDie -= SetTimeScale;
            RestartLevel -= SetTimeScale;
        }

        #endregion

        #region Awake

        private void Awake() {
            InitializeSingleton();
        }

        #endregion

        #region Update

        private void Update() {
            if (Input.GetKeyDown(KeyCode.Return) && !_sceneSkipped)
            {
                SkipCutscene();
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

        private void SkipCutscene() {
            _currentDirector.time = _timeToSkipTo;
            _sceneSkipped = true;
        }

        private void SetTimeScale(int timeScale) {
            Time.timeScale = timeScale;
        }

        #endregion

        #region Custom Public Methods

        public void StartGame() {
            StartTheGame?.Invoke();
        }

        public void GetDirector(PlayableDirector director) {
            _sceneSkipped = false;
            _currentDirector = director;
        }
        public void GetSkipTime(float skipTime) {
            _timeToSkipTo = skipTime;
        }

        #endregion
    }
}