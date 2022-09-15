using Core.Observable;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;
using Player;

namespace EnemyRoad
{
    public class Enemy : MonoBehaviour, IObserver<float>
    {
        private EnemyMovementManager _enemyMovementManager;
        private IObjectPool<Enemy> _pool;
        private PlayerComponents _player;
        private Collider _roadCollider;
        private Rigidbody _rb;
        private float _speed;
        
        [Inject]
        public void Construct(EnemyMovementManager manager, PlayerComponents playerComponents)
        {
            _player = playerComponents;
            _enemyMovementManager = manager;
        }

        public void OnNotify(float time)
        {
            _rb.MovePosition(transform.position + transform.forward * (_speed * time));
        }

        public void SetData(in EnemySpawnData data, IObjectPool<Enemy> pool)
        {
            _pool = pool;
            _roadCollider = data.DestinationCollider;
            _speed = data.Speed;
            transform.position = data.Position;
            transform.rotation = data.Rotation;
        }

        private void Awake() =>_rb = GetComponent<Rigidbody>();

        private void OnEnable() => _enemyMovementManager.AddObserver(this);

        private void OnDisable() => _enemyMovementManager.RemoveObserver(this);

        private void OnTriggerEnter(Collider other)
        {
            if (other == _roadCollider)
            {
                Debug.Log("ok");
                _pool.Release(this);
            }
            else if (other == _player.Collider)
            {
                _player.Health.Kill();
            }
        }
    }
}