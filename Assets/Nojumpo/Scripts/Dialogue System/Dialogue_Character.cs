using System;
using UnityEngine;

namespace Nojumpo
{
    [Serializable]
    public class Dialogue_Character
    {
        public string characterName;
        public Sprite characterSprite;
        public AudioClip[] talkingSFX;
    }
}