using Zenject;

namespace SpaceGame
{
    public sealed class SoundServiceInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ISoundService>().To<SoundService>().AsSingle().NonLazy();
        }
    }
}