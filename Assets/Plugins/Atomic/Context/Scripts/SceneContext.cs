using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Contexts
{
    [AddComponentMenu("Atomic/Context/Context")]
    [DisallowMultipleComponent, DefaultExecutionOrder(-1000)]
    public class SceneContext : MonoBehaviour, IContext
    {
        #region Main

        public string Name
        {
            get => this.context.Name;
            set => this.context.Name = value;
        }

        public IContext Parent
        {
            get { return this.context.Parent; }
            set { this.context.Parent = value; }
        }

        private readonly Context context = new();

        #endregion

        #region Install
        
        [SerializeField]
        private bool installOnAwake = true;

#if ODIN_INSPECTOR
        [ShowIf(nameof(installOnAwake))]
#endif
        [Space]
        [SerializeField]
        public SceneContext initialParent;

        [Space]
        [SerializeField]
        public List<SceneContextInstallerBase> installers = new();

        [Space]
        [SerializeField]
        private List<SceneContext> children;

        [Space]
        [SerializeField]
        private UnityEvent onInstalled;

        private bool installed;
        
        public void Install()
        {
            this.Install(this.initialParent);
        }

        public void Install(IContext parent)
        {
            if (!installed)
            {
                this.InstallInternal(parent);
                this.installed = true;
            }
        }

        private void InstallInternal()
        {
            this.InstallInternal(this.initialParent);
        }

        private void InstallInternal(IContext parent)
        {
            this.context.Clear();
            this.context.Name = this.name;
            this.context.Parent = parent;

            for (int i = 0, count = this.installers.Count; i < count; i++)
            {
                SceneContextInstallerBase installer = this.installers[i];
                if (installer != null)
                {
                    installer.Install(this);
                }
            }

            foreach (var child in this.children)
            {
                child.Install(this);
            }

            this.onInstalled?.Invoke();
        }

        #endregion

        #region Unity
        
        [Space]
        [SerializeField]
        private bool refreshOnValidate = true;

        protected virtual void Awake()
        {
            if (this.installOnAwake)
            {
                this.Install();
            }
        }

        protected virtual void OnValidate()
        {
            if (!this.refreshOnValidate)
            {
                return;
            }

#if UNITY_EDITOR
            try
            {
                if (!EditorApplication.isPlaying)
                {
                    this.InstallInternal();
                }
            }
            catch (Exception)
            {
                // ignored
            }
#endif
        }

        #endregion

        #region Values

        public event Action<int, object> OnValueAdded
        {
            add => context.OnValueAdded += value;
            remove => context.OnValueAdded -= value;
        }

        public event Action<int, object> OnValueDeleted
        {
            add => context.OnValueDeleted += value;
            remove => context.OnValueDeleted -= value;
        }

        public event Action<int, object> OnValueChanged
        {
            add => context.OnValueChanged += value;
            remove => context.OnValueChanged -= value;
        }

        public IReadOnlyDictionary<int, object> Values => context.Values;

        public bool AddValue(int key, object value)
        {
            return context.AddValue(key, value);
        }

        public void SetValue(int key, object value)
        {
            context.SetValue(key, value);
        }

        public bool DelValue(int key)
        {
            return context.DelValue(key);
        }

        public bool DelValue(int key, out object removed)
        {
            return context.DelValue(key, out removed);
        }

        public bool HasValue(int key)
        {
            return context.HasValue(key);
        }

        public T GetValue<T>(int key)
        {
            return context.GetValue<T>(key);
        }

        public object GetValue(int key)
        {
            return context.GetValue(key);
        }

        public bool TryGetValue<T>(int id, out T value)
        {
            return context.TryGetValue(id, out value);
        }

        public bool TryGetValue(int id, out object value)
        {
            return context.TryGetValue(id, out value);
        }

        #endregion

        #region Systems

        public event Action<IContextSystem> OnSystemAdded
        {
            add => context.OnSystemAdded += value;
            remove => context.OnSystemAdded -= value;
        }

        public event Action<IContextSystem> OnSystemRemoved
        {
            add => context.OnSystemRemoved += value;
            remove => context.OnSystemRemoved -= value;
        }


        public IReadOnlyCollection<IContextSystem> Systems => context.Systems;

        public T GetSystem<T>() where T : IContextSystem
        {
            return context.GetSystem<T>();
        }

        public bool TryGetSystem<T>(out T result) where T : IContextSystem
        {
            return context.TryGetSystem(out result);
        }

        public bool AddSystem(IContextSystem system)
        {
            return context.AddSystem(system);
        }

        public bool AddSystem<T>() where T : IContextSystem, new()
        {
            return context.AddSystem<T>();
        }

        public bool DelSystem(IContextSystem system)
        {
            return context.DelSystem(system);
        }

        public bool DelSystem<T>() where T : IContextSystem
        {
            return context.DelSystem<T>();
        }

        public bool HasSystem(IContextSystem system)
        {
            return context.HasSystem(system);
        }

        public bool HasSystem<T>() where T : IContextSystem
        {
            return context.HasSystem<T>();
        }

        public void Clear()
        {
            this.context.Clear();
        }

        #endregion

        #region Lifecycle

        public event Action OnInitiazized
        {
            add => context.OnInitiazized += value;
            remove => context.OnInitiazized -= value;
        }

        public event Action OnEnabled
        {
            add => context.OnEnabled += value;
            remove => context.OnEnabled -= value;
        }

        public event Action OnDisabled
        {
            add => context.OnDisabled += value;
            remove => context.OnDisabled -= value;
        }

        public event Action OnDisposed
        {
            add => context.OnDisposed += value;
            remove => context.OnDisposed -= value;
        }

        public event Action<float> OnUpdated
        {
            add => context.OnUpdated += value;
            remove => context.OnUpdated -= value;
        }

        public event Action<float> OnFixedUpdated
        {
            add => context.OnFixedUpdated += value;
            remove => context.OnFixedUpdated -= value;
        }

        public event Action<float> OnLateUpdated
        {
            add => context.OnLateUpdated += value;
            remove => context.OnLateUpdated -= value;
        }

        public bool Initialized => context.Initialized;

        public bool Enabled => context.Enabled;

        public void Init()
        {
            context.Init();
        }

        public void Enable()
        {
            context.Enable();
        }

        public void Disable()
        {
            context.Disable();
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public void OnUpdate(float deltaTime)
        {
            context.OnUpdate(deltaTime);
        }

        public void OnFixedUpdate(float deltaTime)
        {
            context.OnFixedUpdate(deltaTime);
        }

        public void OnLateUpdate(float deltaTime)
        {
            context.OnLateUpdate(deltaTime);
        }

        #endregion

        #region Debug

#if UNITY_EDITOR && ODIN_INSPECTOR
        ///Main

        [FoldoutGroup("Debug")]
        [ShowInInspector, ReadOnly]
        [HideInEditorMode, LabelText("Name")]
        private string NameDebug
        {
            get { return this.context?.Name ?? "undefined"; }
            set
            {
                if (this.context != null) this.context.Name = value;
            }
        }

        [FoldoutGroup("Debug")]
        [ShowInInspector, ReadOnly, PropertySpace]
        [HideInEditorMode, LabelText("Initialized")]
        private bool InitializedDebug
        {
            get { return this.context?.Initialized ?? default; }
        }

        [FoldoutGroup("Debug")]
        [ShowInInspector, ReadOnly]
        [HideInEditorMode, LabelText("Enabled")]
        private bool EnabledDebug
        {
            get { return this.context?.Enabled ?? default; }
        }

        ///Values
        private static readonly List<ValueElement> _valueElementsCache = new();

        private struct ValueElement
        {
            [HorizontalGroup(200), ShowInInspector, ReadOnly, HideLabel]
            public string name;

            [HorizontalGroup, ShowInInspector, HideLabel]
            public object value;

            internal readonly int id;

            public ValueElement(string name, object value, int id)
            {
                this.name = name;
                this.value = value;
                this.id = id;
            }
        }

        [FoldoutGroup("Debug")]
        [LabelText("Values")]
        [ShowInInspector, PropertyOrder(100)]
        [ListDrawerSettings(
            CustomRemoveElementFunction = nameof(RemoveValueElement),
            CustomRemoveIndexFunction = nameof(RemoveValueElementAt),
            HideAddButton = true
        )]
        private List<ValueElement> ValuesDebug
        {
            get
            {
                _valueElementsCache.Clear();

                IReadOnlyDictionary<int, object> values = this.context?.Values;
                if (values == null)
                {
                    return _valueElementsCache;
                }

                foreach ((int id, object value) in values)
                {
                    string name = debugUtils.ConvertToName(id);
                    _valueElementsCache.Add(new ValueElement(name, value, id));
                }

                return _valueElementsCache;
            }

            set
            {
                /** noting... **/
            }
        }

        private void RemoveValueElement(ValueElement valueElement)
        {
            if (this.context != null) this.DelValue(valueElement.id);
        }

        private void RemoveValueElementAt(int index)
        {
            if (this.context != null) this.DelValue(ValuesDebug[index].id);
        }

        ///Logics
        private static readonly List<SystemElement> _systemElementsCache = new();

        [InlineProperty]
        private struct SystemElement
        {
            [ShowInInspector, ReadOnly, HideLabel]
            public string name;

            internal readonly IContextSystem value;

            public SystemElement(IContextSystem value)
            {
                this.name = value.GetType().Name;
                this.value = value;
            }
        }

        [FoldoutGroup("Debug")]
        [LabelText("Systems")]
        [ShowInInspector, PropertyOrder(100)]
        [ListDrawerSettings(
            CustomRemoveElementFunction = nameof(RemoveSystemElement),
            CustomRemoveIndexFunction = nameof(RemoveSystemElementAt)
        )]
        private List<SystemElement> SystemsDebug
        {
            get
            {
                _systemElementsCache.Clear();

                var logics = this.context?.Systems;
                if (logics == null)
                {
                    return _systemElementsCache;
                }

                foreach (var system in logics)
                {
                    _systemElementsCache.Add(new SystemElement(system));
                }

                return _systemElementsCache;
            }
            set
            {
                /** noting... **/
            }
        }

        [FoldoutGroup("Debug")]
        [ShowInInspector, PropertyOrder(100)]
        [Button("Refresh"), HideInPlayMode]
        private void Refresh()
        {
            this.InstallInternal();
        }

        [FoldoutGroup("Debug")]
        [ShowInInspector, PropertyOrder(100)]
        [Button("Add System"), HideInEditorMode]
        private void AddSystemDebug(IContextSystem system)
        {
            this.context.AddSystem(system);
        }

        [FoldoutGroup("Debug")]
        [ShowInInspector, PropertyOrder(100)]
        [Button("Add Value"), HideInEditorMode]
        private void AddValueDebug(int key, object value)
        {
            this.context.AddValue(key, value);
        }

        private void RemoveSystemElement(SystemElement systemElement)
        {
            if (this.context != null) this.DelSystem(systemElement.value);
        }

        private void RemoveSystemElementAt(int index)
        {
            if (this.context != null) this.DelSystem(SystemsDebug[index].value);
        }
#endif

        #endregion
    }
}