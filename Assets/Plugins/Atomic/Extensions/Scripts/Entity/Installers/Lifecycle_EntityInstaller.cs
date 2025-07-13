using System;
using Atomic.Extensions;
using UnityEngine;

namespace Atomic.Entities
{
    [Serializable]
    public sealed class Lifecycle_EntityInstaller : IEntityInstaller
    {
        [Header("Init")]
        [SerializeReference]
        private IEntityActionAsset[] initActions = default;

        [Header("Enable")]
        [SerializeReference]
        private IEntityActionAsset[] enableActions = default;

        [Header("Disable")]
        [SerializeReference]
        private IEntityActionAsset[] disableActions = default;

        [Header("Dispose")]
        [SerializeReference]
        private IEntityActionAsset[] disposeActions = default;

        [Header("Update")]
        [SerializeReference]
        private IEntityActionAsset_Float[] updateActions = default;

        [Header("Fixed Update")]
        [SerializeReference]
        private IEntityActionAsset_Float[] fixedUpdateActions = default;

        [Header("Late Update")]
        [SerializeReference]
        private IEntityActionAsset_Float[] lateUpdateActions = default;

        public void Install(IEntity entity)
        {
            this.AddInitActions(entity);
            this.AddEnableActions(entity);
            this.AddDisableActions(entity);
            this.AddDisposeActions(entity);
            this.AddUpdateActions(entity);
            this.AddFixedUpdateActions(entity);
            this.AddLateUpdateActions(entity);
        }

        private void AddLateUpdateActions(IEntity entity)
        {
            if (this.lateUpdateActions != null)
            {
                for (int i = 0, count = this.lateUpdateActions.Length; i < count; i++)
                {
                    IEntityActionAsset<float> actionAsset = this.lateUpdateActions[i];
                    if (actionAsset != null)
                    {
                        entity.OnLateUpdated += actionAsset.Create(entity);
                    }
                }
            }
        }

        private void AddFixedUpdateActions(IEntity entity)
        {
            if (this.fixedUpdateActions != null)
            {
                for (int i = 0, count = this.fixedUpdateActions.Length; i < count; i++)
                {
                    IEntityActionAsset<float> actionAsset = this.fixedUpdateActions[i];
                    if (actionAsset != null)
                    {
                        entity.OnFixedUpdated += actionAsset.Create(entity);
                    }
                }
            }
        }

        private void AddUpdateActions(IEntity entity)
        {
            if (this.updateActions != null)
            {
                for (int i = 0, count = this.updateActions.Length; i < count; i++)
                {
                    IEntityActionAsset<float> actionAsset = this.updateActions[i];
                    if (actionAsset != null)
                    {
                        entity.OnUpdated += actionAsset.Create(entity);
                    }
                }
            }
        }

        private void AddDisposeActions(IEntity entity)
        {
            if (this.disposeActions != null)
            {
                for (int i = 0, count = this.disposeActions.Length; i < count; i++)
                {
                    IEntityActionAsset actionAsset = this.disposeActions[i];
                    if (actionAsset != null)
                    {
                        entity.OnDisposed += actionAsset.Create(entity);
                    }
                }
            }
        }

        private void AddDisableActions(IEntity entity)
        {
            if (this.disableActions != null)
            {
                for (int i = 0, count = this.disableActions.Length; i < count; i++)
                {
                    IEntityActionAsset actionAsset = this.disableActions[i];
                    if (actionAsset != null)
                    {
                        entity.OnDisabled += actionAsset.Create(entity);
                    }
                }
            }
        }

        private void AddEnableActions(IEntity entity)
        {
            if (this.enableActions != null)
            {
                for (int i = 0, count = this.enableActions.Length; i < count; i++)
                {
                    IEntityActionAsset actionAsset = this.enableActions[i];
                    if (actionAsset != null)
                    {
                        entity.OnEnabled += actionAsset.Create(entity);
                    }
                }
            }
        }

        private void AddInitActions(IEntity entity)
        {
            if (this.initActions != null)
            {
                for (int i = 0, count = this.initActions.Length; i < count; i++)
                {
                    IEntityActionAsset actionAsset = this.initActions[i];
                    if (actionAsset != null)
                    {
                        entity.OnInitialized += actionAsset.Create(entity);
                    }
                }
            }
        }
    }
}