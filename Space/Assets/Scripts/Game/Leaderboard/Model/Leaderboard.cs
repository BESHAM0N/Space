using System;
using System.Collections.Generic;
using System.Linq;
using Dan.Main;
using Dan.Models;
using UnityEngine;

namespace SpaceGame
{
    public sealed class Leaderboard : ILeaderboard
    {
        public event Action OnLoadLeaderboard;
        
        private readonly string _leaderboardKey = "55ae7efe633a340fda8a4b16bfac5871cb806e5615c1a81dc5ca28c4e5c51c75";
        private readonly Dictionary<string, int> _leaders = new(StringComparer.OrdinalIgnoreCase);
        private const string PLAYER_NAME_KEY = "PlayerName";

        private readonly List<string> _badWords = new()
        {
            "fuck", "shit", "bitch", "asshole", "dick", "cunt", "fag", "slut", "whore", "jerk", "retard", "moron", 
            "f*ck", "f@ck", "sh1t", "b!tch", "p1d0r", "xuy", "blya", "blyat", "suka", "pidr", "ebat"
        };

        public void GetLeaderboard()
        {
            LeaderboardCreator.GetLeaderboard(_leaderboardKey, GetLeaderboard_callback);
        }

        private void GetLeaderboard_callback(Entry[] response)
        {
            _leaders.Clear();

            if (response != null)
            {
                foreach (var entry in response.OrderByDescending(e => e.Score))
                {
                    _leaders[entry.Username] = entry.Score;
                }
            }

            OnLoadLeaderboard?.Invoke();
        }
        
        public bool TryCreateUser(string username, int initialScore = 0)
        {
            if (!IsValidName(username)) return false;
            if (_leaders.ContainsKey(username)) return false;
           
            PlayerPrefs.SetString(PLAYER_NAME_KEY, username);
            PlayerPrefs.Save();

            Upload(username, initialScore);
            return true;
        }
        
        public bool HasUsername() => PlayerPrefs.HasKey(PLAYER_NAME_KEY);

        public Dictionary<string, int> GetLeaders() => _leaders;

        public void UpdateUserScoreIfHigher(string username, int newScore)
        {
            if (!IsValidName(username)) return;

            if (_leaders.TryGetValue(username, out var current))
            {
                if (newScore > current)
                    Upload(username, newScore);
            }
            else
            {
                Upload(username, newScore);
            }
        }
        
        public void ForceUpdateUserScore(string username, int newScore)
        {
            if (!IsValidName(username)) return;
            Upload(username, newScore);
        }

        public string GetSavedPlayerName()
        {
            return PlayerPrefs.GetString(PLAYER_NAME_KEY, string.Empty);
        }

        private void Upload(string username, int score)
        {
            LeaderboardCreator.UploadNewEntry(
                _leaderboardKey,
                username,
                score,
                _ =>
                {
                    GetLeaderboard();
                },
                _ =>
                {
                    Debug.LogError($"UploadNewEntry");
                });
        }

        private bool IsValidName(string username)
        {
            if (string.IsNullOrWhiteSpace(username)) 
                return false;

            var lower = username.ToLowerInvariant();
            return !_badWords.Any(b => lower.Contains(b));
        }
    }
}