using System;
using System.Collections.Generic;

namespace SpaceGame
{
    public interface IBoard
    {
        int SlotsCount { get; }
        bool TryPlace(int index, ICard card);
        bool TryRemove(int index, out ICard removed);
        ICard GetCard(int index);
        IReadOnlyList<ICard> GetAllCards();
        int GetCardIndex(ICard card);
        event Action<int, ICard> OnSlotChanged;
    }
}