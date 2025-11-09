using UnityEngine;

namespace SpaceGame
{
    public static class UnityExtensions
    {
        /// <summary>
        /// Ограничивает положение RectTransform границами заданного контейнера
        /// </summary>
        /// <param name="container">RectTransform контейнера. При null - родительский RectTransform</param>
        public static void ClampRectPositionToContainer(this RectTransform rectTransform, RectTransform container = null)
        {
            var corners = new Vector3[4];

            if (container == null)
                (rectTransform.parent.transform as RectTransform).GetWorldCorners(corners);
            else
                container.GetWorldCorners(corners);

            var containerBottomLeft = corners[0];
            var containerTopRight = corners[2];

            rectTransform.GetWorldCorners(corners);

            var movableBottomLeft = corners[0]; 
            var movableTopRight = corners[2];

            var position = rectTransform.position;
            var deltaBottomLeft = position - movableBottomLeft;
            var deltaTopRight = movableTopRight - position;

            position.x = Mathf.Clamp(position.x, containerBottomLeft.x + deltaBottomLeft.x, containerTopRight.x - deltaTopRight.x);
            position.y = Mathf.Clamp(position.y, containerBottomLeft.y + deltaBottomLeft.y, containerTopRight.y - deltaTopRight.y);

            rectTransform.position = position;
        }
    }
}