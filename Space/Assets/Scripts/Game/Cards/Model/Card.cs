using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame
{
    public sealed class Card : ICard
    {
        public string Id { get; private set; }
        public string DisplayName { get; private set; }
        public string Description { get; private set; }
        public Sprite Image { get; private set; }
        public ElementSuit Suit { get; private set; }
        public int BasePoints { get; private set; }
      
        private readonly List<SuitInteractionRule> _interactions = new();

        public void InitializeFromPrototype(CardPrototype prototype)
        {
            Id = prototype.Id;
            DisplayName = prototype.DisplayName;
            Description = prototype.Description;
            Image = prototype.image;
            Suit = prototype.suit;
            BasePoints = prototype.basePoints;
          
            _interactions.Clear();
            if (prototype.interactions != null)
            {
                _interactions.AddRange(prototype.interactions);
            }
        }

        public IReadOnlyList<SuitInteractionRule> GetInteractions()
        {
            return _interactions;
        }
    }
}