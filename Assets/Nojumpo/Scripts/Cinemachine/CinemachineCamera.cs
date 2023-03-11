using Cinemachine;
using System.Collections;
using UnityEngine;
using Nojumpo.Managers;

namespace Nojumpo
{
    public class CinemachineCamera : MonoBehaviour
    {

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

        private void OnEnable() {
            GameManager.OnPlayerDie += StartChangeOrtographicSizeCoroutine;
            GameManager.RestartLevel += ResetOrtographicSize;
        }

        private void OnDisable() {
            GameManager.OnPlayerDie -= StartChangeOrtographicSizeCoroutine;
            GameManager.RestartLevel -= ResetOrtographicSize;
        }

        #region Awake

        private void Awake() {
            SetComponents();
        }

        #endregion

        #region Update

        private void Update() {
            if (_shakeTimer > 0)
            {
                CalculateShakeTime();
            }
        }

        #endregion

        #endregion


        #region Custom Private Methods

        private void SetComponents() {
            _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
            _cinemachineBasicMultiChannelPerlin = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }

        private void CalculateShakeTime() {
            _shakeTimer -= Time.deltaTime;

            if (_shakeTimer <= 0.0f)
            {
                _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0.0f;
            }
        }

        private void ResetOrtographicSize(int timeScale, bool isDead) {
            _cinemachineVirtualCamera.m_Lens.OrthographicSize = 8.25f;
        }

        private void StartChangeOrtographicSizeCoroutine(int timeScale, bool isDead) {
            StartCoroutine(ChangeOrtographicSizeSmoothlyCoroutine(0.05f, 35f));
        }

        private IEnumerator ChangeOrtographicSizeSmoothlyCoroutine(float endValue, float duration) {

            yield return new WaitForSecondsRealtime(2.5f);

            float elapsed = 0.0f;
            while (elapsed < duration)
            {
                _cinemachineVirtualCamera.m_Lens.OrthographicSize = Mathf.MoveTowards(_cinemachineVirtualCamera.m_Lens.OrthographicSize, endValue, elapsed / duration);
                elapsed += Time.unscaledDeltaTime;
                yield return null;
            }
        }

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


        #endregion
    }
}