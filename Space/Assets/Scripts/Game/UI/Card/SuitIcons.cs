using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame
{
    [CreateAssetMenu(fileName = "SuitIcons", menuName = "SpaceGame/Suit Icons Config")]
    public sealed class SuitIcons : ScriptableObject
    {
        [Serializable]
        public struct SuitIconPair
        {
            public ElementSuit Suit;
            public Sprite Icon;
        }
        [Tooltip("Сопоставление мастей и иконок")]
        public List<SuitIconPair> Icons = new();
        
        private Dictionary<ElementSuit, Sprite> _sprites;
        
        /// <summary>
        /// Быстрый доступ по масти.
        /// </summary>
        public Sprite GetIcon(ElementSuit suit)
        {
            if (_sprites == null)
            {
                _sprites = new Dictionary<ElementSuit, Sprite>();
                foreach (var pair in Icons)
                    if (!_sprites.ContainsKey(pair.Suit))
                        _sprites.Add(pair.Suit, pair.Icon);
            }

            return _sprites.TryGetValue(suit, out var sprite) ? sprite : null;
        }
    }
}