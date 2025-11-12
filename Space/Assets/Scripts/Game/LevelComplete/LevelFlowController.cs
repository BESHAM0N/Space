using System;
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
        private readonly LevelController _levelController;
        private readonly IGameEndUI _gameEndUi;
        private readonly ISoundService _soundService;
        
        public LevelFlowController(ILevelEndUI ui, DeckService deck, IHandService hand, ISceneLoader sceneLoader, IScore score, LevelController levelController, IGameEndUI gameEndUi, ISoundService soundService)
        {
            _ui = ui;
            _deck = deck;
            _hand = hand;
            _sceneLoader = sceneLoader;
            _score = score;
            _levelController = levelController;
            _gameEndUi = gameEndUi;
            _soundService = soundService;
        }

        public void Initialize()
        {
            _ui.Hide();
            _score.Load();
            _ui.NextClicked += OnNextClicked;
            _ui.MenuClicked += OnMenuClicked;
            _gameEndUi.MenuClicked += OnMenuClicked;
            
            StartLevel();
        }

        public void Dispose()
        {
            _ui.NextClicked -= OnNextClicked;
            _ui.MenuClicked -= OnMenuClicked;
            _gameEndUi.MenuClicked -= OnMenuClicked;
        }
     
        public void CompleteLevel(int levelScore)
        {
            _soundService.StopLoop();
            _soundService.PlayLoop(SoundType.Bonus);
            
            if (_ui is LevelCompletePopupView popup)
                popup.Show(levelScore, _score.GetCurrentScore());
            else
                _ui.Show(levelScore);
        }
        
        private void StartLevel()
        {
            _levelController?.SetRandomLevelTheme();
            if (!TryBuildNewHand(10))
            {
                EndGame();
                return;
            }
            _ui.Hide();
        }
        
        private void OnNextClicked()
        {
            _ui.Hide();
            BuildNewHand();
        }
        
        private void BuildNewHand()
        {
            _ui.Hide();
            _levelController?.SetRandomLevelTheme();
            if (!TryBuildNewHand(10))
            {
                EndGame();
                return;
            }

            Debug.Log($"Следующий уровень загружен. В колоде осталось: {_deck.RemainingCount}");
        }

        private void OnMenuClicked()
        {
            _sceneLoader.LoadMainMenu();
        }
        
        private bool TryBuildNewHand(int count)
        {
            if (_deck.RemainingCount < count)
                return false;

            _hand.ClearHand();
            var next = _deck.DealRandom(count);
            _hand.BuildHand(next);
            return true;
        }
        
        private void EndGame()
        {
            _ui.Hide();
            _hand.ClearHand();
            
            _soundService.StopLoop();
            _soundService.PlayLoop(SoundType.MainMenuBackgroundMusic);

            var total = _score.GetCurrentScore();
            if (_gameEndUi != null)
            {
                _gameEndUi.Show(total);
            }
            else
            {
                Debug.Log($"GAME COMPLETE. Total score: {total}");
                _sceneLoader.LoadMainMenu();
            }
        }
    }
}