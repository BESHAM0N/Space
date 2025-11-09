using UnityEngine;
using Zenject;

namespace SpaceGame
{
    public sealed class GameInstaller : MonoInstaller
    {
        [Header("Cards")] [SerializeField] private CardView _cardViewPrefab;
        [SerializeField] private Transform _handParent;
        [SerializeField] private ListCardPrototypes _prototypes;

        public override void InstallBindings()
        {
            LevelCircleInstaller.Install(Container);
            ScoreInstaller.Install(Container);
            FactoryInstaller.Install(Container, _cardViewPrefab, _handParent, _prototypes);
            Container.BindInterfacesAndSelfTo<GameOverObserver>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<LevelManager>().AsSingle().NonLazy();
        }
    }
}