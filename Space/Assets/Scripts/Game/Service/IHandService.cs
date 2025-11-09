using System.Collections.Generic;

namespace SpaceGame
{
    public interface IHandService
    {
        void ClearHand();
        void BuildHand(IReadOnlyList<Card> cards);
    }
}