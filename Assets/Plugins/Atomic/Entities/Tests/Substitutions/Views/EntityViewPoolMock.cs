using System;
using System.Collections.Generic;
using UnityEngine;

namespace Atomic.Entities
{
    public class EntityViewPoolMock : EntityViewPoolBase
    {
        private Dictionary<string, BehaviourEntityView> _rented = new();

        public override EntityView Rent(string name)
        {
            var go = new GameObject($"View-{name}");
            var view = go.AddComponent<BehaviourEntityView>();
            view.name = name;
            _rented[name] = view;
            return view;
        }

        public override void Return(string name, EntityView view)
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