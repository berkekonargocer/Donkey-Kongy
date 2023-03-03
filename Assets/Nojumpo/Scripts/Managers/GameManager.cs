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

        #region Cutscene Skip Settings

        private PlayableDirector _currentDirector;
        private bool _sceneSkipped = false;
        private float _timeToSkipTo;

        #endregion

        #endregion



        #region Unity Methods

        #region OnEnable

        private void OnEnable()
        {
            Player_Controller.OnPlayerDie += SetTimeScaleToZero;
        }

        #endregion

        #region OnDisable

        private void OnDisable()
        {
            Player_Controller.OnPlayerDie -= SetTimeScaleToZero;
        }

        #endregion

        #region Awake and Start

        private void Awake()
        {
            InitializeSingleton();
        }

        #endregion

        #region Update

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !_sceneSkipped)
            {
                SkipCutscene();
            }
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

        private void SkipCutscene()
        {
            _currentDirector.time = _timeToSkipTo;
            _sceneSkipped = true;
        }

        private void SetTimeScaleToZero()
        {
            Time.timeScale = 0.0f;
        }

        #endregion

        #region Custom Public Methods

        public void GetDirector(PlayableDirector director)
        {
            _sceneSkipped = false;
            _currentDirector = director;
        }
        public void GetSkipTime(float skipTime)
        {
            _timeToSkipTo = skipTime;
        }

        #endregion
    }
}