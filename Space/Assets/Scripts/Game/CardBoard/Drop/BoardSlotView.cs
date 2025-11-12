using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace SpaceGame
{
    [RequireComponent(typeof(Image))]
    public sealed class BoardSlotView : MonoBehaviour, IDropHandler
    {
        [Inject] private readonly ISoundService _sound;
        [SerializeField] private int _slotIndex;

        [SerializeField] private Image _img;
        [SerializeField] private GameObject _arrow;
        [SerializeField] private BoardController _controller;

        public BoardController Controller => _controller;
        public int SlotIndex => _slotIndex;

        private void Awake()
        {
            _img.raycastTarget = true;
            _arrow.SetActive(true);
        }

        public void BindModel(ICard card)
        {
            if (card == null)
            {
                _arrow.SetActive(true);
            }
            else
            {
                _arrow.SetActive(false);
            }
        }

        public void OnDrop(PointerEventData eventData)
        {
            _sound.Play(SoundType.UpAndDownCard);

            var go = eventData.pointerDrag;
            if (!go || _controller == null)
                return;

            var view = go.GetComponent<CardView>();
            var drag = go.GetComponent<CardDragHandler>();

            if (view == null)
                return;

            var ok = _controller.TryPlaceFromUI(_slotIndex, view);
            if (!ok)
            {
                drag.ReturnToOriginalPos();
            }
        }
    }
}