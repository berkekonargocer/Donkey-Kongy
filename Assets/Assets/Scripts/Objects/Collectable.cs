using DG.Tweening;
using Nojumpo.Interfaces;
using UnityEngine;
using Nojumpo.ScriptableObjects;

namespace Nojumpo
{
    public class Collectable : MonoBehaviour, ICollectable
    {
        #region Fields

        [SerializeField] private ItemType _itemType;
        public ItemType ItemType { get { return _itemType; } }

        #region Animation Settings
        [Header("Animation Settings")]

        [SerializeField] private GameObject _uiPositionObject;
        [SerializeField] private float _animationSpeed = 50.0f;
        [SerializeField] private float _endAnimationAfterSeconds = 2.0f;
        private Vector3 _smoothDampVelocity = Vector3.zero;

        #endregion

        #region Sound Effects Settings
        [Header("Sound Effects Settings")]

        [SerializeField] private AudioSource _collectAudioSource;
        [SerializeField] private AudioClip _collectSFXAudio;

        #endregion


        #region 

        #endregion

        #endregion



        #region Custom Private Methods

        private void PlayCollectAudio()
        {
            _collectAudioSource.clip = _collectSFXAudio;
            _collectAudioSource.Play();
        }

        private void PlayCollectAnimation()
        {
            transform.parent = _uiPositionObject.transform;

            gameObject.transform.DOLocalMove(Vector3.zero, 2.5f);
        }

        #endregion

        #region Custom Public Methods

        public void Collect()
        {
            PlayCollectAudio();
            PlayCollectAnimation();
        }

        #endregion
    }
}