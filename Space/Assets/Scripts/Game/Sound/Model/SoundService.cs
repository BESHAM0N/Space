using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace SpaceGame
{
    public sealed class SoundService : ISoundService
    {
        private const string PREF_KEY = "sound_enabled";
        public bool IsSoundEnabled { get; private set; } = true;

        private readonly SoundPlayer _player;
        private readonly HashSet<SoundType> _bgmTypes;
        
        private const float EFFECTS_VOLUME = 0.3f;
        private SoundType? _lastMusic;

        [Inject]
        public SoundService(SoundPlayer player)
        {
            _player = player;
            
            _bgmTypes = new HashSet<SoundType>
            {
                SoundType.MainMenuBackgroundMusic,
                SoundType.LevelOneBackgroundMusic,
                SoundType.LevelTwoBackgroundMusic,
                SoundType.LevelThreeBackgroundMusic,
                SoundType.LevelFourBackgroundMusic,
                SoundType.LevelFiveBackgroundMusic,
                SoundType.LevelSixBackgroundMusic,
            };

            IsSoundEnabled = PlayerPrefs.GetInt(PREF_KEY, 1) == 1;
            Apply(IsSoundEnabled);
           
            SceneManager.activeSceneChanged += (_, __) => Apply(IsSoundEnabled);
        }

        public void ToggleSound() => SetEnabled(!IsSoundEnabled);

        public void SetEnabled(bool enabled)
        {
            if (IsSoundEnabled == enabled) return;
            IsSoundEnabled = enabled;
            PlayerPrefs.SetInt(PREF_KEY, enabled ? 1 : 0);
            PlayerPrefs.Save();
            Apply(enabled);

            if (enabled && _lastMusic.HasValue)
            {
                _player.PlayMusic(_lastMusic.Value);
            }
        }

        private void Apply(bool enabled)
        {
            _player.SetMasterMute(!enabled);
        }

        public void Play(SoundType type, float volume = 0.2f, float pitch = 1f)
        {
            // if (!IsSoundEnabled) return;
            // if (_bgmTypes.Contains(type))
            // {
            //     _player.PlayMusic(type);
            // }
            // else
            // {
            //     _player.PlaySfx(type, EFFECTS_VOLUME, pitch);
            // }
            
            if (_bgmTypes.Contains(type))
            {
                _lastMusic = type;
                if (!IsSoundEnabled) return;     // не играем, но запомнили что хотели
                _player.PlayMusic(type);
            }
            else
            {
                if (!IsSoundEnabled) return;     // SFX глушим полностью, без очереди
                _player.PlaySfx(type, EFFECTS_VOLUME, pitch);
            }
        }

        public void PlayLoop(SoundType type)
        {
            _lastMusic = type;
            if (!IsSoundEnabled) return;         // не играем сейчас, но запомнили
            _player.PlayMusic(type);
        }

        public void StopLoop()
        {
            _player.StopMusic();
        }
    }
}
