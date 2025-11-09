using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame
{
    public sealed class BoardReactionsManager
    {
        private readonly IInteractionMatrix _matrix;
        private readonly int _comboBonus;

        public BoardReactionsManager(IInteractionMatrix matrix, int comboBonus = 30)
        {
            _matrix = matrix;
            _comboBonus = comboBonus;
        }
        
        public int RunDetailed(ICard[] slots, List<CardAnimEvent> animEvents)
        {
            animEvents?.Clear();
            int total = 0;

            for (int i = 0; i < slots.Length; i++)
            {
                var a = slots[i];
                if (a == null) continue;

                total += a.BasePoints;

                int j = i + 1;
                if (j >= slots.Length) continue;

                var b = slots[j];
                if (b == null) continue;

                var type = _matrix.Get(a.Suit, b.Suit);
                switch (type)
                {
                    case InteractionType.None:
                        animEvents?.Add(new CardAnimEvent(i, CardAnimType.NoneLift));
                        animEvents?.Add(new CardAnimEvent(j, CardAnimType.NoneLift));
                        break;

                    case InteractionType.Bonus:
                        total += _comboBonus;
                        animEvents?.Add(new CardAnimEvent(i, CardAnimType.Bonus));
                        animEvents?.Add(new CardAnimEvent(j, CardAnimType.Bonus));
                        break;

                    case InteractionType.Destroys:
                        slots[j] = null;
                        animEvents?.Add(new CardAnimEvent(j, CardAnimType.Destroy));
                        break;

                    case InteractionType.Absorption:
                        b.CopyFrom(a); 
                        animEvents?.Add(new CardAnimEvent(j, CardAnimType.Absorption));
                        break;
                }
            }

            return total;
        }
    }
}
