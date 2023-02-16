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
    }
}