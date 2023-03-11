using Nojumpo.Interfaces;
using Nojumpo.Managers;
using UnityEngine;
using UnityEngine.Playables;

namespace Nojumpo
{
    public class TriggerCutscene : MonoBehaviour
    {
        #region Fields

        #region Components

        private PlayableDirector _endingCutscene;
        private Dialogue_Manager _dialogueManager;
        private Dialogue_Trigger _dialogueTrigger;

        #endregion

        #region Dialogue Variables

        private bool _isDialogueStarted = false;

        #endregion

        #endregion



        #region Unity Methods

        #region Awake

        private void Awake() {
            SetComponents();
        }

        #endregion

        #region OnTriggerEnter2D

        private void OnTriggerEnter2D(Collider2D collision) {

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

        #endregion

        #region OnTriggerExit2D

        private void OnTriggerExit2D(Collider2D collision) {
            if (!_isDialogueStarted)
            {
                _dialogueManager.StopAllCoroutines();
                _dialogueManager.StartDialogueBoxSetActiveCoroutine(false);
            }
        }

        #endregion

        #endregion


        #region Custom Private Methods

        private void SetComponents() {
            _endingCutscene = GetComponent<PlayableDirector>();
            _dialogueTrigger = GetComponent<Dialogue_Trigger>();
            _dialogueManager = GameObject.Find("Dialogue Manager").GetComponent<Dialogue_Manager>();
        }

        #endregion

    }
}