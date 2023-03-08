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

        #endregion

        #region Custom Public Methods

        public void InvokeRestartLevel() {
            RestartLevel?.Invoke(1);
        }

        #endregion
    }
}