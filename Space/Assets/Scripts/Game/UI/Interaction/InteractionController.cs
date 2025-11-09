using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SpaceGame
{
    public sealed class InteractionController : MonoBehaviour
    {
        [SerializeField] private BoardController _boardController;

        [Header("UI")]
        [SerializeField] private Button _playButton;    // Начать шоу
        [SerializeField] private Button _finishButton;  // Закончить шоу

        [Header("Timings (seconds)")]
        [SerializeField] private float _noneDuration       = 0.35f;
        [SerializeField] private float _bonusDuration      = 1.00f;
        [SerializeField] private float _destroyDuration    = 1.00f;
        [SerializeField] private float _absorptionDuration = 1.25f;
        [SerializeField] private float _gapBetweenSteps    = 0.12f;
        [SerializeField] private float _postAnimDelay      = 0.20f;

        private BoardReactionsManager _manager;

        [Inject] private ILevelFlow _levelFlow;
        [Inject] private IScoreEvents _scoreEvents;

        private readonly List<CardAnimEvent> _animBuffer = new();
        private Sequence _seq;

        private void Awake()
        {
            _manager = new BoardReactionsManager(new StaticInteractionMatrix(), comboBonus: 30);
        }

        private void Start()
        {
            if (_playButton)   _playButton.onClick.AddListener(PlayPresentation);
            if (_finishButton) _finishButton.onClick.AddListener(FinishPresentation);

            SetUIStateIdle(); // стартовое состояние
        }

        // ===== UI states =====
        private void SetUIStateIdle()
        {
            // Готовы начать: показать "Начать", скрыть "Закончить"
            if (_playButton)
            {
                _playButton.gameObject.SetActive(true);
                _playButton.interactable = true;
            }
            if (_finishButton)
            {
                _finishButton.gameObject.SetActive(false);
                _finishButton.interactable = false;
            }
        }

        private void SetUIStatePlaying()
        {
            // Во время шоу: "Начать" скрыть/выключить, "Закончить" скрыть
            if (_playButton)
            {
                _playButton.interactable = false;
                _playButton.gameObject.SetActive(false);
            }
            if (_finishButton)
            {
                _finishButton.interactable = false;
                _finishButton.gameObject.SetActive(false);
            }
        }

        private void SetUIStateWaitingFinish()
        {
            // После анимаций: показать "Закончить"
            if (_finishButton)
            {
                _finishButton.gameObject.SetActive(true);
                _finishButton.interactable = true;
            }
        }

        // =============== КНОПКА 1 ===============
        private void PlayPresentation()
        {
            if (_seq != null && _seq.IsActive() && _seq.IsPlaying())
                return;

            SetUIStatePlaying();

            var count = _boardController.Board.SlotsCount;
            var slots = new ICard[count];
            for (int i = 0; i < count; i++)
                slots[i] = _boardController.Board.GetCard(i);

            _manager.RunDetailed(slots, _animBuffer);

            // Если анимировать нечего — сразу показать "Закончить"
            if (_animBuffer.Count == 0)
            {
                SetUIStateWaitingFinish();
                return;
            }

            _seq = DOTween.Sequence();

            for (int k = 0; k < _animBuffer.Count; k++)
            {
                var evt = _animBuffer[k];
                var view = SafeGetView(evt.Index, count);
                if (view == null) continue;

                // Бонус-пара одновременно
                if (evt.Type == CardAnimType.Bonus && k + 1 < _animBuffer.Count)
                {
                    var next = _animBuffer[k + 1];
                    if (next.Type == CardAnimType.Bonus)
                    {
                        var view2 = SafeGetView(next.Index, count);
                        _seq.AppendCallback(() =>
                        {
                            view.OnBonusAnim();
                            if (view2 != null) view2.OnBonusAnim();
                        });
                        _seq.AppendInterval(_bonusDuration + _gapBetweenSteps);
                        k++;
                        continue;
                    }
                }

                _seq.AppendCallback(() =>
                {
                    switch (evt.Type)
                    {
                        case CardAnimType.NoneLift:
                            view.OnNoneAnim();
                            break;
                        case CardAnimType.Bonus:
                            view.OnBonusAnim();
                            break;
                        case CardAnimType.Destroy:
                            view.OnDestroyAnim();
                            break;
                        case CardAnimType.Absorption:
                            view.OnAbsorptionAnim(autoHideSeconds: 1f);
                            break;
                    }
                });

                _seq.AppendInterval(GetStepDuration(evt.Type) + _gapBetweenSteps);
            }

            _seq.AppendInterval(_postAnimDelay);

            _seq.SetAutoKill(true)
                .SetUpdate(true)
                .OnComplete(() =>
                {
                    _seq = null;
                    SetUIStateWaitingFinish(); // показать кнопку "Закончить"
                })
                .Play();
        }

        // =============== КНОПКА 2 ===============
        private void FinishPresentation()
        {
            // Если вдруг ещё что-то играет — остановим
            if (_seq != null && _seq.IsActive())
            {
                _seq.Kill(true);
                _seq = null;
            }

            var count = _boardController.Board.SlotsCount;
            var slots = new ICard[count];
            for (int i = 0; i < count; i++)
                slots[i] = _boardController.Board.GetCard(i);

            var levelScore = _manager.RunDetailed(slots, _animBuffer);

            _scoreEvents.RaiseLevelFinished(levelScore);
            _levelFlow.CompleteLevel(levelScore);

            SetUIStateIdle();
        }
     
        private CardView SafeGetView(int index, int count)
        {
            if (index < 0 || index >= count) return null;
            return _boardController.GetView(index);
        }

        private float GetStepDuration(CardAnimType type) => type switch
        {
            CardAnimType.NoneLift   => _noneDuration,
            CardAnimType.Bonus      => _bonusDuration,
            CardAnimType.Destroy    => _destroyDuration,
            CardAnimType.Absorption => _absorptionDuration,
            _ => 0.8f
        };
    }
}
