using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SpaceGame
{
    [RequireComponent(typeof(RectTransform))]
    public sealed class CardDragHandler : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler,
        IBeginDragHandler
    {
        private RectTransform _dragLayer;
        private Transform _originalParent;

        private Vector2 _touchOffset;
        private Vector2 _originalWorldPos;
        private CanvasGroup _canvasGroup;

        public event Action Dragged;
        public event Action Dropped;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            var dragCanvasObj = GameObject.Find("DragCanvas");
            _dragLayer = dragCanvasObj.GetComponent<RectTransform>();
            _originalParent = transform.parent;
            _originalWorldPos = transform.position;
        }

        public void OnBeginDrag(PointerEventData eventData)
        { 
            if (transform.parent.TryGetComponent(out BoardSlotView slot))
                slot.Controller.TryRemoveFromUI(slot.SlotIndex);
            
            if (_dragLayer != null)
                transform.SetParent(_dragLayer, true);
        }

        public void OnDrag(PointerEventData eventData)
        {
            RefreshPosition(eventData);
            Dragged?.Invoke();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_canvasGroup) _canvasGroup.blocksRaycasts = false;

            _touchOffset = (Vector2)transform.position - eventData.position;
            RefreshPosition(eventData);
            Dragged?.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            RefreshPosition(eventData);

            if (transform.parent == _dragLayer)
                transform.SetParent(_originalParent, true);

            Dropped?.Invoke();
            if (_canvasGroup)
                _canvasGroup.blocksRaycasts = true;
        }

        private void RefreshPosition(PointerEventData eventData)
        {
            transform.position = eventData.position + _touchOffset;
            var container = _dragLayer ? _dragLayer : (RectTransform)transform.parent;
            ((RectTransform)transform).ClampRectPositionToContainer(container);
        }

        public void ReturnToOriginalPos()
        {
            if (_originalParent == null)
                return;

            transform.SetParent(_originalParent, true);
            transform.position = _originalWorldPos;

            if (_canvasGroup)
                _canvasGroup.blocksRaycasts = true;
        }
    }
}