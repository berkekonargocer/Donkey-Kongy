using UnityEngine;
using UnityEngine.Pool;

namespace Nojumpo
{
    public class BarrelSpawner : MonoBehaviour
    {
        [Header("BARREL OBJECT POOL SETTINGS")]
        [SerializeField]  Barrel _barrelPrefab;
        [SerializeField]  bool _checkCollection;
        [SerializeField]  int _defaultCapacity;
        [SerializeField]  int _maxCapacity;
         IObjectPool<Barrel> _barrelPool;


        // ------------------------ UNITY BUILT-IN METHODS ------------------------
         void Awake() {
            CreateBarrelPool();
        }


        // ------------------------ CUSTOM PRIVATE METHODS ------------------------
         Barrel CreateBarrel() {
            Barrel spawnBarrel = Instantiate(_barrelPrefab, transform.position, Quaternion.identity);
            spawnBarrel.SetPool(_barrelPool);

            return spawnBarrel;
        }

         void OnGetBarrel(Barrel obj) {
            obj.gameObject.SetActive(true);
            obj.transform.position = transform.position;
        }

         void OnReleaseBarrel(Barrel obj) {
            obj.gameObject.SetActive(false);
        }

         void OnDestroyBarrel(Barrel obj) {
            Destroy(obj.gameObject);
        }

         void CreateBarrelPool() {
            _barrelPool = new ObjectPool<Barrel>(CreateBarrel, OnGetBarrel, OnReleaseBarrel, OnDestroyBarrel, _checkCollection, _defaultCapacity, _maxCapacity);
        }

        // ------------------------ CUSTOM PUBLIC METHODS ------------------------
        public void RollABarrel() {
            _barrelPool.Get();
        }
    }
}