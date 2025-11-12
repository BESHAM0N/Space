using DG.Tweening;
using UnityEngine;

namespace SpaceGame
{
    public class BackgroundRotation : MonoBehaviour
    {
        [Header("Rotation Settings")]
        [SerializeField] private float rotationAngle = 360f;   // на сколько градусов поворачивать
        [SerializeField] private float duration = 60f;         // время одного полного оборота
        [SerializeField] private Ease easeType = Ease.Linear;  // тип плавности вращения
        [SerializeField] private bool clockwise = true;        // направление вращения
        [SerializeField] private bool pingPong = false;        // туда-сюда или по кругу

        private Tween _rotationTween;

        private void Start()
        {
            float angle = clockwise ? rotationAngle : -rotationAngle;

            if (pingPong)
            {
                // вращение туда-обратно
                _rotationTween = transform
                    .DORotate(new Vector3(0f, 0f, angle), duration)
                    .SetEase(easeType)
                    .SetLoops(-1, LoopType.Yoyo);
            }
            else
            {
                // постоянное вращение по кругу
                _rotationTween = transform
                    .DORotate(new Vector3(0f, 0f, angle), duration, RotateMode.FastBeyond360)
                    .SetEase(easeType)
                    .SetLoops(-1, LoopType.Restart);
            }
        }

        private void OnDestroy()
        {
            _rotationTween?.Kill();
        }
    }
}