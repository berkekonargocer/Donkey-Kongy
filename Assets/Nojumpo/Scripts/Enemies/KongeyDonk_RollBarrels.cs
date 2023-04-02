using System.Collections;
using UnityEngine;

namespace Nojumpo
{
    public class KongeyDonk_RollBarrels : MonoBehaviour
    {
        [Header("ROLL BARREL SETTINGS")]
        [SerializeField] private BarrelSpawner _barrelSpawner;
        private Animator _kongeyDonkAnimator;


        // ------------------------ UNITY BUILT-IN METHODS ------------------------
        private void OnEnable() {
            Timeline.StartTheGame += StartRollingBarrels;
        }

        private void OnDisable() {
            Timeline.StartTheGame -= StartRollingBarrels;
        }

        private void Awake() {
            SetComponents();
        }


        // ------------------------ CUSTOM PRIVATE METHODS ------------------------
        private void SetComponents() {
            _kongeyDonkAnimator = GetComponent<Animator>();
        }

        private IEnumerator RollABarrelCoroutine() {
            _barrelSpawner.RollABarrel();
            _kongeyDonkAnimator.SetBool("IsBarrelThrown", true);

            int randomSecond = Random.Range(2, 5);
            yield return new WaitForSeconds(randomSecond);

            _kongeyDonkAnimator.SetBool("IsBarrelThrown", false);
        }


        // ------------------------ CUSTOM PUBLIC METHODS ------------------------
        public void StartRollingBarrels() {
            _kongeyDonkAnimator.SetBool("IsGameActive", true);
            _kongeyDonkAnimator.SetBool("IsBarrelThrown", false);
        }

        public void StopRollingBarrels() {
            _kongeyDonkAnimator.SetBool("IsGameActive", false);
        }
    }
}