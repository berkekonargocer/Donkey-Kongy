using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Nojumpo.Managers
{
    [RequireComponent(typeof(AudioSource))]
    public class Dialogue_Manager : MonoBehaviour
    {
        [Header("DIALOGUE UI")]
        [SerializeField]  GameObject _dialogueBox;
        [SerializeField]  Image _characterAvatar;
        [SerializeField]  TextMeshProUGUI _characterNameText;
        [SerializeField]  TextMeshProUGUI _dialogueText;
         RectTransform _dialogueBoxRectTransform;

        [Header("DIALOGUE SETTINGS")]
         AudioSource _dialogueAudio;
         Dialogue_Dialogue _currentDialogue;
         int _activeMessage = 0;
         Vector3 _normalScale = new Vector3(0.35f, 0.15f, 1.0f);
        public static bool IsDialogueActive { get;  set; } = false;


        // ------------------------ UNITY BUILT-IN METHODS ------------------------
         void Awake() {
            SetComponents();
        }


        // ------------------------ CUSTOM PRIVATE METHODS ------------------------
         void SetComponents() {
            _dialogueAudio = GetComponent<AudioSource>();
            _dialogueBoxRectTransform = _dialogueBox.GetComponent<RectTransform>();
        }

         void DisplayMessage() {
            Dialogue_Message messageToDisplay = _currentDialogue.dialogueMessages[_activeMessage];

            Dialogue_Character characterToDisplay = _currentDialogue.dialogueCharacters[messageToDisplay.characterId];
            _characterNameText.text = characterToDisplay.characterName;
            _characterAvatar.sprite = characterToDisplay.characterSprite;

            StopAllCoroutines();
            StartCoroutine(TypeSentence(characterToDisplay, messageToDisplay.message, messageToDisplay.waitTimeBetweenChars));

            IsDialogueActive = true;
        }

         IEnumerator TypeSentence(Dialogue_Character dialogueCharacter, string sentence, float waitTimeBetweenChars) {
            _dialogueText.text = "";

            foreach (char letter in sentence.ToCharArray())
            {
                _dialogueText.text += letter;
                _dialogueAudio.Stop();
                int randomClip = Random.Range(0, dialogueCharacter.talkingSFX.Length);
                _dialogueAudio.clip = dialogueCharacter.talkingSFX[randomClip];
                _dialogueAudio.Play();
                yield return new WaitForSeconds(waitTimeBetweenChars);
            }
        }

         IEnumerator DialogueBoxSetActive(bool isActive) {

            if (isActive == true)
            {
                _dialogueBox.SetActive(isActive);
                _dialogueBoxRectTransform.DOScale(_normalScale, 0.2f);
            }
            else
            {
                _dialogueBoxRectTransform.DOScale(Vector3.zero, 0.2f);
                yield return new WaitForSeconds(0.2f);
                _dialogueBox.SetActive(isActive);
            }
        }


        // ------------------------ CUSTOM PUBLIC METHODS ------------------------
        public void OpenDialogue(Dialogue_Dialogue dialogue) {
            _currentDialogue = dialogue;
            _activeMessage = 0;

            DisplayMessage();
        }

        public void NextMessage() {
            _activeMessage++;
            if (_activeMessage < _currentDialogue.dialogueMessages.Length)
            {
                DisplayMessage();
            }
            else
            {
                EndDialogue();
            }
        }

        public void EndDialogue() {
            StopAllCoroutines();
            IsDialogueActive = false;
        }

        public void StartDialogueBoxSetActiveCoroutine(bool isActive) {
            StartCoroutine(DialogueBoxSetActive(isActive));
        }
    }
}