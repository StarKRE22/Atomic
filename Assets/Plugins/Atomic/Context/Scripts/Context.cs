using System;
using System.Collections.Generic;
using UnityEngine;

namespace Atomic.Contexts
{
    public class Context : IContext
    {
        #region Main

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public IContext Parent
        {
            get { return this.parent; }
            set { this.parent = value; }
        }

        private string name;
        private IContext parent;

        public Context(string name = null, IContext parent = null)
        {
            this.name = name;
            this.parent = parent;
        }

        #endregion

        #region Values

        public event Action<int, object> OnValueAdded;
        public event Action<int, object> OnValueDeleted;
        public event Action<int, object> OnValueChanged;

        public IReadOnlyDictionary<int, object> Values => this.values;

        private readonly Dictionary<int, object> values = new();

        public bool HasValue(int key)
        {
            return this.values.ContainsKey(key);
        }

        public T GetValue<T>(int key)
        {
            if (this.values.TryGetValue(key, out object value))
            {
                return (T) value;
            }

            return default;
        }

        public object GetValue(int key)
        {
            if (this.values.TryGetValue(key, out var value))
            {
                return value;
            }

            return null;
        }

        public bool TryGetValue<T>(int id, out T value)
        {
            if (this.values.TryGetValue(id, out object field))
            {
                value = (T) field;
                return true;
            }

            value = default;
            return false;
        }

        public bool TryGetValue(int id, out object value)
        {
            return this.values.TryGetValue(id, out value);
        }

        public bool AddValue(int key, object value)
        {
            if (this.values.TryAdd(key, value))
            {
                this.OnValueAdded?.Invoke(key, value);
                return true;
            }

            return false;
        }

        public void SetValue(int key, object value)
        {
            this.values[key] = value;
            this.OnValueChanged?.Invoke(key, value);
        }

        public bool DelValue(int key)
        {
            if (this.values.Remove(key, out object removed))
            {
                this.OnValueDeleted?.Invoke(key, removed);
                return true;
            }

            return false;
        }

        public bool DelValue(int key, out object removed)
        {
            if (this.values.Remove(key, out removed))
            {
                this.OnValueDeleted?.Invoke(key, removed);
                return true;
            }

            return false;
        }

        #endregion

        #region Systems

        public event Action<IContextSystem> OnSystemAdded;
        public event Action<IContextSystem> OnSystemRemoved;

        public IReadOnlyCollection<IContextSystem> Systems => this.systems;

        private readonly HashSet<IContextSystem> systems = new();

        public T GetSystem<T>() where T : IContextSystem
        {
            foreach (IContextSystem system in this.systems)
            {
                if (system is T tSystem)
                {
                    return tSystem;
                }
            }

            return default;
        }

        public bool TryGetSystem<T>(out T result) where T : IContextSystem
        {
            foreach (IContextSystem system in this.systems)
            {
                if (system is T tSystem)
                {
                    result = tSystem;
                    return true;
                }
            }

            result = default;
            return false;
        }

        public bool HasSystem(IContextSystem system)
        {
            return this.systems.Contains(system);
        }

        public bool HasSystem<T>() where T : IContextSystem
        {
            foreach (IContextSystem system in this.systems)
            {
                if (system is T)
                {
                    return true;
                }
            }

            return false;
        }

        public bool AddSystem<T>() where T : IContextSystem, new()
        {
            return this.AddSystem(new T());
        }

        public bool AddSystem(IContextSystem system)
        {
            if (!this.systems.Add(system))
            {
                return false;
            }

            if (this.initialized && system is IContextInit initSystem)
            {
                initSystem.Init(this);
            }

            if (this.enabled && system is IContextEnable enableSystem)
            {
                enableSystem.Enable(this);
            }

            if (system is IContextUpdate update)
            {
                this.updates.Add(update);
            }

            if (system is IContextFixedUpdate fixedUpdate)
            {
                this.fixedUpdates.Add(fixedUpdate);
            }

            if (system is IContextLateUpdate lateUpdate)
            {
                this.lateUpdates.Add(lateUpdate);
            }

            this.OnSystemAdded?.Invoke(system);
            return true;
        }

        public bool DelSystem<T>() where T : IContextSystem
        {
            T system = this.GetSystem<T>();
            if (system == null)
            {
                return false;
            }

            return this.DelSystem(system);
        }

        public bool DelSystem(IContextSystem system)
        {
            if (!this.systems.Remove(system))
            {
                return false;
            }

            if (system is IContextUpdate update)
            {
                this.updates.Remove(update);
            }

            if (system is IContextFixedUpdate fixedUpdate)
            {
                this.fixedUpdates.Remove(fixedUpdate);
            }

            if (system is IContextLateUpdate lateUpdate)
            {
                this.lateUpdates.Remove(lateUpdate);
            }

            if (this.enabled && system is IContextDisable disableSystem)
            {
                disableSystem.Disable(this);
            }

            if (this.initialized && system is IContextDispose disposeSystem)
            {
                disposeSystem.Dispose(this);
            }

            this.OnSystemRemoved?.Invoke(system);
            return true;
        }

        #endregion

        #region Lifecycle

        public event Action OnInitiazized;
        public event Action OnEnabled;
        public event Action OnDisabled;
        public event Action OnDisposed;

        public event Action<float> OnUpdated;
        public event Action<float> OnFixedUpdated;
        public event Action<float> OnLateUpdated;

        public bool Initialized => this.initialized;
        public bool Enabled => this.enabled;

        private bool initialized;
        private bool enabled;

        private readonly List<IContextUpdate> updates = new();
        private readonly List<IContextFixedUpdate> fixedUpdates = new();
        private readonly List<IContextLateUpdate> lateUpdates = new();

        private readonly List<IContextUpdate> _updateCache = new();
        private readonly List<IContextFixedUpdate> _fixedUpdateCache = new();
        private readonly List<IContextLateUpdate> _lateUpdateCache = new();

        public void Init()
        {
            if (this.initialized)
            {
                Debug.LogWarning($"Context with name {name} is already initialized!");
                return;
            }

            foreach (IContextSystem system in this.systems)
            {
                if (system is IContextInit initSystem)
                {
                    initSystem.Init(this);
                }
            }

            this.initialized = true;
            this.OnInitiazized?.Invoke();
        }

        public void Enable()
        {
            if (this.enabled)
            {
                Debug.LogWarning($"Context with name {name} is already enabled!");
                return;
            }

            if (!this.initialized)
            {
                Debug.LogError($"You can enable context only after initialize! Context: {name}");
                return;
            }

            foreach (IContextSystem system in this.systems)
            {
                if (system is IContextEnable enableSystem)
                {
                    enableSystem.Enable(this);
                }
            }

            this.enabled = true;
            this.OnEnabled?.Invoke();
        }

        public void Disable()
        {
            if (!this.enabled)
            {
                Debug.LogWarning($"Context with {name} is not enabled!");
                return;
            }

            foreach (IContextSystem system in this.systems)
            {
                if (system is IContextDisable disableSystem)
                {
                    disableSystem.Disable(this);
                }
            }

            this.enabled = false;
            this.OnDisabled?.Invoke();
        }

        public void Dispose()
        {
            if (!this.initialized)
            {
                Debug.LogWarning($"Context with name {name} is not initialized!");
            }

            if (this.enabled)
            {
                this.Disable();
            }

            foreach (IContextSystem system in this.systems)
            {
                if (system is IContextDispose destroySystem)
                {
                    destroySystem.Dispose(this);
                }
            }

            this.initialized = false;
            this.OnDisposed?.Invoke();
        }

        public void OnUpdate(float deltaTime)
        {
            if (!this.enabled)
            {
                Debug.LogError($"You can update context if it's enabled! Context {name}");
                return;
            }

            int count = this.updates.Count;
            if (count != 0)
            {
                _updateCache.Clear();
                _updateCache.AddRange(this.updates);

                for (int i = 0; i < count; i++)
                {
                    IContextUpdate update = _updateCache[i];
                    update.Update(this, deltaTime);
                }
            }

            this.OnUpdated?.Invoke(deltaTime);
        }

        public void OnFixedUpdate(float deltaTime)
        {
            if (!this.enabled)
            {
                Debug.LogError($"You can update context if it's enabled! Context {name}");
                return;
            }

            int count = this.fixedUpdates.Count;
            if (count != 0)
            {
                _fixedUpdateCache.Clear();
                _fixedUpdateCache.AddRange(this.fixedUpdates);

                for (int i = 0; i < count; i++)
                {
                    IContextFixedUpdate updateSystem = _fixedUpdateCache[i];
                    updateSystem.FixedUpdate(this, deltaTime);
                }
            }

            this.OnFixedUpdated?.Invoke(deltaTime);
        }

        public void OnLateUpdate(float deltaTime)
        {
            if (!this.enabled)
            {
                Debug.LogError($"You can update context if it's enabled! Context {name}");
                return;
            }

            int count = this.lateUpdates.Count;
            if (count != 0)
            {
                _lateUpdateCache.Clear();
                _lateUpdateCache.AddRange(this.lateUpdates);

                for (int i = 0; i < count; i++)
                {
                    IContextLateUpdate updateSystem = _lateUpdateCache[i];
                    updateSystem.LateUpdate(this, deltaTime);
                }
            }

            this.OnLateUpdated?.Invoke(deltaTime);
        }

        #endregion

        public void Clear()
        {
            this.values.Clear();
            this.systems.Clear();
            this.updates.Clear();
            this.fixedUpdates.Clear();
            this.lateUpdates.Clear();
        }
    }
}