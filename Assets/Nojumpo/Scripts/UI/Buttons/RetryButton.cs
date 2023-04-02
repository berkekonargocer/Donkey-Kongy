using Nojumpo.Managers;

namespace Nojumpo.Buttons
{
    public class RetryButton : ButtonBase
    {
        public void Retry() {
            GameManager.RestartLevel?.Invoke(1, false);
        }
    }
}