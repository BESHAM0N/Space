using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceGame
{
    public sealed class CardView : MonoBehaviour
    {
        public ICard Card => _card;
        
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private TMP_Text _pointsText;
        [SerializeField] private Image _suitIcon;
        [SerializeField] private SuitIcons _suitIcons;

        private ICard _card;
        
        public void Initialize(ICard card)
        {
            _image.sprite = card.Image;
            _titleText.text = card.DisplayName;
            _pointsText.text = card.BasePoints.ToString();
            _suitIcon.sprite = _suitIcons.GetIcon(card.Suit);
            _card = card;
        }
    }
}