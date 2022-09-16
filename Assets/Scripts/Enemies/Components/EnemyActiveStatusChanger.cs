using Enemies.Spawn;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace Enemies.Components
{
    public class EnemyActiveStatusChanger : EnemyComponent
    {
        private Enemy _selfReference;
        private IObjectPool<Enemy> _pool;
        private Collider _roadCollider;

        [Inject]
        public void Construct(EnemyFabric fabric) => _pool = fabric.Pool;
        
        public override void SetData(EnemySpawnData data)
        {
            _roadCollider = data.DestinationCollider;
            _selfReference = data.EnemyRef;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other == _roadCollider)
            {
                _pool.Release(_selfReference);
            }
        }
    }
}