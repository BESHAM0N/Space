using Zenject;

namespace SpaceGame
{
    public sealed class ScoreInstaller : Installer<ScoreInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IScore>().To<Score>().AsSingle().NonLazy();
            Container.Bind<IScoreEvents>().To<ScoreEvents>().AsSingle();
            Container.BindInterfacesAndSelfTo<ScoreIncreaseObserver>().AsSingle().NonLazy();
        }
    }
}