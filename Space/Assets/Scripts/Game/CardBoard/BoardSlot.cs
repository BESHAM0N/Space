namespace SpaceGame
{
    public sealed class BoardSlot : IBoardSlot
    {
        private readonly IBoard _board;
        public int Index { get; }
        
        public BoardSlot(IBoard board, int index)
        {
            _board = board;
            Index = index;
        }

        public ICard Card => _board.GetCard(Index);

        public bool TryPlace(ICard card) => _board.TryPlace(Index, card);

        public bool TryClear(out ICard removed) => _board.TryRemove(Index, out removed);
    }
}