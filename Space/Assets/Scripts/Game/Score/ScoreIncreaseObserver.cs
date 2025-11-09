using System;
using Zenject;

namespace SpaceGame
{
    public sealed class ScoreIncreaseObserver :  IInitializable, IDisposable
    {
        private readonly IScore _score;
        private readonly IScoreEvents _events;
        private readonly ILeaderboard _leaderboard;

        public ScoreIncreaseObserver(IScore score, ILeaderboard leaderboard, IScoreEvents events)
        {
            _score = score;
            _leaderboard = leaderboard;
            _events = events;
        }
        
        public void Initialize()
        {
            _score.Load();
            _events.LevelFinished += OnLevelFinished;
        }

        public void Dispose()
        {
            _events.LevelFinished -= OnLevelFinished;
        }
        
        private void OnLevelFinished(int levelScore)
        {
            _score.Add(levelScore);
          
            var name = _leaderboard.GetSavedPlayerName();
            if (!string.IsNullOrEmpty(name))
                _leaderboard.UpdateUserScoreIfHigher(name, _score.GetCurrentScore());
        }
    } 
}