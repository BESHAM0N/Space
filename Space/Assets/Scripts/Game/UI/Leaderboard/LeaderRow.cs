using TMPro;
using UnityEngine;

namespace SpaceGame
{
    public sealed class LeaderRow : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _leaderNameText;
        [SerializeField] private TextMeshProUGUI _leaderListNumberText;
        [SerializeField] private TextMeshProUGUI _scoreText;

        public void Initialize(string leaderListNumber, string leaderName, string score)
        {
            _leaderNameText.text = leaderName;
            _leaderListNumberText.text = leaderListNumber;
            _scoreText.text = score;
        }
    }
}