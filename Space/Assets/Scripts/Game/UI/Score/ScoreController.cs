using System;
using Zenject;

namespace SpaceGame
{
    public sealed class ScoreController : IInitializable, IDisposable
    {
        private IScore _score;
        private ScoreView _view;

        public ScoreController(IScore score, ScoreView view)
        {
            _score = score;
            _view = view;
        }
        
        public void Initialize()
        {
            _score.OnStateChanged += UpdateScore;
            StartSetScore();
        }

        public void Dispose()
        {
            _score.OnStateChanged -= UpdateScore;
        }
        
        private void UpdateScore(int score)
        {
            _view.SetScore(score.ToString());
        }

        private void StartSetScore()
        {
            _view.SetScore(_score.GetCurrentScore().ToString());
        }
    }
}