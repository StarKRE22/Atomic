using System;
using System.Collections.Generic;

namespace Atomic.Entities
{
    public class TestSceneEntityScope : IDisposable
    {
        private readonly List<MonoEntity> _entities = new();
        
        public MonoEntity NewEntity(in MonoEntity.CreateArgs args = default)
        {
            MonoEntity entity = MonoEntity.Create(in args);
            _entities.Add(entity);
            return entity;
        }

        public void Dispose()
        {
            for (int i = 0, count = _entities.Count; i < count; i++) 
                _entities[i].Dispose();

            _entities.Clear();
        }
    }
}