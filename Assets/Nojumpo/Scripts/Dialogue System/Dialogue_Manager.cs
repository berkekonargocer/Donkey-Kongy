using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Threading.Tasks;

namespace Nojumpo.Managers
{
    [RequireComponent(typeof(AudioSource))]
    public class Dialogue_Manager : MonoBehaviour
    {

        #region Fields

        #region Dialogue UI
        [Header("Dialogue UI")]

        [SerializeField] private GameObject _dialogueBox;
        [SerializeField] private Image _characterAvatar;
        [SerializeField] private TextMeshProUGUI _characterNameText;
        [SerializeField] private TextMeshProUGUI _dialogueText;

        private RectTransform _dialogueBoxRectTransform;

        #endregion

        #region Dialogue

        private AudioSource _dialogueAudio;
        private Dialogue_Dialogue _currentDialogue;
        private int _activeMessage = 0;

        public static bool IsDialogueActive { get; private set; } = false;

        #endregion

        #region Dialogue Box Animation

        private Vector3 _normalScale = new Vector3(0.35f, 0.15f, 1.0f);

        #endregion

        #endregion



        #region Unity Methods

        #endregion

        #region Awake 

        private void Awake() {
            SetComponents();
        }

        #endregion


        #region Custom Private Methods

        private void SetComponents() {
            _dialogueAudio = GetComponent<AudioSource>();
            _dialogueBoxRectTransform = _dialogueBox.GetComponent<RectTransform>();
        }

        private void DisplayMessage() {
            Dialogue_Message messageToDisplay = _currentDialogue.dialogueMessages[_activeMessage];

            Dialogue_Character characterToDisplay = _currentDialogue.dialogueCharacters[messageToDisplay.characterId];
            _characterNameText.text = characterToDisplay.characterName;
            _characterAvatar.sprite = characterToDisplay.characterSprite;

            StopAllCoroutines();
            StartCoroutine(TypeSentence(characterToDisplay, messageToDisplay.message, messageToDisplay.waitTimeBetweenChars));

            IsDialogueActive = true;
        }

        private IEnumerator TypeSentence(Dialogue_Character dialogueCharacter, string sentence, float waitTimeBetweenChars) {
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

        #endregion

        #region Custom Public Methods

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

        public async Task DialogueBoxSetActive(bool isActive) {

            if (isActive == true)
            {
                _dialogueBox.SetActive(isActive);
                _dialogueBoxRectTransform.DOScale(_normalScale, 0.2f);
            }
            else
            {
                _dialogueBoxRectTransform.DOScale(Vector3.zero, 0.2f);
                await Task.Delay(200);
                _dialogueBox.SetActive(isActive);
            }
        }

        #endregion
    }
}