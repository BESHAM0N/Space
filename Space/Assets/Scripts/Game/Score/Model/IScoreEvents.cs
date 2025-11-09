using System;

namespace SpaceGame
{
    public interface IScoreEvents
    {
        event Action<int> LevelFinished;
        void RaiseLevelFinished(int levelScore);
    }
}