using UnityEngine;
using Zenject;

public class BootstrapInstaller : MonoInstaller
{
    [SerializeField] private SwipeSystem _swipeSystem;
    
    public override void InstallBindings()
    {
        BindSwipeSystemWithCar();
    }

    private void BindSwipeSystemWithCar()
    {
        Container.Bind<SwipeSystem>().FromInstance(_swipeSystem).AsSingle();
    }
}