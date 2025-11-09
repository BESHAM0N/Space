using System;

namespace SpaceGame
{
    public interface IScore
    {
        event Action<int> OnStateChanged;
        
        int GetCurrentScore();
        
        void Add(int points);   // +N очков к сумме кампании
        void Reset();           // обнулить (например, при новой кампании)
        void Load();            // загрузить из PlayerPrefs
        void Save();            // сохранить в PlayerPrefs
    }
}