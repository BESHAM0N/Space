using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SpaceGame
{
    [RequireComponent(typeof(Image))]
    public sealed class BoardSlotView : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private int _slotIndex;

        [Header("Подсветка")] 
        [SerializeField] private Color normal = new (1, 1, 1, 0.15f);
        [SerializeField] private Color hover = new (1, 1, 1, 0.35f);
        [SerializeField] private Image _img;
        [SerializeField] private BoardController _controller;
        
        public BoardController Controller => _controller;
        public int SlotIndex => _slotIndex;

        private void Awake()
        {
            _img.raycastTarget = true;
            _img.color = normal;
        }
      
        public void BindModel(ICard card)
        {
            _img.color = card == null ? normal : hover;
        }

        public void OnPointerEnter(PointerEventData eventData) => _img.color = hover;
        public void OnPointerExit(PointerEventData eventData) => _img.color = normal;

        public void OnDrop(PointerEventData eventData)
        {
            _img.color = normal;

            var go = eventData.pointerDrag;
            if (!go || _controller == null) 
                return;

            var view = go.GetComponent<CardView>();
            var drag = go.GetComponent<CardDragHandler>();
            
            if (view == null) 
                return;

            var ok = _controller.TryPlaceFromUI(_slotIndex, view);
            if (!ok)
                drag.ReturnToOriginalPos();
        }
    }
}