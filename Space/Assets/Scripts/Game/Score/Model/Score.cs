using System;

namespace SpaceGame
{
    public sealed class Score : IScore
    {
        public event Action<int> OnStateChanged;
        
        private int _currentScore;
        
        public void AddScore()
        {
            _currentScore++;
            OnStateChanged?.Invoke(_currentScore);
        }

        public int GetCurrentScore()
        {
            return _currentScore;
        }
    }
}