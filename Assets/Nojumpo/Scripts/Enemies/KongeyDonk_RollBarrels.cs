using System.Collections;
using UnityEngine;

namespace Nojumpo
{
    public class KongeyDonk_RollBarrels : MonoBehaviour
    {
        [Header("ROLL BARREL SETTINGS")]
        [SerializeField]  BarrelSpawner _barrelSpawner;
         Animator _kongeyDonkAnimator;


        // ------------------------ UNITY BUILT-IN METHODS ------------------------
         void OnEnable() {
            Timeline.StartTheGame += StartRollingBarrels;
        }

         void OnDisable() {
            Timeline.StartTheGame -= StartRollingBarrels;
        }

         void Awake() {
            SetComponents();
        }


        // ------------------------ CUSTOM PRIVATE METHODS ------------------------
         void SetComponents() {
            _kongeyDonkAnimator = GetComponent<Animator>();
        }

         IEnumerator RollABarrelCoroutine() {
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