using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace SpaceGame
{
    public class LevelFlowController : ILevelFlow, IInitializable, IDisposable
    {
        private readonly ILevelEndUI _ui;
        private readonly DeckService _deck;
        private readonly IHandService _hand;
        private readonly ISceneLoader _sceneLoader;
        private readonly IScore _score;
        
        public LevelFlowController(ILevelEndUI ui, DeckService deck, IHandService hand, ISceneLoader sceneLoader, IScore score)
        {
            _ui = ui;
            _deck = deck;
            _hand = hand;
            _sceneLoader = sceneLoader;
            _score = score;
        }

        public void Initialize()
        {
            _ui.Hide();
            _score.Load();
            _ui.NextClicked += OnNextClicked;
            _ui.MenuClicked += OnMenuClicked;
        }

        public void Dispose()
        {
            _ui.NextClicked -= OnNextClicked;
            _ui.MenuClicked -= OnMenuClicked;
        }
     
        public void CompleteLevel(int levelScore)
        {
            if (_ui is LevelCompletePopupView popup)
                popup.Show(levelScore, _score.GetCurrentScore());
            else
                _ui.Show(levelScore);
        }

        private void OnNextClicked()
        {
            _ui.Hide();
            _hand.ClearHand();
           
            var next = _deck.DealRandom(10);
            _hand.BuildHand(next);
            Debug.Log($"Выдано {next.Count} карт, в остатке: {_deck.RemainingCount}");
        }

        private void OnMenuClicked()
        {
            _ui.Hide();
            _sceneLoader.LoadMainMenu();
        }
    }
}