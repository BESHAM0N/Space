using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame
{
    public sealed class LevelManager : MonoBehaviour
    {
        [SerializeField] private ListCardPrototypes _listCardPrototypes;
        [SerializeField] private CardFactory _cardFactory;
        [SerializeField] private Transform _handParent; 
        
        private List<Card> _cards;
        private List<CardView> _views;
        
        private void Start()
        {
            // 1) сборка моделей
            _cards = _cardFactory.BuildModels(_listCardPrototypes);

            // 2) сборка вьюшек
            _views = _cardFactory.CreateViews(_cards, _handParent);
        }
    }
}