using UnityEngine;

namespace Nojumpo.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewPlayerMovementSettings", menuName = "Nojumpo/Scriptable Objects/Items/New Item Type")]
    public class ItemType : ScriptableObject
    {
        #region Fields

#if UNITY_EDITOR

        [TextArea]
        [SerializeField] private string _developerDescription;

#endif 

        #endregion
    }
}