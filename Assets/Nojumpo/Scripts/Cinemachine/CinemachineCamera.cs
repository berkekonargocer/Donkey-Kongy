using Cinemachine;
using System.Collections;
using UnityEngine;
using Nojumpo.Managers;

namespace Nojumpo
{
    public class CinemachineCamera : MonoBehaviour
    {
        [Header("COMPONENTS")]
        CinemachineVirtualCamera _cinemachineVirtualCamera;
        CinemachineBasicMultiChannelPerlin _cinemachineBasicMultiChannelPerlin;

        [Header("CAMERA SHAKE SETTINGS")]
        float _shakeTimer;


        // ------------------------ UNITY BUILT-IN METHODS ------------------------
        void OnEnable() {
            GameManager.OnPlayerDie += StartChangeOrtographicSizeCoroutine;
            GameManager.RestartLevel += ResetOrtographicSize;
        }

        void OnDisable() {
            GameManager.OnPlayerDie -= StartChangeOrtographicSizeCoroutine;
            GameManager.RestartLevel -= ResetOrtographicSize;
        }

        void Awake() {
            SetComponents();
        }

        void Update() {
            if (_shakeTimer > 0)
            {
                CalculateShakeTime();
            }
        }


        // ------------------------ CUSTOM PRIVATE METHODS ------------------------
        void SetComponents() {
            _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
            _cinemachineBasicMultiChannelPerlin = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }

        void CalculateShakeTime() {
            _shakeTimer -= Time.deltaTime;

            if (_shakeTimer <= 0.0f)
            {
                _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0.0f;
            }
        }

        void ResetOrtographicSize(int timeScale, bool isDead) {
            _cinemachineVirtualCamera.m_Lens.OrthographicSize = 8.25f;
        }

        void StartChangeOrtographicSizeCoroutine(int timeScale, bool isDead) {
            StartCoroutine(ChangeOrtographicSizeSmoothlyCoroutine(0.05f, 35f));
        }

        IEnumerator ChangeOrtographicSizeSmoothlyCoroutine(float endValue, float duration) {

            yield return new WaitForSecondsRealtime(2.5f);

            float elapsed = 0.0f;
            while (elapsed < duration)
            {
                Vector3 position = transform.position;
                _cinemachineVirtualCamera.transform.position = Vector3.MoveTowards(position, new Vector3(position.x, -1.09f, position.z), elapsed / duration);
                _cinemachineVirtualCamera.m_Lens.OrthographicSize = Mathf.MoveTowards(_cinemachineVirtualCamera.m_Lens.OrthographicSize, endValue, elapsed / duration);
                elapsed += Time.unscaledDeltaTime;
                yield return null;
            }
        }


        // ------------------------ CUSTOM PUBLIC METHODS ------------------------
        public void ShakeCamera(float intensity) {
            _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
            _shakeTimer = 0.2f;
        }

        public void ShakeCamera(float intensity, float time) {
            _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
            _shakeTimer = time;
        }
    }
}
