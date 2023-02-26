using System.Collections.Generic;
using UnityEngine;
using Nojumpo.ScriptableObjects;

namespace Nojumpo
{
    public class CollectedItems : MonoBehaviour
    {
        public static Dictionary<ItemType, Collectable> ItemsCollection = new Dictionary<ItemType, Collectable>();
    }
}