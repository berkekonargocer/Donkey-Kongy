using UnityEngine;
using UnityEngine.Pool;

namespace Nojumpo
{
    public class BarrelSpawner : MonoBehaviour
    {
        [Header("BARREL OBJECT POOL SETTINGS")]
        [SerializeField] private Barrel _barrelPrefab;
        [SerializeField] private bool _checkCollection;
        [SerializeField] private int _defaultCapacity;
        [SerializeField] private int _maxCapacity;
        private IObjectPool<Barrel> _barrelPool;


        // ------------------------ UNITY BUILT-IN METHODS ------------------------
        private void Awake() {
            SetBarrelPool();
        }


        // ------------------------ CUSTOM PRIVATE METHODS ------------------------
        private Barrel CreateBarrel() {
            Barrel spawnBarrel = Instantiate(_barrelPrefab, transform.position, Quaternion.identity);
            spawnBarrel.SetPool(_barrelPool);

            return spawnBarrel; // return the object
        }

        private void OnGetBarrel(Barrel obj) {
            obj.gameObject.SetActive(true);
            obj.transform.position = transform.position;
        }

        private void OnReleaseBarrel(Barrel obj) {
            obj.gameObject.SetActive(false);
        }

        private void OnDestroyBarrel(Barrel obj) {
            Destroy(obj.gameObject);
        }

        private void SetBarrelPool() {
            _barrelPool = new ObjectPool<Barrel>(CreateBarrel, OnGetBarrel, OnReleaseBarrel, OnDestroyBarrel, _checkCollection, _defaultCapacity, _maxCapacity);
        }

        // ------------------------ CUSTOM PUBLIC METHODS ------------------------
        public void RollABarrel() {
            _barrelPool.Get();
        }
    }
}