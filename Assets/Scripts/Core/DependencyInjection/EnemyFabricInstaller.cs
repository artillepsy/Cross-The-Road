using EnemyRoad;
using UnityEngine;
using Zenject;

namespace Core.DependencyInjection
{
    public class EnemyFabricInstaller : MonoInstaller
    {
        [SerializeField] private Transform singletonsParent;
        [SerializeField] private EnemyFabric enemyFabricPrefab;
        
        public override void InstallBindings()
        {
            var instance = Container.InstantiatePrefabForComponent<EnemyFabric>(
                enemyFabricPrefab, Vector3.zero, Quaternion.identity, singletonsParent);

            Container.Bind<EnemyFabric>().FromInstance(instance).AsSingle().NonLazy();
        }
    }
}