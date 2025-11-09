using System.Collections.Generic;
using Zenject;

namespace SpaceGame
{
    public sealed class LevelManager : IInitializable
    {
        private readonly DeckService _deck;
        private readonly ICardFactory _factory;

        private List<Card> _levelCards;
        private List<CardView> _views;

        public LevelManager(DeckService deck, ICardFactory factory)
        {
            _deck = deck;
            _factory = factory;
        }

        public void Initialize()
        {
            _levelCards = _deck.DealRandom(10);
            _views = _factory.CreateViews(_levelCards);
        }
    }
}