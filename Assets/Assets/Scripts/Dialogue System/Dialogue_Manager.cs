using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Nojumpo.Managers
{
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

        private void DisplayMessage()
        {
            Dialogue_Message messageToDisplay = _currentDialogue.dialogueMessages[_activeMessage];
            _dialogueText.text = messageToDisplay.message;

            Dialogue_Character characterToDisplay = _currentDialogue.dialogueCharacters[messageToDisplay.characterId];
            _characterNameText.text = characterToDisplay.characterName;
            _characterAvatar.sprite = characterToDisplay.characterSprite;

            IsDialogueActive = true;
        }

        #endregion

        #region Custom Public Methods

        public void OpenDialogue(Dialogue_Dialogue dialogue)
        {
            _currentDialogue= dialogue;
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
                // end the conversation
                IsDialogueActive = false;
            }
        }

        #endregion
    }
}