namespace SpaceGame
{
    public interface IInteractionMatrix
    {
        InteractionType Get(ElementSuit first, ElementSuit second);
    }
}