using DG.Tweening;
using Nojumpo.Managers;
using UnityEngine;

namespace Nojumpo
{
    public class GameOverPanel : MonoBehaviour
    {
        [Header("COMPONENTS")]
        private CanvasGroup _canvasGroup;


        // ------------------------ UNITY BUILT-IN METHODS ------------------------
        private void OnEnable() {
            EventSubscriptions();
        }

        private void OnDisable() {
            EventUnsubscriptions();
        }

        private void Awake() {
            SetComponents();
        }


        // ------------------------ CUSTOM PRIVATE METHODS ------------------------
        private void SetComponents() {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        private void EventSubscriptions() {
            GameManager.Deadge += ScaleUpAnimation;
            GameManager.Deadge += CanvasAnimation;
        }

        private void EventUnsubscriptions() {
            GameManager.Deadge -= ScaleUpAnimation;
            GameManager.Deadge -= CanvasAnimation;
        }

        private void ScaleUpAnimation() {
            transform.DOScale(Vector3.one, 1.5f).SetUpdate(true);
        }

        private void CanvasAnimation() {
            _canvasGroup.DOFade(1, 2.5f).SetUpdate(true);
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }
    }
}