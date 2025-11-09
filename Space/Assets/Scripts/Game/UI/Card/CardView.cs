using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SpaceGame
{
    public sealed class CardView : MonoBehaviour
    {
        public Action<int> OnCardPoints;
        
        [Inject] private ISoundService _soundService;
        public ICard Card => _card;

        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private TMP_Text _pointsText;
        [SerializeField] private Image _suitIcon;
        [SerializeField] private SuitIcons _suitIcons;
        
        [SerializeField] private GameObject _destroyAnim;
        [SerializeField] private GameObject _bonusAnim;
        [SerializeField] private GameObject _absorptionAnim;

        private ICard _card;

        public void Initialize(ICard card)
        {
            _image.sprite = card.Image;
            _titleText.text = card.DisplayName;
            _pointsText.text = card.BasePoints.ToString();
            _suitIcon.sprite = _suitIcons.GetIcon(card.Suit);
            _card = card;
        }
        
        private void CopyCard()
        {
            _image.sprite = _card.Image;
            _titleText.text = _card.DisplayName;
            _pointsText.text = _card.BasePoints.ToString();
            _suitIcon.sprite = _suitIcons.GetIcon(_card.Suit);
            
            OnCardPoints?.Invoke(_card.BasePoints);
        }
    
        public void OnDestroyAnim()
        {
            if (!_destroyAnim) return;
            
            _destroyAnim.SetActive(true);
            _soundService.Play(SoundType.Destroys);
        }
        
        public void OnBonusAnim()
        {
            if (!_bonusAnim) return;
            _bonusAnim.SetActive(true);
            _soundService.Play(SoundType.Bonus);
            OnCardPoints?.Invoke(30);
        }
        
        public void OnNoneAnim()
        {
            transform.DOKill();
            transform.DOScale(Vector3.one * 1.1f, 0.3f * 0.5f)
                .SetLoops(2, LoopType.Yoyo)
                .SetEase(Ease.OutQuad);
            _soundService.Play(SoundType.None);
            OnCardPoints?.Invoke(_card.BasePoints);
        }
        
        public void OnAbsorptionAnim(float autoHideSeconds = 3f)
        {
            if (!_absorptionAnim) return;
            _absorptionAnim.SetActive(true);
            _soundService.Play(SoundType.Absorption);
            DOVirtual.DelayedCall(autoHideSeconds, () =>
            {
                if (_absorptionAnim) _absorptionAnim.SetActive(false);
                CopyCard();
            });
        }
        
        public class Factory : PlaceholderFactory<CardView> { }
    }
}