using UnityEngine;
using Zenject;

namespace Core.DependencyInjection
{
    public class CameraInstaller : MonoInstaller
    {
        [SerializeField] private Camera cameraInstance;

        public override void InstallBindings()
        {
            Container.Bind<Camera>().FromInstance(cameraInstance).AsSingle();
        }
    }
}