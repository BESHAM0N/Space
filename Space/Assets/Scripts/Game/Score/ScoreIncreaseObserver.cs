using System;
using Zenject;

namespace SpaceGame
{
    public sealed class ScoreIncreaseObserver :  IInitializable, IDisposable
    {
        private readonly IScore _score;

        public ScoreIncreaseObserver(IScore score)
        {
            _score = score;
        }
        
        public void Initialize()
        {
            //TODO: Подписка на начисление баллов 
        }

        public void Dispose()
        {
            //TODO: Отписка от начисление баллов 
        }
        
        private void OnAddScore()
        {
            _score.AddScore();
        }
    } 
}