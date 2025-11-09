using Zenject;

namespace SpaceGame
{
    public class LevelCircleInstaller : Installer<LevelCircleInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<ILevelEndUI>().To<LevelCompletePopupView>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<HandService>().AsSingle();
            Container.Bind<ISceneLoader>().To<UnitySceneLoader>().AsSingle();
            Container.BindInterfacesAndSelfTo<LevelFlowController>().AsSingle();
        }
    }
}