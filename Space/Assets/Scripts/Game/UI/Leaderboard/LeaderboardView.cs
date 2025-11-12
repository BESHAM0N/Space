using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SpaceGame
{
    public sealed class LeaderboardView : MonoBehaviour
    {
        [SerializeField] private GameObject _leaderboardContent;
        [SerializeField] private StartGameButtonView _startGameButtonView;
        [SerializeField] private TMP_InputField _leaderboardNameInputField;
        [SerializeField] private TMP_Text _currentScoreText;
        [SerializeField] private Button _enterNameButton;
        [SerializeField] private LeaderRow _rowPrefab;
        [SerializeField] private GameObject _warningText;

        [Inject] private ILeaderboard _leaderboard;
        [Inject] private ISoundService _sound;
        
        private void Start()
        {
            _leaderboard.GetLeaderboard();
         
            if (_leaderboard.HasUsername())
            {
                _leaderboardNameInputField.gameObject.SetActive(false);
                _enterNameButton.gameObject.SetActive(false);
                _startGameButtonView.gameObject.SetActive(true);
            }
        }

        private void OnEnable()
        {
            _sound.PlayLoop(SoundType.MainMenuBackgroundMusic);
            _leaderboard.OnLoadLeaderboard += SetLeaderboard;
            _enterNameButton.onClick.AddListener(SetUsername);
            _leaderboardNameInputField.onValueChanged.AddListener(FilterInput);
        }

        private void OnDisable()
        {
            _sound.StopLoop();
            _leaderboard.OnLoadLeaderboard -= SetLeaderboard;
            _enterNameButton.onClick.RemoveListener(SetUsername);
            _leaderboardNameInputField.onValueChanged.RemoveListener(FilterInput);
        }
        
        private void FilterInput(string input)
        {
            if (string.IsNullOrEmpty(input))
                return;

            // Разрешены: a-z, A-Z, 0-9, _
            string filtered = new string(input.Where(c => 
                (c >= 'A' && c <= 'Z') ||
                (c >= 'a' && c <= 'z') ||
                (c >= '0' && c <= '9') ||
                c == '_'
            ).ToArray());
            
            if (filtered != input)
                _leaderboardNameInputField.text = filtered;
        }

        private void SetLeaderboard()
        {
            foreach (Transform child in _leaderboardContent.transform)
                Destroy(child.gameObject);
            
            var playerName = _leaderboard.GetSavedPlayerName();
            
            if (!string.IsNullOrEmpty(playerName))
            {
                _leaderboard.GetLeaders().TryGetValue(playerName, out var myPoints);
                _currentScoreText.text = $"My points: {myPoints}";
            }
            else
            {
                _currentScoreText.text = "My points: 0";
            }
            
            var ordered = _leaderboard.GetLeaders()
                .OrderByDescending(kv => kv.Value)
                .Take(30)
                .ToList();
            
            for (int i = 0; i < ordered.Count; i++)
            {
                var row = Instantiate(_rowPrefab, _leaderboardContent.transform);
                row.Initialize((i + 1).ToString(), ordered[i].Key, ordered[i].Value.ToString());
            }
        }

        private void SetUsername()
        {
            var name = _leaderboardNameInputField.text?.Trim();
            if (string.IsNullOrEmpty(name)) return;

            if (_leaderboard.TryCreateUser(name, 0))
            {
                _enterNameButton.gameObject.SetActive(false);
                _startGameButtonView.gameObject.SetActive(true);
                _warningText.SetActive(false);
            }
            else
            {
                _warningText.SetActive(true);
            }
        }
    }
}