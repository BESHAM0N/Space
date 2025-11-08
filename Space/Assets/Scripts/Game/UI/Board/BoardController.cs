using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame
{
    public sealed class BoardController : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private Transform _slotsParent;
        [SerializeField] private bool _useChildIndexAsSlotIndex = true;
        [SerializeField] private List<BoardSlotView> _slotViews;

        // Модель доски
        private IBoard Board { get;  set; }
      
        private void Awake()
        {
            Board = new Board();
            Board.OnSlotChanged += OnBoardSlotChanged;
        }

        private void OnDestroy()
        {
            if (Board != null) 
                Board.OnSlotChanged -= OnBoardSlotChanged;
        }

        private void OnBoardSlotChanged(int index, ICard newCard)
        {
            var view = _slotViews[index];
            view.BindModel(newCard);
        }
    }
}
