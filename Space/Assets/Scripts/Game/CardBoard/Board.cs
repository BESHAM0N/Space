using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame
{
    public sealed class Board : IBoard
    {
        public event Action<int, ICard> OnSlotChanged;
        public int SlotsCount => _slots.Length;
        
        private readonly ICard[] _slots = new ICard[7];
        
        public bool TryPlace(int index, ICard card)
        {
            Debug.Log($"TryPlace. Index: {index}, Card: {card.DisplayName}");
            
            if (!IsIndexValid(index) || card == null || _slots[index] != null)
                return false;

            _slots[index] = card;
            OnSlotChanged?.Invoke(index, card);
            Debug.Log($"Card: {card.DisplayName} добавлена в {index} ячейку");
            return true;
        }

        public bool TryRemove(int index, out ICard removed)
        {
            Debug.Log($"TryRemove из {index} ячейки");
            
            removed = null;
            if (!IsIndexValid(index) || _slots[index] == null)
                return false;

            removed = _slots[index];
            _slots[index] = null;
            OnSlotChanged?.Invoke(index, null);
            Debug.Log($"{removed.DisplayName} удалена из {index} ячейки");
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
        
        public int GetCardIndex(ICard card)
        {
            if (card == null)
                return -1;

            for (int i = 0; i < _slots.Length; i++)
            {
                if (_slots[i] == card)
                    return i;
            }

            return -1; 
        }
        
        private bool IsIndexValid(int index) => index >= 0 && index < _slots.Length;
    }
}