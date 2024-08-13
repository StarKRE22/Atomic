using System;
using System.Collections.Generic;
using System.Reflection;

#if UNITY_EDITOR
using UnityEditor;
#endif

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

using UnityEngine;
using UnityEngine.Events;

namespace Atomic.Entities
{
    [AddComponentMenu("Atomic/Entities/Entity")]
    [DisallowMultipleComponent, DefaultExecutionOrder(-1000)]
    public sealed class SceneEntity : MonoBehaviour, IEntity
    {
        #region Main

        public int InstanceId
        {
            get { return entity.InstanceId; }
        }

        public string Name
        {
            get { return entity.Name; }
            set { entity.Name = value; }
        }

        public bool Installed
        {
            get { return _installed; }
        }

        public Entity Entity
        {
            get { return this.entity; }
        }

        private readonly Entity entity = new();

        [SerializeField]
        private bool autoRefresh = true;

        [SerializeField]
        private bool installOnAwake = true;

#if ODIN_INSPECTOR
        [DisableInPlayMode]
        [PropertySpace(SpaceAfter = 8)]
#endif
        [SerializeField, Space]
        private List<SceneEntityInstallerBase> installPipeline;

        [SerializeField, Space]
        private UnityEvent onInstalled;

        private void Awake()
        {
            _sceneEntityMap.Add(entity, this);

            if (this.installOnAwake)
            {
                this.Install();
            }
        }

        private void OnDestroy()
        {
            _sceneEntityMap.Remove(entity);
        }
        
        public override string ToString()
        {
            return $"{base.ToString()}, {nameof(entity)}: {entity}";
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is SceneEntity other && this.Equals(other);
        }

        public bool Equals(SceneEntity other)
        {
            return Equals(this.entity, other.entity);
        }

        public override int GetHashCode()
        {
            return this.entity.GetHashCode();
        }

        #endregion

        #region Lifecycle

        public event Action OnInitialized
        {
            add => entity.OnInitialized += value;
            remove => entity.OnInitialized -= value;
        }

        public event Action OnDisposed
        {
            add => entity.OnDisposed += value;
            remove => entity.OnDisposed -= value;
        }

        public event Action OnEnabled
        {
            add => entity.OnEnabled += value;
            remove => entity.OnEnabled -= value;
        }

        public event Action OnDisabled
        {
            add => entity.OnDisabled += value;
            remove => entity.OnDisabled -= value;
        }

        public event Action<float> OnUpdated
        {
            add => entity.OnUpdated += value;
            remove => entity.OnUpdated -= value;
        }

        public event Action<float> OnFixedUpdated
        {
            add => entity.OnFixedUpdated += value;
            remove => entity.OnFixedUpdated -= value;
        }

        public event Action<float> OnLateUpdated
        {
            add => entity.OnLateUpdated += value;
            remove => entity.OnLateUpdated -= value;
        }

        public bool Initialized
        {
            get { return entity.Initialized; }
        }

        public bool Enabled
        {
            get { return entity.Enabled; }
            set => this.enabled = value;
        }

        private bool _installed;

        public void Install()
        {
            if (!_installed)
            {
                this.InstallInternal();
                this.onInstalled?.Invoke();
                _installed = true;
            }
        }

        private void InstallInternal()
        {
            this.entity.Name = this.name;

            if (this.installPipeline != null)
            {
                for (int i = 0, count = this.installPipeline.Count; i < count; i++)
                {
                    SceneEntityInstallerBase installer = this.installPipeline[i];
                    if (installer != null)
                    {
                        installer.Install(this.entity);
                    }
                }
            }
        }

        public void Init()
        {
            entity.Init();
        }

        public void Enable()
        {
            entity.Enable();
        }

        public void Disable()
        {
            entity.Disable();
        }

        public void Dispose()
        {
            entity.Dispose();
        }

        public void OnUpdate(float deltaTime)
        {
            entity.OnUpdate(deltaTime);
        }

        public void OnFixedUpdate(float deltaTime)
        {
            entity.OnFixedUpdate(deltaTime);
        }

        public void OnLateUpdate(float deltaTime)
        {
            entity.OnLateUpdate(deltaTime);
        }

        #endregion

        #region Tags

        public event Action<IEntity, int> OnTagAdded
        {
            add => entity.OnTagAdded += value;
            remove => entity.OnTagAdded -= value;
        }

        public event Action<IEntity, int> OnTagDeleted
        {
            add => entity.OnTagDeleted += value;
            remove => entity.OnTagDeleted -= value;
        }

        public event Action<IEntity> OnTagsCleared
        {
            add => entity.OnTagsCleared += value;
            remove => entity.OnTagsCleared -= value;
        }

        public IReadOnlyCollection<int> Tags
        {
            get { return entity.Tags; }
        }

        public bool DelTag(int tag)
        {
            return entity.DelTag(tag);
        }

        public bool ClearTags()
        {
            return entity.ClearTags();
        }

        public bool HasTag(int tag)
        {
            return entity.HasTag(tag);
        }

        public bool AddTag(int tag)
        {
            return entity.AddTag(tag);
        }

        #endregion

        #region Values

        public event Action<IEntity, int, object> OnValueAdded
        {
            add => entity.OnValueAdded += value;
            remove => entity.OnValueAdded -= value;
        }

        public event Action<IEntity, int, object> OnValueDeleted
        {
            add => entity.OnValueDeleted += value;
            remove => entity.OnValueDeleted -= value;
        }

        public event Action<IEntity, int, object> OnValueChanged
        {
            add => entity.OnValueChanged += value;
            remove => entity.OnValueChanged -= value;
        }

        public event Action<IEntity> OnValuesCleared
        {
            add => entity.OnValuesCleared += value;
            remove => entity.OnValuesCleared -= value;
        }

        public IReadOnlyDictionary<int, object> Values
        {
            get { return entity.Values; }
        }

        public T GetValue<T>(int id)
        {
            return entity.GetValue<T>(id);
        }

        public bool TryGetValue<T>(int id, out T value)
        {
            return entity.TryGetValue(id, out value);
        }

        public object GetValue(int id)
        {
            return entity.GetValue(id);
        }

        public bool TryGetValue(int id, out object value)
        {
            return entity.TryGetValue(id, out value);
        }

        public bool AddValue(int id, object value)
        {
            return entity.AddValue(id, value);
        }

        public bool DelValue(int id)
        {
            return entity.DelValue(id);
        }

        public bool DelValue(int id, out object removed)
        {
            return entity.DelValue(id, out removed);
        }

        public void SetValue(int id, object value)
        {
            entity.SetValue(id, value);
        }

        public void SetValue(int id, object value, out object previous)
        {
            entity.SetValue(id, value, out previous);
        }

        public bool HasValue(int id)
        {
            return entity.HasValue(id);
        }

        public bool ClearValues()
        {
            return entity.ClearValues();
        }

        #endregion

        #region Behaviours

        public event Action<IEntity, IEntityBehaviour> OnBehaviourAdded
        {
            add => entity.OnBehaviourAdded += value;
            remove => entity.OnBehaviourAdded -= value;
        }

        public event Action<IEntity, IEntityBehaviour> OnBehaviourDeleted
        {
            add => entity.OnBehaviourDeleted += value;
            remove => entity.OnBehaviourDeleted -= value;
        }

        public event Action<IEntity> OnBehavioursCleared
        {
            add => entity.OnBehavioursCleared += value;
            remove => entity.OnBehavioursCleared -= value;
        }

        public IReadOnlyCollection<IEntityBehaviour> Behaviours
        {
            get { return entity.Behaviours; }
        }

        public bool AddBehaviour(IEntityBehaviour behaviour)
        {
            return entity.AddBehaviour(behaviour);
        }

        public bool DelBehaviour(IEntityBehaviour behaviour)
        {
            return entity.DelBehaviour(behaviour);
        }

        public bool HasBehaviour(IEntityBehaviour behaviour)
        {
            return entity.HasBehaviour(behaviour);
        }

        public bool ClearBehaviours()
        {
            return entity.ClearBehaviours();
        }

        #endregion

        #region Static

        private static readonly Dictionary<IEntity, SceneEntity> _sceneEntityMap = new();

        public static SceneEntity Instantiate(SceneEntity prefab, Transform parent)
        {
            SceneEntity entity = GameObject.Instantiate(prefab, parent);
            entity.Install();
            return entity;
        }
        
        public static SceneEntity Instantiate(
            string name = null,
            IEnumerable<int> tags = null,
            IReadOnlyDictionary<int, object> values = null,
            IEnumerable<IEntityBehaviour> behaviours = null,
            bool installOnAwake = false
        )
        {
            GameObject gameObject = new GameObject(name);
            SceneEntity sceneEntity = gameObject.AddComponent<SceneEntity>();
            sceneEntity.Name = name;
            sceneEntity.installOnAwake = installOnAwake;

            sceneEntity.AddTags(tags);
            sceneEntity.AddValues(values);
            sceneEntity.AddBehaviours(behaviours);

            sceneEntity.Install();
            return sceneEntity;
        }

        public static SceneEntity Cast(IEntity entity)
        {
            if (entity == null)
            {
                return null;
            }
            
            if (entity is SceneEntity sceneEntity)
            {
                return sceneEntity;
            }

            if (entity is SceneEntityProxy proxy)
            {
                return proxy.source;
            }

            _sceneEntityMap.TryGetValue(entity, out sceneEntity);
            return sceneEntity;
        }

        public static bool TryCast(IEntity entity, out SceneEntity result)
        {
            if (entity == null)
            {
                result = null;
                return false;
            }
            
            if (entity is SceneEntity sceneEntity)
            {
                result = sceneEntity;
                return true;
            }

            if (entity is SceneEntityProxy proxy)
            {
                result = proxy.source;
                return true;
            }

            return _sceneEntityMap.TryGetValue(entity, out result);
        }

        #endregion

        #region Editor

        private void OnValidate()
        {
#if UNITY_EDITOR
            this.AutoRefresh();
#endif
        }

#if UNITY_EDITOR
        private void AutoRefresh()
        {
            if (!this.autoRefresh || EditorApplication.isPlaying || EditorApplication.isCompiling)
            {
                return;
            }

            try
            {
                this.SetRefreshCallbackToInstallers();
                this.RefreshInEditMode();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void SetRefreshCallbackToInstallers()
        {
            foreach (SceneEntityInstallerBase installer in this.installPipeline)
            {
                if (installer != null)
                {
                    installer.mRefreshCallback = this.RefreshInEditMode;
                }
            }
        }

#if ODIN_INSPECTOR
        [FoldoutGroup("Debug")]
        [PropertyOrder(95)]
        [Button("Test Install"), HideInPlayMode]
        [GUIColor(0f, 0.83f, 1f)]
        [PropertySpace(SpaceAfter = 8, SpaceBefore = 8)]
#endif
        private void RefreshInEditMode()
        {
            if (entity == null)
            {
                return;
            }

            bool isPrefab = PrefabUtility.GetPrefabInstanceHandle(this.gameObject) == this.gameObject;
            if (!isPrefab)
            {
                this.DisableInEditMode();
                this.DisposeInEditMode();
            }

            entity.Clear();
            this.InstallInternal();

            if (!isPrefab)
            {
                entity.Name = this.name;
                this.InitInEditMode();
                this.EnableInEditMode();
            }
        }

        private bool ExecuteAlwaysAnnotated(IEntityBehaviour entity)
        {
            return entity.GetType().IsDefined(typeof(ExecuteAlways));
        }

        private void DisableInEditMode()
        {
            if (!entity.Enabled)
            {
                return;
            }

            foreach (IEntityBehaviour behaviour in entity.Behaviours)
            {
                if (behaviour is IEntityDisable disable && this.ExecuteAlwaysAnnotated(behaviour))
                {
                    disable.Disable(entity);
                }
            }
        }

        private void DisposeInEditMode()
        {
            if (!entity.Initialized)
            {
                return;
            }

            foreach (IEntityBehaviour behaviour in entity.Behaviours)
            {
                if (behaviour is IEntityDispose dispose && this.ExecuteAlwaysAnnotated(behaviour))
                {
                    dispose.Dispose(entity);
                }
            }
        }

        private void EnableInEditMode()
        {
            if (entity.Enabled)
            {
                return;
            }

            foreach (IEntityBehaviour behaviour in entity.Behaviours)
            {
                if (behaviour is IEntityEnable dispose && this.ExecuteAlwaysAnnotated(behaviour))
                {
                    dispose.Enable(entity);
                }
            }
        }

        private void InitInEditMode()
        {
            if (entity.Initialized)
            {
                return;
            }

            foreach (IEntityBehaviour behaviour in entity.Behaviours)
            {
                if (behaviour is IEntityInit dispose && this.ExecuteAlwaysAnnotated(behaviour))
                {
                    dispose.Init(entity);
                }
            }
        }
#endif

        #endregion

        #region Debug

#if UNITY_EDITOR
        public static ITagNameFormatter TagNameFormatter;
        public static IValueNameFormatter ValueNameFormatter;

        public interface ITagNameFormatter
        {
            string GetName(int id);
        }

        public interface IValueNameFormatter
        {
            string GetName(int id);
        }
#endif

#if UNITY_EDITOR && ODIN_INSPECTOR

        [FoldoutGroup("Debug")]
        [ShowInInspector, ReadOnly]
        [LabelText("Name")]
        private string NameDebug
        {
            get { return entity?.Name ?? this.name; }
        }

        [FoldoutGroup("Debug")]
        [LabelText("Initialized")]
        [ShowInInspector, ReadOnly]
        private bool InitializedDebug
        {
            get { return entity?.Initialized ?? false; }
        }

        [FoldoutGroup("Debug")]
        [ShowInInspector, ReadOnly]
        [LabelText("Enabled")]
        private bool EnabledDebug
        {
            get { return entity?.Enabled ?? false; }
        }

        ///Tags
        private static readonly List<TagElement> _tagElememtsCache = new();

        [InlineProperty]
        private struct TagElement : IComparable<TagElement>
        {
            [ShowInInspector, ReadOnly]
            internal string name;
            internal readonly int id;

            public TagElement(string name, int id)
            {
                this.name = name;
                this.id = id;
            }

            public int CompareTo(TagElement other)
            {
                return string.Compare(name, other.name, StringComparison.Ordinal);
            }
        }

        [FoldoutGroup("Debug")]
        [LabelText("Tags")]
        [ShowInInspector, PropertyOrder(100)]
        [ListDrawerSettings(
            CustomRemoveElementFunction = nameof(RemoveTagElement),
            CustomRemoveIndexFunction = nameof(RemoveTagElementAt),
            HideAddButton = true
        )]
        private List<TagElement> TagElememts
        {
            get
            {
                _tagElememtsCache.Clear();

                IReadOnlyCollection<int> tags = entity?.Tags;
                if (tags == null)
                {
                    return _tagElememtsCache;
                }

                foreach (int tag in tags)
                {
                    string name = TagNameFormatter?.GetName(tag) ?? tag.ToString();
                    _tagElememtsCache.Add(new TagElement(name, tag));
                }

                _tagElememtsCache.Sort();
                return _tagElememtsCache;
            }
            set
            {
                /** noting... **/
            }
        }

        private void RemoveTagElement(TagElement tagElement)
        {
            if (entity != null) this.DelTag(tagElement.id);
        }

        private void RemoveTagElementAt(int index)
        {
            if (entity != null) this.DelTag(TagElememts[index].id);
        }

        ///Values
        private static readonly List<ValueElement> _valueElementsCache = new();

        [InlineProperty]
        private struct ValueElement : IComparable<ValueElement>
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

            public int CompareTo(ValueElement other)
            {
                return string.Compare(name, other.name, StringComparison.Ordinal);
            }
        }

        [FoldoutGroup("Debug")]
        [LabelText("State")]
        [ShowInInspector, PropertyOrder(100)]
        [ListDrawerSettings(
            CustomRemoveElementFunction = nameof(RemoveValueElement),
            CustomRemoveIndexFunction = nameof(RemoveValueElementAt),
            HideAddButton = true
        )]
        private List<ValueElement> ValueElements
        {
            get
            {
                _valueElementsCache.Clear();

                IReadOnlyDictionary<int, object> values = entity?.Values;
                if (values == null)
                {
                    return _valueElementsCache;
                }

                foreach ((int id, object value) in values)
                {
                    string name = ValueNameFormatter?.GetName(id) ?? id.ToString();
                    _valueElementsCache.Add(new ValueElement(name, value, id));
                }

                _valueElementsCache.Sort();
                return _valueElementsCache;
            }

            set
            {
                /** noting... **/
            }
        }

        private void RemoveValueElement(ValueElement valueElement)
        {
            if (entity != null) this.DelValue(valueElement.id);
        }

        private void RemoveValueElementAt(int index)
        {
            if (entity != null) this.DelValue(ValueElements[index].id);
        }


        ///Logics
        private static readonly List<LogicElement> _logicElementsCache = new();

        [InlineProperty]
        private struct LogicElement : IComparable<LogicElement>
        {
            [ShowInInspector, ReadOnly]
            public string name;

            internal readonly IEntityBehaviour value;

            public LogicElement(string name, IEntityBehaviour value)
            {
                this.name = name;
                this.value = value;
            }

            public int CompareTo(LogicElement other)
            {
                return string.Compare(name, other.name, StringComparison.Ordinal);
            }
        }

        [FoldoutGroup("Debug")]
        [LabelText("Behaviours")]
        [ShowInInspector, PropertyOrder(100)]
        [ListDrawerSettings(
            CustomRemoveElementFunction = nameof(RemoveLogicElement),
            CustomRemoveIndexFunction = nameof(RemoveLogicElementAt),
            HideAddButton = true
        )]
        private List<LogicElement> LogicElements
        {
            get
            {
                _logicElementsCache.Clear();

                var behaviours = entity?.Behaviours;
                if (behaviours == null)
                {
                    return _logicElementsCache;
                }

                foreach (var behaviour in behaviours)
                {
                    string name = behaviour.GetType().Name;
                    _logicElementsCache.Add(new LogicElement(name, behaviour));
                }

                _logicElementsCache.Sort();
                return _logicElementsCache;
            }
            set
            {
                /** noting... **/
            }
        }

        private void RemoveLogicElement(LogicElement logicElement)
        {
            if (entity != null) this.DelBehaviour(logicElement.value);
        }

        private void RemoveLogicElementAt(int index)
        {
            if (entity != null) this.DelBehaviour(LogicElements[index].value);
        }

        ///Add Element 
        [PropertySpace]
        [FoldoutGroup("Debug")]
        [Button("Install")]
        [ShowInInspector, PropertyOrder(100), HideInEditorMode]
        private void InstallDebug(IEntityInstaller installer)
        {
            installer.Install(this);
        }
#endif

        #endregion
    }
}