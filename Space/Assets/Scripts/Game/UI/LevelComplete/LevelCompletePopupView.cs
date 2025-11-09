using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceGame
{
    public class LevelCompletePopupView : MonoBehaviour, ILevelEndUI
    {
        [SerializeField] private GameObject _root;
        [SerializeField] private TMP_Text _levelScoreText;
        [SerializeField] private TMP_Text _totalScoreText;
        [SerializeField] private Button _nextButton;
        [SerializeField] private Button _menuButton;

        public event Action NextClicked;
        public event Action MenuClicked;

        private void Awake()
        {
            if (_root) 
                _root.SetActive(false);
            
            if (_nextButton) 
                _nextButton.onClick.AddListener(() => NextClicked?.Invoke());
            
            if (_menuButton) 
                _menuButton.onClick.AddListener(() => MenuClicked?.Invoke());
        }

        public void Show(int score)
        {
            if (_levelScoreText) 
                _levelScoreText.text = $"Очки: {score}";
            
            if (_root) 
                _root.SetActive(true);
        }
        
        public void Show(int levelScore, int? totalScore)
        {
            if (_levelScoreText) 
                _levelScoreText.text = $"За уровень: {levelScore}";
            
            if (_totalScoreText && totalScore.HasValue) 
                _totalScoreText.text = $"Всего: {totalScore.Value}";
            
            if (_root) 
                _root.SetActive(true);
        }

        public void Hide()
        {
            if (_root) 
                _root.SetActive(false);
        }
    }
}