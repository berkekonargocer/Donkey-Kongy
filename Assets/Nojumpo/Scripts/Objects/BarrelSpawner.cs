using UnityEngine;
using UnityEngine.Pool;

namespace Nojumpo
{
    public class BarrelSpawner : MonoBehaviour
    {
        #region Fields

        #region Spawn Barrel Object Pool

        [Header("Spawn Barrel Object Pool")]
        [SerializeField] private Barrel _barrelPrefab;

        [SerializeField] private bool _checkCollection;
        [SerializeField] private int _defaultCapacity;
        [SerializeField] private int _maxCapacity;

        private IObjectPool<Barrel> _barrelPool;

        #endregion

        #endregion


        #region Unity Methods

        #region Awake

        private void Awake()
        {
            _barrelPool = new ObjectPool<Barrel>(CreateBarrel, OnGetBarrel, OnReleaseBarrel, OnDestroyBarrel, _checkCollection, _defaultCapacity, _maxCapacity);
        }

        #endregion

        #region Custom Private Methods

        private Barrel CreateBarrel()
        {
            Barrel spawnBarrel = Instantiate (_barrelPrefab, transform.position, Quaternion.identity);
            spawnBarrel.SetPool(_barrelPool);

            return spawnBarrel; // return the object
        }

        private void OnGetBarrel(Barrel obj)
        {
            obj.gameObject.SetActive(true);
            obj.transform.position = transform.position;
        }

        private void OnReleaseBarrel(Barrel obj)
        {
            obj.gameObject.SetActive(false);
        }

        private void OnDestroyBarrel(Barrel obj)
        {
            Destroy(obj.gameObject);
        }

        #endregion

        #region Custom Public Methods

        public void RollABarrel()
        {
            _barrelPool.Get();
        }

        #endregion

        #endregion
    }
}