using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame
{
    /// <summary>
    /// Готовая колода карт для уровня 
    /// </summary>
    [CreateAssetMenu(fileName = "ListCard", menuName = "SpaceGame/List Card")]
    public class ListCardPrototypes : ScriptableObject
    {
        [Tooltip("Тематика шоу")]
        public string DisplayName;
        
        [Tooltip("Список карт уровня")]
        public List<CardPrototype> Cards = new();
    }
}