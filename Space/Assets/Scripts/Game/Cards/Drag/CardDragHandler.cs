using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SpaceGame
{
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(CanvasGroup))]
    public sealed class CardDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [Header("Настройки")]
        [Tooltip("Canvas (RectTransform) поверх всего UI, куда временно переносим карту во время drag.")]
        [SerializeField] private RectTransform _dragCanvas;

        private RectTransform _rt;
        private CanvasGroup _cg;
        private Transform _originalParent;
        private Vector2 _originalAnchoredPos;
        
        private LayoutElement _placeholder;

        private void Awake()
        {
            _rt = GetComponent<RectTransform>();
            _cg = GetComponent<CanvasGroup>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _originalParent = transform.parent;
            _originalAnchoredPos = _rt.anchoredPosition;
          
            var ph = new GameObject("Placeholder", typeof(RectTransform), typeof(LayoutElement));
            var phRt = (RectTransform)ph.transform;
            phRt.SetParent(_originalParent, false);
            phRt.SetSiblingIndex(transform.GetSiblingIndex());
            _placeholder = ph.GetComponent<LayoutElement>();
           
            _cg.blocksRaycasts = false;
           
            transform.SetParent(_dragCanvas, worldPositionStays: false);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    _dragCanvas, eventData.position, eventData.pressEventCamera, out var local))
            {
                _rt.anchoredPosition = local;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _cg.blocksRaycasts = true;
           
            if (transform.parent == _dragCanvas)
                ReturnToOriginalParent();
          
            if (_placeholder != null)
            {
                Destroy(_placeholder.gameObject);
                _placeholder = null;
            }
        }

        public void ReturnToOriginalParent()
        {
            transform.SetParent(_originalParent, false);
            _rt.anchoredPosition = _originalAnchoredPos;
        }
    }
}
