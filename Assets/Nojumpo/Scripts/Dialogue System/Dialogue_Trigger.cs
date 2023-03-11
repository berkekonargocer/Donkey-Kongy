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

        public void StartDialogue(int dialogueNumber) {
            Dialogue_Manager dialogue_Manager = GameObject.Find("Dialogue Manager").GetComponent<Dialogue_Manager>();
            dialogue_Manager.OpenDialogue(_dialoguesToTrigger[dialogueNumber]);
        }

        #endregion
    }
}