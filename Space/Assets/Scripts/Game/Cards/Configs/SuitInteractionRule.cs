using System;
using UnityEngine;

namespace SpaceGame
{
    /// <summary>
    /// Набор взаимодействий карты с другими мастями
    /// </summary>
    [Serializable]
    public sealed class SuitInteractionRule
    {
        [Tooltip("С какой мастью взаимодействуем")]
        public ElementSuit WithSuit;

        [Tooltip("Тип взаимодействия")]
        public InteractionType InteractionType;

        [Tooltip("Численное значение (например +3 очка, или множитель 2.0)")]
        public float Value;
    }
}