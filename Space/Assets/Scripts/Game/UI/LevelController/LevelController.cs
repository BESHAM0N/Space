using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Random = UnityEngine.Random;

namespace SpaceGame
{
    public class LevelController : MonoBehaviour
    {
        [Inject] private readonly ISoundService _sound;
        [Inject] private readonly ISceneLoader _sceneLoader;
        
        [SerializeField] private TMP_Text _levelNamedText;
        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private SuitIcons _suitIcons;
        [SerializeField] private Image _levelIconImage;
        
        public ElementSuit CurrentSuit { get; private set; }
        
        private static readonly Dictionary<ElementSuit, Color> SuitColors = new()
        {
            { ElementSuit.Water,       new Color(0.30f, 0.65f, 0.85f) },  // голубой
            { ElementSuit.Fire,        new Color(0.85f, 0.35f, 0.20f) },  // тёплый красно-оранжевый
            { ElementSuit.Air,         new Color(0.80f, 0.80f, 0.95f) },  // светло-воздушный
            { ElementSuit.Electricity, new Color(0.95f, 0.90f, 0.30f) },  // жёлтый
            { ElementSuit.Prop,        new Color(0.75f, 0.60f, 0.45f) },  // бежево-коричневый
            { ElementSuit.Animals,     new Color(0.50f, 0.75f, 0.40f) },  // зелёный
        };
        
        [Serializable]
        private struct LevelTheme
        {
            public string Name;
            public SoundType Music;
            public ElementSuit Suit;
        }
        
        [SerializeField] 
        private List<LevelTheme> _allThemes = new()
        {
            new LevelTheme { Name = "Aqua Mirage. +10 for water",       Music = SoundType.LevelOneBackgroundMusic,  Suit = ElementSuit.Water },
            new LevelTheme { Name = "Blaze Spectacular. +10 for fire",  Music = SoundType.LevelTwoBackgroundMusic,  Suit = ElementSuit.Fire },
            new LevelTheme { Name = "Celestial Aerial. +10 for air",    Music = SoundType.LevelThreeBackgroundMusic,Suit = ElementSuit.Air },
            new LevelTheme { Name = "Voltage Velocity. +10 for electro",Music = SoundType.LevelFourBackgroundMusic, Suit = ElementSuit.Electricity },
            new LevelTheme { Name = "Carnival of Wonders. +10 for props",Music= SoundType.LevelFiveBackgroundMusic, Suit = ElementSuit.Prop },
            new LevelTheme { Name = "Wild Kingdom Revue. +10 for animals",Music= SoundType.LevelSixBackgroundMusic,  Suit = ElementSuit.Animals },
        };
        
        private static List<LevelTheme> _remainingThemes;

        private void Awake()
        {
            if (_mainMenuButton)
                _mainMenuButton.onClick.AddListener(() =>
                {
                    _sound.Play(SoundType.ButtonClick);
                    _sceneLoader.LoadMainMenu();
                });

            if (_remainingThemes == null || _remainingThemes.Count == 0)
                _remainingThemes = new List<LevelTheme>(_allThemes);
        }

        /// <summary>
        /// Назначает случайную тему уровня и удаляет её из списка, чтобы не повторялась.
        /// </summary>
        public void SetRandomLevelTheme()
        {
            if (_remainingThemes.Count == 0)
                _remainingThemes = new List<LevelTheme>(_allThemes);

            int randomIndex = Random.Range(0, _remainingThemes.Count);
            var theme = _remainingThemes[randomIndex];
            _remainingThemes.RemoveAt(randomIndex);

            // 1) Название уровня
            _levelNamedText.text = theme.Name;

            // 2) Цвет текста
            if (SuitColors.TryGetValue(theme.Suit, out var color))
                _levelNamedText.color = color;

            // 3) Музыка
            _sound.StopLoop();
            _sound.PlayLoop(theme.Music);

            // 4) Иконка
            if (_levelIconImage && _suitIcons)
            {
                var sprite = _suitIcons.GetIcon(theme.Suit);
                _levelIconImage.sprite = sprite;
                _levelIconImage.enabled = sprite != null;
            }

            // 5) Запоминаем текущую масть
            CurrentSuit = theme.Suit;
        }
        
        public ElementSuit GetCurrentElement()
        {
            return CurrentSuit;
        }
    }
}