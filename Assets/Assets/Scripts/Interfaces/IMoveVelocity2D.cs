using UnityEngine;

namespace Nojumpo.Interfaces
{
    public interface IMoveVelocity2D
    {
        public Vector2 GetVelocity();
        public void SetVelocity(Vector2 velocity);
        public void VelocityPlusEquals(Vector2 velocity);
        public void SetVelocityX(float moveVelocityX);
        public void SetVelocityY(float moveVelocityY);
        public void MultiplyVelocityX(float moveVelocityX);
        public void MultiplyVelocityY(float moveVelocityY);
    }
}