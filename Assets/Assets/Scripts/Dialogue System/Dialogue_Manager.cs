using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Nojumpo.Managers
{
    [RequireComponent(typeof(AudioSource))]
    public class Dialogue_Manager : MonoBehaviour
    {
        #region Instance

        private static Dialogue_Manager _instance;

        public static Dialogue_Manager Instance { get { return _instance; } }

        #endregion

        #region Fields

        #region Dialogue UI
        [Header("Dialogue UI")]

        [SerializeField] private Image _characterAvatar;
        [SerializeField] private TextMeshProUGUI _characterNameText;
        [SerializeField] private TextMeshProUGUI _dialogueText;

        #endregion

        #region Dialogue

        private AudioSource _dialogueAudio;
        private Dialogue_Dialogue _currentDialogue;
        private int _activeMessage = 0;

        public static bool IsDialogueActive { get; private set; } = false;

        #endregion

        #endregion



        #region Unity Methods

        #region Awake 

        private void Awake()
        {
            InitializeSingleton();
            SetComponents();
        }

        #endregion

        #endregion


        #region Custom Private Methods

        private void InitializeSingleton()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void SetComponents()
        {
            _dialogueAudio = GetComponent<AudioSource>();
        }

        private void DisplayMessage()
        {
            Dialogue_Message messageToDisplay = _currentDialogue.dialogueMessages[_activeMessage];

            Dialogue_Character characterToDisplay = _currentDialogue.dialogueCharacters[messageToDisplay.characterId];
            _characterNameText.text = characterToDisplay.characterName;
            _characterAvatar.sprite = characterToDisplay.characterSprite;

            StopAllCoroutines();
            StartCoroutine(TypeSentence(characterToDisplay, messageToDisplay.message, messageToDisplay.waitTimeBetweenChars));

            IsDialogueActive = true;
        }

        private IEnumerator TypeSentence(Dialogue_Character dialogueCharacter, string sentence, float waitTimeBetweenChars)
        {
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

        public void OpenDialogue(Dialogue_Dialogue dialogue)
        {
            _currentDialogue = dialogue;
            _activeMessage = 0;

            DisplayMessage();
        }

        public void NextMessage()
        {
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

        public void EndDialogue()
        {
            IsDialogueActive = false;
        }

        #endregion
    }
}