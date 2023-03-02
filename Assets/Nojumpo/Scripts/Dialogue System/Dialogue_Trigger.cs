using Nojumpo.Managers;
using UnityEngine;

namespace Nojumpo
{
    public class Dialogue_Trigger : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Dialogue_Dialogue[] _dialoguesToTrigger;

        #endregion

        #region Custom Public Methods

        public void StartDialogue(int dialogueNumber)
        {
            Dialogue_Manager.Instance.OpenDialogue(_dialoguesToTrigger[dialogueNumber]);
        }

        #endregion
    }
}