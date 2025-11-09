using System.Collections.Generic;

namespace SpaceGame
{
    public interface ICardFactory
    {
        List<Card> BuildModels(ListCardPrototypes source);
        CardView CreateView(Card model); 
        List<CardView> CreateViews(IReadOnlyList<Card> models);
    }
}