using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SpaceGame
{
    public sealed class InteractionController : MonoBehaviour
    {
        [SerializeField] private BoardController _boardController;
        [SerializeField] private Button _enterButton;

        private BoardReactionsManager _manager;
        
        [Inject] private ILevelFlow _levelFlow;
        [Inject] private IScoreEvents _scoreEvents;

        private void Awake()
        {
            _manager = new BoardReactionsManager(new StaticInteractionMatrix(), comboBonus: 30);
        }

        private void Start()
        {
            _enterButton.onClick.AddListener(ProcessBoard);
        }

        private void ProcessBoard()
        {
            var slots = new ICard[_boardController.Board.SlotsCount];
            for (int i = 0; i < slots.Length; i++)
            {
                //var index = i;
                slots[i] = _boardController.Board.GetCard(i);
                //Debug.Log($"в ячейке {index}: {slots[i].DisplayName}");
            }

            var levelScore  = _manager.Run(slots);
            Debug.Log($"Итоговые очки: {levelScore }");
            
            _scoreEvents.RaiseLevelFinished(levelScore );
            _levelFlow. CompleteLevel(levelScore );
        }
    }
}