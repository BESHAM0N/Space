using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame
{
    public sealed class BoardController : MonoBehaviour
    {
        public IBoard Board => _board;
        
        [SerializeField] private List<BoardSlotView> _slotViews;
        [SerializeField] private RectTransform _boardArea;
        [SerializeField] private LevelCompletePopupView _level;
        private IBoard _board { get; set; }

        private void Awake()
        {
            _board = new Board();
            _board.OnSlotChanged += OnBoardSlotChanged;
            _level.NextClicked += ClearAllSlots;

            for (int i = 0; i < _slotViews.Count; i++)
            {
                _slotViews[i].BindModel(_board.GetCard(i));
            }
        }
        
        public CardView GetView(int index)
        {
            if (index < 0 || index >= _slotViews.Count) return null;
            var slot = _slotViews[index];
            if (!slot) return null;

            return slot.GetComponentInChildren<CardView>(true);
        }

        private void OnDestroy()
        {
            if (_board != null)
                _board.OnSlotChanged -= OnBoardSlotChanged;
            
            _level.NextClicked -= ClearAllSlots;
        }

        private void OnBoardSlotChanged(int index, ICard newCard)
        {
            if (index >= 0 && index < _slotViews.Count)
                _slotViews[index].BindModel(newCard);
        }

        public bool TryPlaceFromUI(int slotIndex, CardView cardView)
        {
            if (cardView == null || cardView.Card == null)
                return false;

            // Слот занят? 
            if (_board.GetCard(slotIndex) != null)
            {
                return false;
            }

            // Положить в модель
            if (!_board.TryPlace(slotIndex, cardView.Card))
            {
                return false;
            }
          
            // Визуально – перепривязать вью к слоту
            ((RectTransform)cardView.transform).SetParent((RectTransform)_slotViews[slotIndex].transform, false);
            return true;
        }

        public void TryRemoveFromUI(int slotIndex)
        {
            //Удаляем карту из модели
            if (_board.TryRemove(slotIndex, out _))
            {
                _slotViews[slotIndex].BindModel(null);
            }
            else
            {
                Debug.Log($"Не удалось удалить карту из слота {slotIndex} (возможно, он уже пуст)");
            }
        }
        
        public void ClearAllSlots()
        {
            if (_board == null) return;

            // 1) Модель: очистить все слоты
            for (int i = 0; i < _board.SlotsCount; i++)
            {
                if (_board.GetCard(i) != null)
                    _board.TryRemove(i, out _);
            }

            // 2) Визуал: удалить CardView из каждого слота
            for (int i = 0; i < _slotViews.Count; i++)
            {
                var slot = _slotViews[i];
                if (!slot) continue;

                // найти CardView, лежащий как ребёнок слота
                var view = slot.GetComponentInChildren<CardView>(true);
                if (view != null)
                    Destroy(view.gameObject);

                // 3) Обновить подсветку "пусто"
                slot.BindModel(null);
            }
        }
    }
}