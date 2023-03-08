using Nojumpo.Managers;

namespace Nojumpo
{
    public class RetryButton : ButtonBase
    {
        #region Custom Public Methods

        public void Retry() {
            GameManager.RestartLevel?.Invoke(1);
        }

        #endregion
    }
}