namespace SpaceGame
{
    public class DefaultScorePolicy : IScorePolicy
    {
        private readonly int _comboBonus;

        public DefaultScorePolicy(int comboBonus = 30)
        {
            _comboBonus = comboBonus;
        }

        // Нет реакции — забираем очки обеих
        public int PointsForNone(ICard a, ICard b) => (a?.BasePoints ?? 0) + (b?.BasePoints ?? 0);

        // Поглощение — очки только с первой
        public int PointsForAbsorb(ICard a, ICard b) => a?.BasePoints ?? 0;

        // Уничтожение — очки только с первой
        public int PointsForDestroy(ICard a, ICard b) => a?.BasePoints ?? 0;

        // Комбо — обе + бонус
        public int PointsForCombo(ICard a, ICard b) => (a?.BasePoints ?? 0) + (b?.BasePoints ?? 0) + _comboBonus;
    }
}