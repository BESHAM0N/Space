using System;
using System.Collections.Generic;

namespace SpaceGame
{
    public sealed class Board : IBoard
    {
        private readonly ICard[] _slots = new ICard[7];
        private List<BoardSlot> _boardSlots;
        public event Action<int, ICard> OnSlotChanged;

        public bool TryPlace(int index, ICard card)
        {
            if (!IsIndexValid(index) || card == null || _slots[index] != null)
                return false;

            _slots[index] = card;
            OnSlotChanged?.Invoke(index, card);
            return true;
        }

        public bool TryRemove(int index, out ICard removed)
        {
            removed = null;
            if (!IsIndexValid(index) || _slots[index] == null)
                return false;

            removed = _slots[index];
            _slots[index] = null;
            OnSlotChanged?.Invoke(index, null);
            return true;
        }

        public ICard GetCard(int index)
        {
            if (!IsIndexValid(index)) 
                return null;
            
            return _slots[index];
        }
        
        public IReadOnlyList<ICard> GetAllCards()
        {
            var list = new List<ICard>(_slots.Length);
            
            for (int i = 0; i < _slots.Length; i++)
                if (_slots[i] != null) list.Add(_slots[i]);
            
            return list;
        }
        
        private bool IsIndexValid(int index) => index >= 0 && index < _slots.Length;
    }
}