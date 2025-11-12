using UnityEngine.SceneManagement;
using Zenject;

namespace SpaceGame
{
    public class UnitySceneLoader: ISceneLoader
    {
        [Inject] private SoundPlayer _soundService;
        public void LoadMainMenu()
        {
            _soundService.StopMusic();
            SceneManager.LoadScene("MainMenu");
        }
    }
}