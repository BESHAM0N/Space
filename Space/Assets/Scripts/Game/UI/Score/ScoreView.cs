using TMPro;
using UnityEngine;

namespace SpaceGame
{
    public sealed class ScoreView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;

        public void SetScore(string score)
        {
            _scoreText.text = score;
        }
    }
}