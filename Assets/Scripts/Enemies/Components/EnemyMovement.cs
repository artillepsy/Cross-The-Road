using Core.Observable;
using Enemies.Movement;
using Enemies.Spawn;
using UnityEngine;
using Zenject;

namespace Enemies.Components
{
    public class EnemyMovement : EnemyComponent, IObserver<float>
    {
        private EnemyMovementManager _movementManager;
        private Rigidbody _rb;
        private float _speed;

        [Inject]
        public void Construct(EnemyMovementManager manager) => _movementManager = manager;

        public override void SetData(EnemySpawnData data)
        {
            _speed = data.Speed;
            transform.position = data.Position;
            transform.rotation = data.Rotation;
        }

        private void Awake() =>_rb = GetComponent<Rigidbody>();

        private void OnEnable() => _movementManager.AddObserver(this);

        private void OnDisable() => _movementManager.RemoveObserver(this);

        public void OnNotify(float time)
        {
            _rb.MovePosition(transform.position + transform.forward * (_speed * time));
        }
    }
}