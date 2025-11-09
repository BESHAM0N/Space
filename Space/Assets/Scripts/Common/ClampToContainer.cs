using UnityEngine;

namespace SpaceGame
{
    public class ClampToContainer : MonoBehaviour
    {
        [SerializeField] private bool clampOnResize = true;

        private RectTransform _rectTransform;
        private RectTransform _container;
        private CardDragHandler _drag;

        private void Awake()
        {
            _rectTransform = (RectTransform)transform;

            var canvas = GetComponentInParent<Canvas>();
            if (canvas != null)
                _container = canvas.transform as RectTransform;

            _drag = GetComponent<CardDragHandler>();
            if (_drag != null)
            {
                _drag.Dragged += ClampNow;
                _drag.Dropped += ClampNow;
            }
        }

        private void OnEnable() => ClampNow();

        private void OnDestroy()
        {
            if (_drag != null)
            {
                _drag.Dragged -= ClampNow;
                _drag.Dropped -= ClampNow;
            }
        }

        private void OnRectTransformDimensionsChange()
        {
            if (isActiveAndEnabled && clampOnResize)
                ClampNow();
        }

        private void ClampNow()
        {
            _rectTransform.ClampRectPositionToContainer(_container);
        }
    }
}

