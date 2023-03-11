using Nojumpo.Managers;

namespace Nojumpo.Buttons
{
    public class RetryButton : ButtonBase
    {
        #region Custom Public Methods

        public void Retry() {
            GameManager.RestartLevel?.Invoke(1, false);
        }

        #endregion
    }
}