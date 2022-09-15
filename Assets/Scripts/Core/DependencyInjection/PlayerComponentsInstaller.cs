using Player;
using UnityEngine;
using Zenject;

namespace Core.DependencyInjection
{
    public class PlayerComponentsInstaller : MonoInstaller
    {
        [SerializeField] private PlayerComponents playerInstance;
        
        public override void InstallBindings()
        {
            Container.Bind<PlayerComponents>().FromInstance(playerInstance).AsSingle();
        }
    }
}