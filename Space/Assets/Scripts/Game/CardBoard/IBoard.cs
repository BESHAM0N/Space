using System.Collections.Generic;

namespace SpaceGame
{
    public interface IBoard
    {
        bool TryPlace(int index, ICard card);
        bool TryRemove(int index, out ICard removed);
        ICard GetCard(int index);
        event System.Action<int, ICard> OnSlotChanged;
    }
}