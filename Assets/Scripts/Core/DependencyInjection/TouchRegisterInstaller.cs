using Core.TouchInput;
using UnityEngine;
using Zenject;

namespace Core.DependencyInjection
{
    public class TouchRegisterInstaller : MonoInstaller
    {
        [SerializeField] private Transform singletonsParent;
        [SerializeField] private TouchRegister touchRegisterPrefab;
    
        public override void InstallBindings()
        {
            var instance = Container.InstantiatePrefabForComponent<TouchRegister>(
                touchRegisterPrefab, Vector3.zero, Quaternion.identity, singletonsParent);

            Container.Bind<TouchRegister>().FromInstance(instance).AsSingle().NonLazy();
        }
    }
}