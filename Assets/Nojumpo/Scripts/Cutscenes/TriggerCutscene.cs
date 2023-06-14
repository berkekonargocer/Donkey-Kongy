using Nojumpo.Interfaces;
using Nojumpo.Managers;
using UnityEngine;
using UnityEngine.Playables;

namespace Nojumpo
{
    public class TriggerCutscene : MonoBehaviour
    {
        [Header("COMPONENTS")]
         PlayableDirector _endingCutscene;
         Dialogue_Manager _dialogueManager;
         Dialogue_Trigger _dialogueTrigger;

        [Header("DIALOGUE SETTINGS")]
         bool _isDialogueStarted = false;


        // ------------------------ UNITY BUILT-IN METHODS ------------------------
         void Awake() {
            SetComponents();
        }

         void OnTriggerEnter2D(Collider2D collision) {

            if (CollectedItems.ItemsCollection.Count == 2)
            {
                collision.GetComponent<IMoveVelocity2D>().SetVelocity(Vector2.zero);
                _isDialogueStarted = true;
                _endingCutscene.Play();
            }
            else
            {
                _dialogueTrigger.StartDialogue(1);
                _dialogueManager.StartDialogueBoxSetActiveCoroutine(true);
            }
        }

         void OnTriggerExit2D(Collider2D collision) {
            if (!_isDialogueStarted)
            {
                _dialogueManager.StopAllCoroutines();
                _dialogueManager.StartDialogueBoxSetActiveCoroutine(false);
            }
        }


        // ------------------------ CUSTOM PRIVATE METHODS ------------------------
         void SetComponents() {
            _endingCutscene = GetComponent<PlayableDirector>();
            _dialogueTrigger = GetComponent<Dialogue_Trigger>();
            _dialogueManager = GameObject.Find("Dialogue Manager").GetComponent<Dialogue_Manager>();
        }
    }
}