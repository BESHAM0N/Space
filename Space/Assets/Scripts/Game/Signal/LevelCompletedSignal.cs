namespace SpaceGame
{
    public sealed class LevelCompletedSignal
    {
        public int Score;
        public LevelCompletedSignal(int score) => Score = score;
    }

    public sealed class NextLevelRequestedSignal { }
    public sealed class ReturnToMenuRequestedSignal { }
}