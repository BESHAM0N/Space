using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceGame
{
    public sealed class GameCompletePopupView : MonoBehaviour, IGameEndUI
    {
        [SerializeField] private GameObject _root;
        [SerializeField] private TMP_Text _totalScoreText;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _menuButton;
        [SerializeField] private ScoreHud _scoreHud;

        public event Action MenuClicked;
        
        private void Awake()
        {
            if (_root) _root.SetActive(false);
            if (_menuButton) _menuButton.onClick.AddListener(() => MenuClicked?.Invoke());
        }

        public void Show(int totalScore)
        {
            if (_totalScoreText) _totalScoreText.text = $"All your points: {totalScore}";
            if (_root) _root.SetActive(true);
            _scoreHud.Reset();
        }

        public void Hide()
        {
            if (_root) _root.SetActive(false);
        }
    }
}