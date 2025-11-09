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

        /// <summary>
        /// Обрабатывает массив слотов in-place: слева направо.
        /// На каждом i: добавляем очки карты i, затем смотрим реакцию только с i+1.
        /// Destroy → slots[i+1] = null; Absorb → slots[i+1] копирует i; Combo → +bonus; None → ничего.
        /// </summary>
        public int Run(ICard[] slots)
        {
            int total = 0;

            for (int i = 0; i < slots.Length; i++)
            {
                var a = slots[i];
                if (a == null) continue;

                // 1) Очки самой карты i
                total += a.BasePoints;

                // 2) Реакция только с соседом справа (если он существует и не null)
                int j = i + 1;
                if (j >= slots.Length) continue;

                var b = slots[j];
                if (b == null) continue; // сосед уничтожен ранее → реакции нет

                var type = _matrix.Get(a.Suit, b.Suit);
                switch (type)
                {
                    case InteractionType.None:
                        // Доп. очков нет
                        break;

                    case InteractionType.Bonus:
                        total += _comboBonus; // только бонус, без изменений карт
                        break;

                    case InteractionType.Destroys:
                        // Уничтожаем вторую, доп. очков нет (мы уже взяли очки за 'a')
                        slots[j] = null;
                        break;

                    case InteractionType.Absorption:
                        // b становится копией a, доп. очков нет (очки за b получим, когда дойдём до индекса j)
                        if (b is Card cb)
                            cb.CopyFrom(a);
                        else if (b is ICard cp)
                            cp.CopyFrom(a);
                        break;
                }
            }

            return total;
        }
    }
}
