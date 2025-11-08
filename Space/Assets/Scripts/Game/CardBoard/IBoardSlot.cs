namespace SpaceGame
{
    public interface IBoardSlot
    {
        int Index { get; }
        ICard Card { get; }
        bool TryPlace(ICard card); 
        bool TryClear(out ICard removed);
    }
}