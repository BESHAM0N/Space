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
        //[SerializeField] private StartGameButtonView _startGameButtonView;
        [SerializeField] private TMP_InputField _leaderboardNameInputField;
        [SerializeField] private TMP_Text _currentScoreText;
        [SerializeField] private Button _enterNameButton;
        [SerializeField] private LeaderRow _rowPrefab;
        [SerializeField] private GameObject _warningText;

        [Inject] private ILeaderboard _leaderboard;

        private void Start()
        {
            _leaderboard.GetLeaderboard();
         
            if (_leaderboard.HasUsername())
            {
                _leaderboardNameInputField.gameObject.SetActive(false);
                _enterNameButton.gameObject.SetActive(false);
                //_startGameButtonView.gameObject.SetActive(true);
                //_startGameButtonView.SetStartText(_leaderboard.GetSavedPlayerName());
            }
        }

        private void OnEnable()
        {
            _leaderboard.OnLoadLeaderboard += SetLeaderboard;
            _enterNameButton.onClick.AddListener(SetUsername);
        }

        private void OnDisable()
        {
            _leaderboard.OnLoadLeaderboard -= SetLeaderboard;
            _enterNameButton.onClick.RemoveListener(SetUsername);
        }

        private void SetLeaderboard()
        {
            foreach (Transform child in _leaderboardContent.transform)
                Destroy(child.gameObject);
            
            var ordered = _leaderboard.GetLeaders()
                .OrderByDescending(kv => kv.Value)
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
                //_startGameButtonView.gameObject.SetActive(true);
                //_warningText.SetActive(false);
            }
            else
            {
                //_warningText.SetActive(true);
            }
        }
        
        
    }
}