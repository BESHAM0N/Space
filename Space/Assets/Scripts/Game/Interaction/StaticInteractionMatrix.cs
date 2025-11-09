using UnityEngine;

namespace SpaceGame
{
    public sealed class StaticInteractionMatrix : IInteractionMatrix
    {
        private readonly InteractionType[,] _matrix;

        public StaticInteractionMatrix()
        {
            _matrix = new InteractionType[6, 6]
            {
                { (InteractionType)3, (InteractionType)0, (InteractionType)1, (InteractionType)2, (InteractionType)3, (InteractionType)2 },
                { (InteractionType)2, (InteractionType)3, (InteractionType)0, (InteractionType)2, (InteractionType)3, (InteractionType)1 },
                { (InteractionType)3, (InteractionType)1, (InteractionType)3, (InteractionType)2, (InteractionType)0, (InteractionType)2 },
                { (InteractionType)3, (InteractionType)0, (InteractionType)2, (InteractionType)3, (InteractionType)2, (InteractionType)1 },
                { (InteractionType)2, (InteractionType)3, (InteractionType)0, (InteractionType)2, (InteractionType)3, (InteractionType)1 },
                { (InteractionType)3, (InteractionType)1, (InteractionType)0, (InteractionType)2, (InteractionType)2, (InteractionType)3 }
            };
        }
        
        public InteractionType Get(ElementSuit first, ElementSuit second)
        {
            Debug.Log($"first: {first}, second: {second}. Matrix: {_matrix[(int)first, (int)second]}");
            return _matrix[(int)first, (int)second];
        }
    }
}