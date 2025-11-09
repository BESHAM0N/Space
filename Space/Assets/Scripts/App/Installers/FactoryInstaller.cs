using UnityEngine;
using Zenject;

namespace SpaceGame
{
    public sealed class FactoryInstaller : Installer<CardView, Transform, ListCardPrototypes, FactoryInstaller>
    {
        [Inject] private CardView _cardViewPrefab;
        [Inject] private Transform _transform;
        [Inject] private ListCardPrototypes _listCardPrototypes;

        public override void InstallBindings()
        {
            Container.Bind<Transform>().WithId("HandParent").FromInstance(_transform);
            Container.BindFactory<CardView, CardView.Factory>()
                .FromComponentInNewPrefab(_cardViewPrefab)
                .UnderTransform(_transform);

            Container.Bind<ICardFactory>().To<CardFactory>().AsSingle();
            Container.Bind<ListCardPrototypes>().FromInstance(_listCardPrototypes).AsSingle();
            Container.Bind<DeckService>().AsSingle().NonLazy();
        }
    }
}