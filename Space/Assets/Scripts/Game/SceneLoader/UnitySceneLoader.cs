using UnityEngine.SceneManagement;

namespace SpaceGame
{
    public class UnitySceneLoader: ISceneLoader
    {
        public void LoadMainMenu() => SceneManager.LoadScene("MainMenu");
    }
}