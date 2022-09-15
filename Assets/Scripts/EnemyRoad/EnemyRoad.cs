using System.Collections;
using UnityEngine;
using Zenject;

namespace EnemyRoad
{
    public class EnemyRoad : MonoBehaviour
    {
        [SerializeField] private Transform startCorner;
        [SerializeField] private Transform endCorner;
        [SerializeField] private bool drawGizmos;
        [Space]        
        [SerializeField] private EnemySpawnSettings enemySpawnSettings;
        private Collider _selfCollider;
        private EnemyFabric _fabric;
        private Vector3 _unNormDirection;
        private Quaternion _rotation;
        private EnemySpawnData _spawnData;

        [Inject]
        public void Construct(EnemyFabric fabric)
        {
            _fabric = fabric;
            Debug.Log(_fabric);
        }

        private void Awake()
        {
            _selfCollider = GetComponentInChildren<Collider>();
            _unNormDirection = (endCorner.position - startCorner.position);
            _rotation = Quaternion.LookRotation(_unNormDirection, Vector3.up);
            
            _spawnData = new EnemySpawnData(Vector3.zero, _rotation, enemySpawnSettings.EnemySpeed, _selfCollider);
            
            SpawnEnemiesInBounds();
            StartCoroutine(SpawnEnemiesCo());
        }

        private void SpawnEnemiesInBounds()
        {
            var distance = _unNormDirection.magnitude;
            Debug.Log(distance);
            
            for (var time = 0f;
                time < distance / enemySpawnSettings.EnemySpeed; 
                time += enemySpawnSettings.GetRandomTime)
            {
                _spawnData.Position = startCorner.position + _unNormDirection.normalized *
                    time * enemySpawnSettings.EnemySpeed;
                _fabric.Spawn(_spawnData);
            }
        }

        private IEnumerator SpawnEnemiesCo()
        {
            _spawnData.Position = startCorner.position;
            while (true)
            {
                _fabric.Spawn(_spawnData);
                yield return new WaitForSeconds(enemySpawnSettings.GetRandomTime);
            }
        }

        private void OnDrawGizmos()
        {
            if (!drawGizmos)
                return;
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(startCorner.position, endCorner.position);
        }
    }
}