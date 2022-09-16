using System.Collections;
using Enemies.Spawn;
using UnityEngine;
using Zenject;

namespace Enemies.Movement
{
    [RequireComponent(typeof(EnemyPath))]
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemyPath path;
        [SerializeField] private EnemySpawnSettings enemySpawnSettings;
        private EnemyFabric _fabric;
        private EnemySpawnData _spawnData;
        
        [Inject]
        public void Construct(EnemyFabric fabric) => _fabric = fabric;

        private void Start()
        {
            var rotation = Quaternion.LookRotation(path.UnNormDirection, Vector3.up);
            _spawnData = new EnemySpawnData(Vector3.zero,
                rotation, enemySpawnSettings.EnemySpeed, path.SelfCollider);
            
            SpawnEnemiesInBounds();
            StartCoroutine(SpawnEnemiesCo());
        }

        private void SpawnEnemiesInBounds()
        {
            var distance = path.UnNormDirection.magnitude;
            Debug.Log(distance);
            
            for (var time = 0f;
                time < distance / enemySpawnSettings.EnemySpeed; 
                time += enemySpawnSettings.GetRandomTime)
            {
                _spawnData.Position = path.StartPos + path.UnNormDirection.normalized *
                    time * enemySpawnSettings.EnemySpeed;
                _fabric.Spawn(_spawnData);
            }
        }

        private IEnumerator SpawnEnemiesCo()
        {
            _spawnData.Position = path.StartPos;
            while (true)
            {
                _fabric.Spawn(_spawnData);
                yield return new WaitForSeconds(enemySpawnSettings.GetRandomTime);
            }
        }
        
    }
}