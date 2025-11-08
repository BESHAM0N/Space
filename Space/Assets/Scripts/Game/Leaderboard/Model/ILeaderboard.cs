using System;
using System.Collections.Generic;

namespace SpaceGame
{
    public interface ILeaderboard
    {
        event Action OnLoadLeaderboard;
        
         void GetLeaderboard();
         Dictionary<string, int> GetLeaders();
         
         /// <summary>
         /// Создаст нового юзера
         /// </summary>
         /// <param name="username"></param>
         /// <param name="initialScore"></param>
         /// <returns></returns>
         bool TryCreateUser(string username, int initialScore = 0);
         
         /// <summary>
         /// Обновит счет существующего юзера
         /// </summary>
         /// <param name="username"></param>
         /// <param name="newScore"></param>
         void UpdateUserScoreIfHigher(string username, int newScore);
         
         /// <summary>
         /// Принудительно выставит счет юзера
         /// </summary>
         /// <param name="username"></param>
         /// <param name="newScore"></param>
         void ForceUpdateUserScore(string username, int newScore);
         
         string GetSavedPlayerName();
         bool HasUsername();
    }
}