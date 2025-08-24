using System;
using System.Collections.Generic;
using UnityEngine;

namespace Atomic.Entities
{
    public class EntityViewPoolMock : EntityViewPoolAbstract
    {
        private Dictionary<string, EntityView> _rented = new();

        public override EntityViewBase Rent(string name)
        {
            var go = new GameObject($"View-{name}");
            var view = go.AddComponent<EntityView>();
            view.name = name;
            _rented[name] = view;
            return view;
        }

        public override void Return(string name, EntityViewBase view)
        {
            DestroyImmediate(view.gameObject);
            _rented.Remove(name);
        }

        public override void Clear()
        {
            throw new NotImplementedException();
        }
    }
}