using System;
using UnityEngine;

namespace SpaceGame
{
    public sealed class Score : IScore
    {
        private const string PREF_TOTAL_SCORE = "TOTAL_SCORE";
        
        public event Action<int> OnStateChanged;
        
        private int _currentScore;
        public int GetCurrentScore()
        {
            return _currentScore;
        }
        
        public void Add(int points)
        {
            if (points <= 0) 
            { 
                OnStateChanged?.Invoke(_currentScore); 
                return;
            }
            
            _currentScore += points;
            
            PlayerPrefs.SetInt(PREF_TOTAL_SCORE, _currentScore);
            PlayerPrefs.Save();
            
            OnStateChanged?.Invoke(_currentScore);
        }

        public void Reset()
        {
            _currentScore = 0;
            PlayerPrefs.SetInt(PREF_TOTAL_SCORE, 0);
            PlayerPrefs.Save();
            OnStateChanged?.Invoke(_currentScore);
        }

        public void Load()
        {
            _currentScore = PlayerPrefs.GetInt(PREF_TOTAL_SCORE, 0);
            OnStateChanged?.Invoke(_currentScore);
        }

        public void Save()
        {
            PlayerPrefs.SetInt(PREF_TOTAL_SCORE, _currentScore);
            PlayerPrefs.Save();
        }
    }
}