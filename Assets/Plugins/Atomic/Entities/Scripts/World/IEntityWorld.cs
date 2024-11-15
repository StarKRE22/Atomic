using System;
using System.Collections.Generic;

namespace Atomic.Entities
{
    public interface IEntityWorld
    {
        event Action OnStateChanged;
        event Action<IEntity> OnEntityAdded;
        event Action<IEntity> OnEntityDeleted;

        string Name { get; set; }
        int EntityCount { get; }
        IReadOnlyList<IEntity> Entities { get; }
        int CopyEntitiesTo(IEntity[] results);

        bool AddEntity(IEntity entity);
        bool HasEntity(IEntity entity);
        bool DelEntity(IEntity entity);
        void ClearEntities();
        
        IEntity GetEntityWithTag(int tag);
        IReadOnlyList<IEntity> GetEntitiesWithTag(int tag);
        int GetEntitiesWithTag(int tag, IEntity[] results);

        IEntity GetEntityWithValue(int valueId);
        IReadOnlyList<IEntity> GetEntitiesWithValue(int valueId);
        int GetEntitiesWithValue(int valueId, IEntity[] results);

        void InitEntities();
        void EnableEntities();
        void UpdateEntities(float deltaTime);
        void FixedUpdateEntities(float deltaTime);
        void LateUpdateEntities(float deltaTime);
        void DisableEntities();
        void DisposeEntities();
    }
}