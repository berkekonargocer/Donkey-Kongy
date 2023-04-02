using Nojumpo.Managers;
using UnityEngine;

namespace Nojumpo
{
    public class Dialogue_Trigger : MonoBehaviour
    {
        [Header("COMPONENTS")]
        [SerializeField] private Dialogue_Dialogue[] _dialoguesToTrigger;


        // ------------------------ CUSTOM PRIVATE METHODS ------------------------
        public void StartDialogue(int dialogueNumber) {
            Dialogue_Manager dialogue_Manager = GameObject.Find("Dialogue Manager").GetComponent<Dialogue_Manager>();
            dialogue_Manager.OpenDialogue(_dialoguesToTrigger[dialogueNumber]);
        }
    }
}