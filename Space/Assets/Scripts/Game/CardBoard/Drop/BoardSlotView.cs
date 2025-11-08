using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SpaceGame
{
    [RequireComponent(typeof(Image))]
    public sealed class BoardSlotView : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public int SlotIndex;

        [Header("Подсветка")]
        [SerializeField] private Color normal = new Color(1,1,1,0.15f);
        [SerializeField] private Color hover  = new Color(1,1,1,0.35f);
        [SerializeField] private Image _img;
        [SerializeField] private BoardController _controller;

        private void Awake()
        {
            _img.color = normal;
        }

        public void Init(BoardController controller) => _controller = controller;

        // Можешь тут отрисовать «пусто/занято»
        public void BindModel(ICard card)
        {
            // пример: меняем альфу рамки
            _img.color = card == null ? normal : hover;
        }

        public void OnPointerEnter(PointerEventData eventData) => _img.color = hover;
        public void OnPointerExit(PointerEventData eventData)  => _img.color = normal;

        public void OnDrop(PointerEventData eventData)
        {
            _img.color = normal;

            var cardView = eventData.pointerDrag ? eventData.pointerDrag.GetComponent<CardView>() : null;
            var drag     = eventData.pointerDrag ? eventData.pointerDrag.GetComponent<CardDragHandler>() : null;
            if (cardView == null) return;

            //var ok = _controller.TryPlaceFromUI(SlotIndex, cardView);
            //if (!ok) drag?.ReturnToOriginalParent();
        }
    }
}