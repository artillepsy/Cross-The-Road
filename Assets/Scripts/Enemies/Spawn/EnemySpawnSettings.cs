using UnityEngine;

namespace Enemies.Spawn
{
    [CreateAssetMenu(fileName = "EnemySpawnSettings")]
    public class EnemySpawnSettings : ScriptableObject
    {
        [SerializeField] private float enemySpeed = 3f;
        [Min(0)]
        [SerializeField] private float minSpawnRateSec = 1f;
        [Min(0)]
        [SerializeField] private float maxSpawnRateSec = 2f;

        public float EnemySpeed => enemySpeed;
        public float GetRandomTime => Random.Range(minSpawnRateSec, maxSpawnRateSec);
    }
}