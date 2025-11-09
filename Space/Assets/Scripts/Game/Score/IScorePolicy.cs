namespace SpaceGame
{
    public interface IScorePolicy
    {
        int PointsForNone(ICard a, ICard b);
        int PointsForAbsorb(ICard a, ICard b);
        int PointsForDestroy(ICard a, ICard b);
        int PointsForCombo(ICard a, ICard b);
    }
}