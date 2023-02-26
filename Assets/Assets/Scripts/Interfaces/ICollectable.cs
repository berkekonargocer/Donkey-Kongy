using Nojumpo.ScriptableObjects;

namespace Nojumpo.Interfaces
{
    public interface ICollectable
    {
        public ItemType ItemType { get; } 
        public void Collect();
    }
}