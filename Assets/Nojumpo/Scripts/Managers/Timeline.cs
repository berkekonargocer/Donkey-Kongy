using System;
using UnityEngine;
using UnityEngine.Playables;

namespace Nojumpo
{
    public class Timeline : MonoBehaviour
    {
        #region Fields

        #region Events

        public static Action StartTheGame;

        #endregion

        #region Cutscene Skip Settings

        private PlayableDirector _currentDirector;
        private bool _sceneSkipped = false;
        private float _timeToSkipTo;

        #endregion

        #endregion



        #region Unity Methods

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

        private void SkipCutscene() {
            _currentDirector.time = _timeToSkipTo;
            _sceneSkipped = true;
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