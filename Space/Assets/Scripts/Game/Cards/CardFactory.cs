using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame
{
    /// <summary>
    /// Отвечает за: 
    /// 1) постройку моделей из ScriptableObject-прототипов,
    /// 2) инстанс префабов CardView и биндинг модели во вью.
    /// </summary>
    public class CardFactory : MonoBehaviour
    {
        [SerializeField] private CardView _prefab;
        [SerializeField] private Transform _content;

        // Создает список моделей из набора прототипов
        public List<Card> BuildModels(ListCardPrototypes list)
        {
            var result = new List<Card>();
            if (list == null || list.Cards == null) return result;

            foreach (var proto in list.Cards)
            {
                if (proto == null) continue;

                var model = new Card();
                model.InitializeFromPrototype(proto);
                result.Add(model);
            }

            return result;
        }

        // Спавнит CardView и биндит модель
        public CardView CreateView(Card model, Transform parentOverride = null)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            var parent = parentOverride != null ? parentOverride : _content;

            var view = Instantiate(_prefab, parent);
            view.Initialize(model);
            return view;
        }

        // Пакетно создает вьюшки для списка моделей
        public List<CardView> CreateViews(IReadOnlyList<Card> models, Transform parentOverride = null)
        {
            var views = new List<CardView>(models?.Count ?? 0);
            if (models == null) return views;

            foreach (var model in models)
                views.Add(CreateView(model, parentOverride));

            return views;
        }
    }
}