using System;
using UnityEngine;

namespace Nojumpo
{
    [Serializable]
    public class Dialogue_Message
    {
        public int characterId;

        [TextArea(2,10)]
        public string message;

        [Range(0.01f, 1f)]
        [Tooltip("Wait time between characters while writing the sentence")]
        public float waitTimeBetweenChars = 0.05f;
    }
}