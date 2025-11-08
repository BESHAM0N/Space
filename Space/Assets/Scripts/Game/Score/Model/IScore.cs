using System;

namespace SpaceGame
{
    public interface IScore
    {
        event Action<int> OnStateChanged;
       
        void AddScore();
        
        int GetCurrentScore();
    }
}