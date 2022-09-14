using Core.TouchInput;
using UnityEngine;
using Zenject;

public class TouchRegisterInstaller : MonoInstaller
{
    [SerializeField] private TouchRegister touchRegisterInstance;
    
    public override void InstallBindings()
    {
        Container.Bind<TouchRegister>().FromInstance(touchRegisterInstance).AsSingle().NonLazy();
    }
}