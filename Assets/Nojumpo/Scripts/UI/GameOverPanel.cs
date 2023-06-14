using DG.Tweening;
using Nojumpo.Managers;
using UnityEngine;

namespace Nojumpo
{
    public class GameOverPanel : MonoBehaviour
    {
        [Header("COMPONENTS")]
         CanvasGroup _canvasGroup;


        // ------------------------ UNITY BUILT-IN METHODS ------------------------
         void OnEnable() {
            EventSubscriptions();
        }

         void OnDisable() {
            EventUnsubscriptions();
        }

         void Awake() {
            SetComponents();
        }


        // ------------------------ CUSTOM PRIVATE METHODS ------------------------
         void SetComponents() {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

         void EventSubscriptions() {
            GameManager.Deadge += ScaleUpAnimation;
            GameManager.Deadge += CanvasAnimation;
        }

         void EventUnsubscriptions() {
            GameManager.Deadge -= ScaleUpAnimation;
            GameManager.Deadge -= CanvasAnimation;
        }

         void ScaleUpAnimation() {
            transform.DOScale(Vector3.one, 1.5f).SetUpdate(true);
        }

         void CanvasAnimation() {
            _canvasGroup.DOFade(1, 2.5f).SetUpdate(true);
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }
    }
}