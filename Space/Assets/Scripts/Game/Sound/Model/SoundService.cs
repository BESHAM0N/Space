using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceGame
{
    public sealed class SoundService : ISoundService
    {
        private const string PREF_KEY = "sound_enabled";
        public bool IsSoundEnabled { get; private set; } = true;

        public SoundService()
        {
            IsSoundEnabled = PlayerPrefs.GetInt(PREF_KEY, 1) == 1;
            Apply(IsSoundEnabled);
            SceneManager.activeSceneChanged += (_, __) => Apply(IsSoundEnabled);
        }

        public void ToggleSound() => SetEnabled(!IsSoundEnabled);

        public void SetEnabled(bool enabled)
        {
            IsSoundEnabled = enabled;
            PlayerPrefs.SetInt(PREF_KEY, enabled ? 1 : 0);
            PlayerPrefs.Save();
            Apply(enabled);
        }

        private static void Apply(bool enabled)
        {
            AudioListener.volume = enabled ? 0.5f : 0f;
        }
    }
}