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
        [SerializeField]  ItemType _itemType;
        public ItemType ItemType { get { return _itemType; } }

        [Header("ANIMATION SETTINGS")]
        [SerializeField]  GameObject _uiPositionObject;
        [SerializeField]  float _animationTime = 2.5f;

        [Header("SOUND EFFECTS SETTINGS")]
        [SerializeField]  AudioSource _collectAudioSource;
        [SerializeField]  AudioClip _collectSFXAudio;


        // ------------------------ CUSTOM PRIVATE METHODS ------------------------
         void PlayCollectAnimation() {
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