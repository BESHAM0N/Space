using Zenject;

namespace SpaceGame
{
    public sealed class LeaderboardInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ILeaderboard>().To<Leaderboard>().AsSingle().NonLazy();
        }
    }
}