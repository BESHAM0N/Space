using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame
{
    /// <summary>
    /// Прототип 1 карты
    /// </summary>
    [CreateAssetMenu(fileName = "CardPrototype", menuName = "Prototypes/Card Prototype")]
    public sealed class CardPrototype : ScriptableObject
    {
        [Header("Основные")]
        public string Id;
        public string DisplayName;
        [TextArea] public string Description;
        public Sprite image;

        [Header("Игровые свойства")]
        public ElementSuit suit;
        public int basePoints = 1;

        [Header("Взаимодействия")]
        public List<SuitInteractionRule> interactions = new();
    }
}