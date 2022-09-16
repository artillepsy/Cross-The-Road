using Enemies.Components;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace Enemies.Spawn
{
    public class EnemyFabric : MonoBehaviour
    {
        [SerializeField] private Transform enemiesParent;
        [SerializeField] private Enemy enemyPrefab;
        public ObjectPool<Enemy> Pool { get; private set; }
        private DiContainer _container;
        
        [Inject]
        public void Construct(DiContainer container) => _container = container;

        // parameters for spawn
        public void Spawn(EnemySpawnData data)
        {
            var instance = Pool.Get();
            data.EnemyRef = instance;
            instance.SetData(data);
        }

        private Enemy CreateEnemy() => _container.InstantiatePrefabForComponent<Enemy>(enemyPrefab, enemiesParent);

        private void OnGetEnemyFromPool(Enemy enemy) => enemy.gameObject.SetActive(true);

        private void OnReturnEnemyToPool(Enemy enemy) => enemy.gameObject.SetActive(false);

        private void Awake()
        {
            Pool = new ObjectPool<Enemy>(CreateEnemy, OnGetEnemyFromPool, OnReturnEnemyToPool);
        }
    }
}