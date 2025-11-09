using System;
using System.Collections.Generic;

namespace SpaceGame
{
    public sealed class DeckService
    {
        private readonly ListCardPrototypes _source;
        private List<CardPrototype> _remaining;
        private readonly Random _random = new Random();

        public DeckService(ListCardPrototypes source)
        {
            _source = source;
            Reset();
        }
        
        public int RemainingCount => _remaining.Count;

        public void Reset()
        {
            _remaining = new List<CardPrototype>(_source?.Cards ?? new List<CardPrototype>());
        }

        public List<Card> Deal(int count)
        {
            var result = new List<Card>(count);
            if (_remaining.Count == 0 || count <= 0)
                return result;

            for (int i = 0; i < count && _remaining.Count > 0; i++)
            {
                var proto = _remaining[0];
                _remaining.RemoveAt(0); // удаляем из общего списка
                result.Add(CreateFromPrototype(proto));
            }

            return result;
        }

        public List<Card> DealRandom(int count)
        {
            var result = new List<Card>(count);
            if (_remaining.Count == 0 || count <= 0)
                return result;

            for (int i = 0; i < count && _remaining.Count > 0; i++)
            {
                int index = _random.Next(_remaining.Count);
                var proto = _remaining[index];
                _remaining.RemoveAt(index);
                result.Add(CreateFromPrototype(proto));
            }

            return result;
        }
        
        private static Card CreateFromPrototype(CardPrototype proto)
        {
            var model = new Card();
            model.InitializeFromPrototype(proto);
            return model;
        }
    }
}