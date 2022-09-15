using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace EnemyRoad
{
    public class EnemyFabric : MonoBehaviour
    {
        [SerializeField] private Transform enemiesParent;
        [SerializeField] private Enemy enemyPrefab;
        private ObjectPool<Enemy> _pool;
        private DiContainer _container;
        
        [Inject]
        public void Construct(DiContainer container) => _container = container;

        // parameters for spawn
        public void Spawn(in EnemySpawnData data) => _pool.Get().SetData(data, _pool);

        private Enemy CreateEnemy() => _container.InstantiatePrefabForComponent<Enemy>(enemyPrefab, enemiesParent);

        private void OnGetEnemyFromPool(Enemy enemy) => enemy.gameObject.SetActive(true);

        private void OnReturnEnemyToPool(Enemy enemy) => enemy.gameObject.SetActive(false);

        private void Awake()
        {
            _pool = new ObjectPool<Enemy>(CreateEnemy, OnGetEnemyFromPool, OnReturnEnemyToPool);
        }
    }
}