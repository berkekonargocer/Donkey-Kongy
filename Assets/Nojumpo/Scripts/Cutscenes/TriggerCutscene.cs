using UnityEngine;
using UnityEngine.Playables;

namespace Nojumpo
{
    public class TriggerCutscene : MonoBehaviour
    {
        #region Fields

        private PlayableDirector _endingCutscene;

        #endregion



        #region Unity Methods

        #region OnTriggerEnter2D

        private void OnTriggerEnter2D(Collider2D collision) {

            if (CollectedItems.ItemsCollection.Count != 2)
            {
                Dialogue_Trigger dialogue_Trigger = GetComponent<Dialogue_Trigger>();
                dialogue_Trigger.StartDialogue(1);
            }
            else
            {
                _endingCutscene.Play();
                //dialogue_Trigger.StartDialogue(0);
            }
        }

        #endregion

        #endregion
    }
}