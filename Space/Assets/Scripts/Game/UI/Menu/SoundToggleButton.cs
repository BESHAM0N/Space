using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SpaceGame
{
    [RequireComponent(typeof(Button))]
    public sealed class SoundToggleButton : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Sprite _spriteOn;
        [SerializeField] private Sprite _spriteOff;
        [SerializeField] private Button _button;
        
        [Inject] private ISoundService _sound;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
            RefreshIcon();
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            _sound.ToggleSound();
            RefreshIcon();
        }

        private void RefreshIcon()
        {
            if (_icon == null) return;
            _icon.sprite = _sound.IsSoundEnabled ? _spriteOn : _spriteOff;
        }
    }
}