using System;
using UnityEngine;
using Zenject;

namespace SpaceGame
{
    public sealed class GameOverObserver : IInitializable, IDisposable
    {
        private readonly IScore _score;
        private readonly ILeaderboard _leaderboard;
        private readonly string _username;
        
        public GameOverObserver(IScore score, ILeaderboard leaderboard)
        {
            _score = score;
            _leaderboard = leaderboard;
            _username = PlayerPrefs.GetString("PlayerName", "Player");
        }
        
        public void Initialize()
        {
            //TODO: Подписка на завершение игры          
        }

        public void Dispose()
        {
            //TODO: Отписка от завершение игры 
        }
        
        private void OnGameOver()
        {
            _leaderboard.UpdateUserScoreIfHigher(_username, _score.GetCurrentScore());
        }
    }
}