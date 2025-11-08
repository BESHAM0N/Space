using Zenject;

namespace SpaceGame
{
    public sealed class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            ScoreInstaller.Install(Container);
            Container.BindInterfacesAndSelfTo<ScoreIncreaseObserver>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameOverObserver>().AsSingle().NonLazy();
        }
    }
}