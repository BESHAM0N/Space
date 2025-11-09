using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace SpaceGame
{
    public sealed class HandService : IHandService
    {
        private readonly ICardFactory _factory;
        private readonly List<CardView> _views = new();
        [Inject(Id = "HandParent")] private Transform _handRoot;

        public HandService(ICardFactory factory) => _factory = factory;

        public void BuildHand(IReadOnlyList<Card> cards)
        {
            ClearHand();
            if (cards == null) return;
            for (int i = 0; i < cards.Count; i++)
                _views.Add(_factory.CreateView(cards[i]));
        }
        
        public void ClearHand()
        {
            // 1) Жёстко сносим всё, что лежит под корнем руки (на 100% надёжно)
            if (_handRoot != null)
            {
                // только прямых детей-CardView (чтобы случайно не снести те, что утащили на борд)
                for (int i = _handRoot.childCount - 1; i >= 0; i--)
                {
                    var child = _handRoot.GetChild(i);
                    if (child.TryGetComponent<CardView>(out var cv))
                        Object.Destroy(child.gameObject);
                }
            }

            // 2) Чистим то, что трекали списком (на случай, если что-то осталось)
            for (int i = 0; i < _views.Count; i++)
                if (_views[i]) Object.Destroy(_views[i].gameObject);

            _views.Clear();
        }
    }
}