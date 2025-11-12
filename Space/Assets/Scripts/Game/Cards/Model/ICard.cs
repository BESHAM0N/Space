using System;
using System.Collections.Generic;
using SpaceGame;
using UnityEngine;

namespace SpaceGame
{
    /// <summary>
    /// Экземпляр карты в рантайме
    /// </summary>
    public interface ICard
    {
        string Id { get; }
        string DisplayName { get; }
        string Description { get; }
        Sprite Image { get; }
        ElementSuit Suit { get; }
        int BasePoints { get; }
        bool IsDestroyed { get; set; }
        void InitializeFromPrototype(CardPrototype prototype);

        IReadOnlyList<SuitInteractionRule> GetInteractions();
        
        void CopyFrom(ICard card);
    }
}