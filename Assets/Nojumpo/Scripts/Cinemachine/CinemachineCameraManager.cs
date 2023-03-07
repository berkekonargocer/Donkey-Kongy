using Cinemachine;
using UnityEngine;

namespace Nojumpo.Managers {
    public class CinemachineCameraManager : MonoBehaviour {
        #region Instance

        private static CinemachineCameraManager _instance;

        public static CinemachineCameraManager Instance { get { return _instance; } }

        #endregion

        #region Fields

        #region Components

        private CinemachineVirtualCamera _cinemachineVirtualCamera;
        private CinemachineBasicMultiChannelPerlin _cinemachineBasicMultiChannelPerlin;

        #endregion

        #region Camera Shake Settings

        private float _shakeTimer;

        #endregion

        #endregion



        #region Unity Methods

        #region Awake

        private void Awake() {
            InitializeSingleton();
            SetComponents();
        }

        #endregion

        #region Update

        private void Update() {
            if (_shakeTimer > 0) {
                CalculateShakeTime();
            }
        }

        #endregion

        #endregion


        #region Custom Private Methods

        private void InitializeSingleton() {
            if (_instance == null) {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else {
                Destroy(gameObject);
            }
        }

        private void SetComponents() {
            _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
            _cinemachineBasicMultiChannelPerlin = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }

        private void CalculateShakeTime() {
            _shakeTimer -= Time.unscaledDeltaTime;

            if (_shakeTimer <= 0.0f) {
                _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0.0f;
            }
        }

        //private IEnumerator ChangeLensOrtographicSizeOverTime(float size, float changeTime)
        //{
        //    _cinemachineVirtualCamera.m_Lens.OrthographicSize = size;
        //}

        #endregion

        #region Custom Public Methods


        public void ShakeCamera(float intensity) {
            _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
            _shakeTimer = 0.2f;
        }

        public void ShakeCamera(float intensity, float time) {
            _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
            _shakeTimer = time;
        }

        public void SetAFollowTarget(Transform followTarget) {
            _cinemachineVirtualCamera.Follow = followTarget;
        }

        public void SetLensOrthographicSize(float size) {
            _cinemachineVirtualCamera.m_Lens.OrthographicSize = size;
        }

        public void ChangeLensOrtographicSize(float size, float translateSpeed) {

        }

        #endregion
    }
}