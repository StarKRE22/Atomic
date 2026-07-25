using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Atomic.Entities
{
    [Serializable]
    public abstract class PriorityEntitySystem<E> : EntitySystemBase<E>, IDisposable where E : IEntity
    {
        [Serializable]
        public new class Settings : EntitySystemBase<E>.Settings
        {
            public float cooldown = 0.25f;

            [LabelText("High")]
            [PropertyRange(0, 100)]
            [OnValueChanged(nameof(Validate))]
            public int highPercent = 70;

            [LabelText("Mid")]
            [PropertyRange(0, 100)]
            [OnValueChanged(nameof(Validate))]
            public int midPercent = 20;

            [ShowInInspector, ReadOnly]
            public int lowPercent => 100 - this.highPercent - this.midPercent;

            private void Validate()
            {
                this.highPercent = Mathf.Clamp(this.highPercent, 0, 100);
                this.midPercent = Mathf.Clamp(this.midPercent, 0, 100 - this.highPercent);
            }
        }
        
        private struct Entry
        {
            public EntityUpdatePriority Priority;
            public int Index;
        }

        private struct Command
        {
            public enum Type : byte
            {
                Add,
                Remove,
                Priority
            }

            public Type CommandType;
            public E Entity;
            public EntityUpdatePriority Priority;
        }

        private readonly Dictionary<E, Entry> _lookup = new();
        private readonly List<Command> _commands = new(64);

        internal E[] _highEntities;
        internal E[] _midEntities;
        internal E[] _lowEntities;

        internal int _highEntityCount;
        internal int _midEntityCount;
        internal int _lowEntityCount;

        private int _highCursor;
        private int _midCursor;
        private int _lowCursor;

        private bool _isUpdating;

        [ShowInInspector, ReadOnly, HideInEditorMode]
        private readonly Settings _settings;

        [ShowInInspector, ReadOnly, HideInEditorMode]
        private float _priorityTime;

        private readonly IEntityTrigger<E>[] _triggers;

        protected PriorityEntitySystem(
            IReadOnlyEntityCollection<E> source,
            Settings settings,
            params IEntityTrigger<E>[] triggers
        ) : base(source, settings)
        {
            int initialCapacity = Math.Max(16, source.Count);
            _highEntities = new E[initialCapacity];
            _midEntities = new E[initialCapacity];
            _lowEntities = new E[initialCapacity];
            
            _settings = settings;
            _triggers = triggers;
        }

        protected override void OnEnable()
        {
            _priorityTime = _settings.cooldown;

            foreach (IEntityTrigger<E> trigger in _triggers)
                trigger.SetAction(this.ChangePriority);
        }

        #region Add

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected sealed override void OnAddEntity(E entity)
        {
            EntityUpdatePriority priority = this.EvaluatePriority(entity);
            if (_isUpdating)
            {
                _commands.Add(new Command
                {
                    CommandType = Command.Type.Add,
                    Entity = entity,
                    Priority = priority
                });
            }
            else
            {
                AddInternal(entity, priority);
            }

            foreach (IEntityTrigger<E> trigger in _triggers)
                trigger.Track(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void AddInternal(E entity, EntityUpdatePriority priority)
        {
            if (_lookup.ContainsKey(entity))
                return;

            switch (priority)
            {
                case EntityUpdatePriority.High:
                {
                    if (_highEntityCount == _highEntities.Length)
                        Array.Resize(ref _highEntities, _highEntities.Length * 2);

                    _highEntities[_highEntityCount] = entity;
                    _lookup[entity] = new Entry {Priority = priority, Index = _highEntityCount};
                    _highEntityCount++;
                    break;
                }

                case EntityUpdatePriority.Medium:
                {
                    if (_midEntityCount == _midEntities.Length)
                        Array.Resize(ref _midEntities, _midEntities.Length * 2);

                    _midEntities[_midEntityCount] = entity;
                    _lookup[entity] = new Entry {Priority = priority, Index = _midEntityCount};
                    _midEntityCount++;
                    break;
                }

                case EntityUpdatePriority.Low:
                default:
                {
                    if (_lowEntityCount == _lowEntities.Length)
                        Array.Resize(ref _lowEntities, _lowEntities.Length * 2);

                    _lowEntities[_lowEntityCount] = entity;
                    _lookup[entity] = new Entry {Priority = priority, Index = _lowEntityCount};
                    _lowEntityCount++;
                    break;
                }
            }
        }

        #endregion
        
        #region Remove

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected sealed override void OnRemoveEntity(E entity)
        {
            if (_isUpdating)
            {
                _commands.Add(new Command
                {
                    CommandType = Command.Type.Remove,
                    Entity = entity
                });
            }
            else
            {
                this.RemoveInternal(entity);
            }

            foreach (IEntityTrigger<E> trigger in _triggers)
                trigger.Untrack(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void RemoveInternal(E entity)
        {
            if (!_lookup.Remove(entity, out Entry entry))
                return;

            EntityUpdatePriority p = entry.Priority;
            int index = entry.Index;

            switch (p)
            {
                case EntityUpdatePriority.High:
                {
                    int last = _highEntityCount - 1;
                    if (index != last)
                    {
                        E lastEntity = _highEntities[last];
                        _highEntities[index] = lastEntity;

                        Entry lastEntry = _lookup[lastEntity];
                        lastEntry.Index = index;
                        _lookup[lastEntity] = lastEntry;
                    }

                    _highEntities[last] = default;
                    _highEntityCount--;

                    if (_highCursor >= _highEntityCount)
                        _highCursor = 0;
                    break;
                }

                case EntityUpdatePriority.Medium:
                {
                    int last = _midEntityCount - 1;
                    if (index != last)
                    {
                        E lastEntity = _midEntities[last];
                        _midEntities[index] = lastEntity;

                        Entry lastEntry = _lookup[lastEntity];
                        lastEntry.Index = index;
                        _lookup[lastEntity] = lastEntry;
                    }

                    _midEntities[last] = default;
                    _midEntityCount--;

                    if (_midCursor >= _midEntityCount)
                        _midCursor = 0;
                    break;
                }

                case EntityUpdatePriority.Low:
                default: // Low
                {
                    int last = _lowEntityCount - 1;
                    if (index != last)
                    {
                        E lastEntity = _lowEntities[last];
                        _lowEntities[index] = lastEntity;

                        Entry lastEntry = _lookup[lastEntity];
                        lastEntry.Index = index;
                        _lookup[lastEntity] = lastEntry;
                    }

                    _lowEntities[last] = default;
                    _lowEntityCount--;

                    if (_lowCursor >= _lowEntityCount)
                        _lowCursor = 0;
                    break;
                }
            }
        }

        #endregion

        #region Update

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected sealed override void OnUpdate(int batchSize, float deltaTime)
        {
            this.UpdateCooldown(deltaTime);
            this.UpdateEntities(batchSize, deltaTime);
            this.UpdateCommands();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void UpdateCooldown(float deltaTime)
        {
            if (_settings.cooldown <= 0)
                return;

            _priorityTime -= deltaTime;
            if (_priorityTime > 0)
                return;

            this.RecalculatePriorities();
            _priorityTime += _settings.cooldown;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void UpdateEntities(int batchSize, float deltaTime)
        {
            _isUpdating = true;

            int highQuota = batchSize * _settings.highPercent / 100;
            int midQuota = batchSize * _settings.midPercent / 100;
            int lowQuota = batchSize - highQuota - midQuota;

            // ===== HIGH =====
            int highProcessed = 0;
            if (_highEntityCount > 0 && highQuota > 0)
            {
                int toProcess = Math.Min(highQuota, _highEntityCount);
                int cursor = _highCursor;
                int count = _highEntityCount;
                E[] array = _highEntities;

                for (int i = 0; i < toProcess; i++)
                {
                    if (cursor >= count)
                        cursor = 0;

                    this.Update(array[cursor++], deltaTime);
                }

                _highCursor = cursor;
                highProcessed = toProcess;
            }

            int remaining = highQuota - highProcessed;

            // ===== MID =====
            int midBudget = midQuota + (remaining > 0 ? remaining : 0);
            int midProcessed = 0;

            if (_midEntityCount > 0 && midBudget > 0)
            {
                int toProcess = Math.Min(midBudget, _midEntityCount);
                int cursor = _midCursor;
                int count = _midEntityCount;
                E[] array = _midEntities;

                for (int i = 0; i < toProcess; i++)
                {
                    if (cursor >= count)
                        cursor = 0;

                    this.Update(array[cursor++], deltaTime);
                }

                _midCursor = cursor;
                midProcessed = toProcess;
            }

            remaining = midBudget - midProcessed;

            // ===== LOW =====
            int lowBudget = lowQuota + (remaining > 0 ? remaining : 0);

            if (_lowEntityCount > 0 && lowBudget > 0)
            {
                int toProcess = Math.Min(lowBudget, _lowEntityCount);
                int cursor = _lowCursor;
                int count = _lowEntityCount;
                E[] array = _lowEntities;

                for (int i = 0; i < toProcess; i++)
                {
                    if (cursor >= count)
                        cursor = 0;

                    this.Update(array[cursor++], deltaTime);
                }

                _lowCursor = cursor;
            }

            _isUpdating = false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void UpdateCommands()
        {
            // ReSharper disable once ForCanBeConvertedToForeach
            for (int i = 0; i < _commands.Count; i++)
            {
                var cmd = _commands[i];
                switch (cmd.CommandType)
                {
                    case Command.Type.Add:
                        AddInternal(cmd.Entity, cmd.Priority);
                        break;

                    case Command.Type.Remove:
                        RemoveInternal(cmd.Entity);
                        break;

                    case Command.Type.Priority:
                        ChangePriorityInternal(cmd.Entity, cmd.Priority);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            _commands.Clear();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected abstract void Update(E entity, float deltaTime);
        
        #endregion
        
        #region ChangePriority

        protected abstract EntityUpdatePriority EvaluatePriority(E entity);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void RecalculatePriorities()
        {
            foreach (E entity in _source)
            {
                this.ChangePriority(entity);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void ChangePriority(E entity)
        {
            EntityUpdatePriority priority = this.EvaluatePriority(entity);
            if (_isUpdating)
            {
                _commands.Add(new Command
                {
                    CommandType = Command.Type.Priority,
                    Entity = entity,
                    Priority = priority
                });
            }
            else
            {
                ChangePriorityInternal(entity, priority);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ChangePriorityInternal(E entity, EntityUpdatePriority priority)
        {
            if (!_lookup.TryGetValue(entity, out var entry))
                return;

            if (entry.Priority == priority)
                return;

            int index = entry.Index;

            // ===== REMOVE =====
            switch (entry.Priority)
            {
                case EntityUpdatePriority.High:
                {
                    int last = _highEntityCount - 1;

                    if (index != last)
                    {
                        E lastEntity = _highEntities[last];
                        _highEntities[index] = lastEntity;

                        var lastEntry = _lookup[lastEntity];
                        lastEntry.Index = index;
                        _lookup[lastEntity] = lastEntry;
                    }

                    _highEntities[last] = default;
                    _highEntityCount--;
                    _highCursor = Math.Min(_highCursor, _highEntityCount);
                    break;
                }

                case EntityUpdatePriority.Medium:
                {
                    int last = _midEntityCount - 1;

                    if (index != last)
                    {
                        E lastEntity = _midEntities[last];
                        _midEntities[index] = lastEntity;

                        var lastEntry = _lookup[lastEntity];
                        lastEntry.Index = index;
                        _lookup[lastEntity] = lastEntry;
                    }

                    _midEntities[last] = default;
                    _midEntityCount--;
                    _midCursor = Math.Min(_midCursor, _midEntityCount);
                    break;
                }

                case EntityUpdatePriority.Low:
                default:
                {
                    int last = _lowEntityCount - 1;

                    if (index != last)
                    {
                        E lastEntity = _lowEntities[last];
                        _lowEntities[index] = lastEntity;

                        var lastEntry = _lookup[lastEntity];
                        lastEntry.Index = index;
                        _lookup[lastEntity] = lastEntry;
                    }

                    _lowEntities[last] = default;
                    _lowEntityCount--;
                    _lowCursor = Math.Min(_lowCursor, _lowEntityCount);
                    break;
                }
            }

            // ===== ADD =====
            switch (priority)
            {
                case EntityUpdatePriority.High:
                {
                    if (_highEntityCount == _highEntities.Length)
                        Array.Resize(ref _highEntities, _highEntities.Length * 2);

                    _highEntities[_highEntityCount] = entity;
                    _lookup[entity] = new Entry {Priority = priority, Index = _highEntityCount};
                    _highEntityCount++;
                    break;
                }

                case EntityUpdatePriority.Medium:
                {
                    if (_midEntityCount == _midEntities.Length)
                        Array.Resize(ref _midEntities, _midEntities.Length * 2);

                    _midEntities[_midEntityCount] = entity;
                    _lookup[entity] = new Entry {Priority = priority, Index = _midEntityCount};
                    _midEntityCount++;
                    break;
                }

                case EntityUpdatePriority.Low:
                default:
                {
                    if (_lowEntityCount == _lowEntities.Length)
                        Array.Resize(ref _lowEntities, _lowEntities.Length * 2);

                    _lowEntities[_lowEntityCount] = entity;
                    _lookup[entity] = new Entry {Priority = priority, Index = _lowEntityCount};
                    _lowEntityCount++;
                    break;
                }
            }
        }

        #endregion

        public void Dispose()
        {
            Array.Clear(_lowEntities, 0, _lowEntityCount);
            Array.Clear(_midEntities, 0, _midEntityCount);
            Array.Clear(_highEntities, 0, _highEntityCount);

            _commands.Clear();
            _lookup.Clear();
        }
    }
}