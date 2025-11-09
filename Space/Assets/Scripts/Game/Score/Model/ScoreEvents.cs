using System;

namespace SpaceGame
{
    public class ScoreEvents : IScoreEvents
    {
        public event Action<int> LevelFinished;
        public void RaiseLevelFinished(int levelScore) => LevelFinished?.Invoke(levelScore);
    }
}