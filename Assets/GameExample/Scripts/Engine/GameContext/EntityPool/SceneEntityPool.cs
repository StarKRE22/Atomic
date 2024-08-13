using System.Collections.Generic;
using Atomic.Entities;
using UnityEngine;

namespace GameExample.Engine
{
    public sealed class SceneEntityPool : IEntityPool
    {
        private readonly SceneEntity prefab;

        private readonly Transform worldContainer;
        private readonly Transform poolContainer;

        private readonly Queue<SceneEntity> queue = new();

        public SceneEntityPool(
            SceneEntity prefab,
            Transform poolContainer,
            Transform worldContainer,
            int initalCount = 0
        )
        {
            this.prefab = prefab;
            this.poolContainer = poolContainer;
            this.worldContainer = worldContainer;

            for (int i = 0; i < initalCount; i++)
            {
                SceneEntity entity = SceneEntity.Instantiate(this.prefab, this.poolContainer);
                this.queue.Enqueue(entity);
            }
        }

        public IEntity Rent()
        {
            if (this.queue.TryDequeue(out SceneEntity entity))
            {
                entity.transform.SetParent(this.worldContainer);
                return entity;
            }

            return SceneEntity.Instantiate(this.prefab, this.worldContainer);
        }

        public void Return(IEntity entity)
        {
            SceneEntity sceneEntity = SceneEntity.Cast(entity);
            sceneEntity.transform.SetParent(this.poolContainer);
            this.queue.Enqueue(sceneEntity);
        }
    }
}