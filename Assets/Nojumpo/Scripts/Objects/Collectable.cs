using DG.Tweening;
using Nojumpo.Interfaces;
using UnityEngine;
using Nojumpo.ScriptableObjects;
using Nojumpo.Managers;

namespace Nojumpo
{
    public class Collectable : MonoBehaviour, ICollectable
    {
        [Header("ITEM TYPE SETTINGS")]
        [SerializeField] private ItemType _itemType;
        public ItemType ItemType { get { return _itemType; } }

        [Header("ANIMATION SETTINGS")]
        [SerializeField] private GameObject _uiPositionObject;
        [SerializeField] private float _animationTime = 2.5f;

        [Header("SOUND EFFECTS SETTINGS")]
        [SerializeField] private AudioSource _collectAudioSource;
        [SerializeField] private AudioClip _collectSFXAudio;


        // ------------------------ CUSTOM PRIVATE METHODS ------------------------
        private void PlayCollectAnimation() {
            transform.parent = _uiPositionObject.transform;
            gameObject.transform.DOLocalMove(Vector3.zero, _animationTime).SetUpdate(true);
        }


        // ------------------------ CUSTOM PUBLIC METHODS ------------------------
        public void Collect() {
            GetComponent<BoxCollider2D>().enabled = false;
            CollectedItems.ItemsCollection.Add(_itemType);
            AudioManager.Instance.PlayAudio(_collectAudioSource, _collectSFXAudio);
            PlayCollectAnimation();
        }
    }
}