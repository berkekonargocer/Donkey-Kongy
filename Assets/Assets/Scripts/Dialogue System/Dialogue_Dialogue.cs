using System;

namespace Nojumpo
{
    [Serializable]
    public class Dialogue_Dialogue
    {
        public string dialogueName;
        public Dialogue_Message[] dialogueMessages;
        public Dialogue_Character[] dialogueCharacters;
    }
}