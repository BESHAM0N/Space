namespace SpaceGame
{
    public interface ISoundService
    {
        bool IsSoundEnabled { get; }
        void ToggleSound();
        void SetEnabled(bool enabled);
    }
}