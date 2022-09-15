using EnemyRoad;
using UnityEngine;
using Zenject;

namespace Core.DependencyInjection
{
    public class EnemyMovementManagerInstaller : MonoInstaller
    {
        [SerializeField] private Transform singletonsParent;
        [SerializeField] private EnemyMovementManager movementManagerPrefab;
        
        public override void InstallBindings()
        {
            var instance = Container.InstantiatePrefabForComponent<EnemyMovementManager>(
                movementManagerPrefab, Vector3.zero, Quaternion.identity, singletonsParent);

            Container.Bind<EnemyMovementManager>().FromInstance(instance).AsSingle().NonLazy();
        }
    }
}