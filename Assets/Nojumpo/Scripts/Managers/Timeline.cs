using System;
using UnityEngine;
using UnityEngine.Playables;

namespace Nojumpo
{
    public class Timeline : MonoBehaviour
    {
        [Header("EVENTS")]
        public static Action StartTheGame;
        public static Action GameFinishedAnimationStart;

        [Header("CUTSCENE SKIP SETTINGS")]
        private PlayableDirector _currentDirector;
        private bool _sceneSkipped = false;
        private float _timeToSkipTo = 0;


        // ------------------------ UNITY BUILT-IN METHODS ------------------------
        private void Update() {
            if (Input.GetKeyDown(KeyCode.Return) && !_sceneSkipped)
            {
                SkipCutscene();
            }
        }


        // ------------------------ CUSTOM PRIVATE METHODS ------------------------
        private void SkipCutscene() {
            _currentDirector.time = _timeToSkipTo;
            _sceneSkipped = true;
        }


        // ------------------------ CUSTOM PUBLIC METHODS ------------------------
        public void StartGame() {
            StartTheGame?.Invoke();
        }

        public void InvokeGameFinishedAnimationStart() {
            GameFinishedAnimationStart?.Invoke();
        }

        public void GetDirector(PlayableDirector director) {
            _sceneSkipped = false;
            _currentDirector = director;
        }

        public void GetSkipTime(float skipTime) {
            _timeToSkipTo = skipTime;
        }
    }
}