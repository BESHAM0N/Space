using UnityEngine;

namespace SpaceGame
{
    public class ClampCard : MonoBehaviour
    {
        [SerializeField] private CardDragHandler _dragComponent;

        private void Awake()
        {
            if (_dragComponent != null)
            {
                _dragComponent.Dragged += ClampNow;
                _dragComponent.Dropped += ClampNow;
            }
        }

        private void OnEnable() => ClampNow();

        private void OnDestroy()
        {
            if (_dragComponent != null)
            {
                _dragComponent.Dragged -= ClampNow;
                _dragComponent.Dropped -= ClampNow;
            }
        }

        private void ClampNow()
        {
            (transform as RectTransform).ClampRectPositionToContainer();
        }
    }
}