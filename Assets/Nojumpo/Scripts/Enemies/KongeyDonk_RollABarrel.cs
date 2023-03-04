using System.Collections;
using UnityEngine;

namespace Nojumpo
{
    public class KongeyDonk_RollABarrel : MonoBehaviour
    {
        #region Fields

        [SerializeField] private BarrelSpawner _barrelSpawner;
        private Animator _kongeyDonkAnimator;

        #endregion



        #region Unity Methods

        #region Awake

        private void Awake()
        {
            SetComponents();
        }

        #endregion

        #endregion


        #region Custom Private Methods

        private void SetComponents()
        {
            _kongeyDonkAnimator = GetComponent<Animator>();
        }

        private IEnumerator RollABarrelCoroutine()
        {
            _barrelSpawner.RollABarrel();
            _kongeyDonkAnimator.SetBool("IsBarrelThrown", true);

            int randomSecond = Random.Range(2, 5);
            yield return new WaitForSeconds(randomSecond);

            _kongeyDonkAnimator.SetBool("IsBarrelThrown", false);
        }

        #endregion

        #region Custom Public Methods

        public void StartRollingBarrels()
        {
            _kongeyDonkAnimator.SetBool("IsBarrelThrown", false);
        }

        #endregion
    }
}