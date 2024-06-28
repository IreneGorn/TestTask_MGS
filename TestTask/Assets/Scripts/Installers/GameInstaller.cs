using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public GameObject cubePrefab;
    
    public override void InstallBindings()
    {
        Container.Bind<IInputService>().To<InputService>().AsSingle();
        Container.Bind<ICubeGenerator>().To<CubeGenerator>().AsSingle().WithArguments(cubePrefab);
        Container.Bind<ZoneManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<ICubeChecker>().To<CubeChecker>().AsSingle();
        Container.Bind<ICubeInteractor>().To<CubeInteractor>().AsSingle();
        Container.Bind<GameManager>().FromNewComponentOnNewGameObject().AsSingle();
    }
}