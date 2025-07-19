using System;
using System.Collections.Generic;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

using UnityEngine;

namespace Atomic.Entities
{
    /// <summary>
    /// MonoBehaviour wrapper for an <see cref="Entity"/> that can be installed from the Unity Scene.
    /// Allows composition through Unity Inspector and automated setup via installers and child entities.
    /// </summary>
    [AddComponentMenu("Atomic/Entities/Entity")]
    [DisallowMultipleComponent, DefaultExecutionOrder(-1000)]
    public abstract partial class SceneEntity<E> : MonoBehaviour, IEntity<E> where E : class, IEntity<E>
    {
        
        private static readonly Dictionary<int, IEntity<E>> s_entities = new();
        private static int s_maxId = -1;

        /// <summary>
        /// Occurs when the state of the entity changes.
        /// </summary>

        /// <summary>
        /// Gets or sets the unique identifier of the entity.
        /// </summary>
        public int Id
        {
            get => this.id;
            set => this.SetId(value);
        }

        /// <summary>
        /// Gets or sets the name of the entity.
        /// </summary>
        public string Name
        {
            get => this.name;
            set => this.name = value;
        }

        private int id;

        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class.
        /// </summary>
        protected Entity()
        {
            this.name = string.Empty;
            this.id = NextId();

            this.InitializeTags();
            this.InitializeValues();
            this.InitializeBehaviours();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class with a specified name.
        /// </summary>
        protected Entity(string name)
        {
            this.name = name;
            this.id = NextId();

            this.InitializeTags();
            this.InitializeValues();
            this.InitializeBehaviours();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class
        /// with optional collections of tags, values, behaviours, and a custom ID.
        /// </summary>
        protected Entity(
            string name = null,
            IEnumerable<int> tags = null,
            IEnumerable<KeyValuePair<int, object>> values = null,
            IEnumerable<IBehaviour<E>> behaviours = null,
            int id = -1
        )
        {
            this.name = name ?? string.Empty;
            this.id = id < 0 ? NextId() : id;

            this.InitializeTags(tags);
            this.InitializeValues(values);
            this.InitializeBehaviours(behaviours);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class with optional capacity settings and custom ID.
        /// </summary>
        protected Entity(
            string name = null,
            int tagCapacity = 0,
            int valueCapacity = 0,
            int behaviourCapacity = 0,
            int id = -1
        )
        {
            this.name = name ?? string.Empty;
            this.id = id < 0 ? NextId() : id;

            this.InitializeTags(in tagCapacity);
            this.InitializeValues(in valueCapacity);
            this.InitializeBehaviours(in behaviourCapacity);
        }

        ~Entity() => this.UnsubscribeAll();

        /// <summary>
        /// Removes all subscriptions and callbacks associated with this entity.
        /// </summary>
        public void UnsubscribeAll()
        {
            InternalUtils.Unsubscribe(ref this.OnStateChanged);

            InternalUtils.Unsubscribe(ref this.OnInitialized);
            InternalUtils.Unsubscribe(ref this.OnEnabled);
            InternalUtils.Unsubscribe(ref this.OnDisabled);
            InternalUtils.Unsubscribe(ref this.OnUpdated);
            InternalUtils.Unsubscribe(ref this.OnFixedUpdated);
            InternalUtils.Unsubscribe(ref this.OnLateUpdated);
            InternalUtils.Unsubscribe(ref this.OnDisposed);

            InternalUtils.Unsubscribe(ref this.OnBehaviourAdded);
            InternalUtils.Unsubscribe(ref this.OnBehaviourDeleted);

            InternalUtils.Unsubscribe(ref this.OnValueAdded);
            InternalUtils.Unsubscribe(ref this.OnValueDeleted);
            InternalUtils.Unsubscribe(ref this.OnValueChanged);

            InternalUtils.Unsubscribe(ref this.OnTagAdded);
            InternalUtils.Unsubscribe(ref this.OnTagDeleted);
        }

        /// <inheritdoc/>
        public override string ToString() => $"{nameof(name)}: {name}, {nameof(id)}: {id}";

        public bool Equals(IEntity<E> other) => this.id == other.Id;

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is IEntity<E> other && other.Id == this.id;

        /// <inheritdoc/>
        public override int GetHashCode() => 

        /// <summary>
        /// Clears all data (tags, values, behaviours) from this entity.
        /// </summary>
        public void Clear()
        {
            this.ClearTags();
            this.ClearValues();
            this.ClearBehaviours();
        }

        private static int NextId()
        {
            do s_maxId++;
            while (s_entities.ContainsKey(s_maxId));
            return s_maxId;
        }

        private void SetId(int id)
        {
            if (id < 0)
                throw new Exception($"Entity Id cannot be negative! Actual: {id}!");

            s_entities.Remove(this.id);
            s_entities[id] = this;

            this.id = id;
        }

        /// <summary>
        /// Finds an entity by its ID.
        /// </summary>
        /// <param name="id">The entity ID.</param>
        /// <param name="entity">The found entity, if any.</param>
        /// <returns>True if the entity exists; otherwise, false.</returns>
        public static bool Find(int id, out IEntity<E> entity) => s_entities.TryGetValue(id, out entity);

        /// <summary>
        /// Resets all static entity tracking information (used internally on play mode enter).
        /// </summary>
#if UNITY_EDITOR
        [InitializeOnEnterPlayMode]
#endif
        public static void ResetAll()
        {
            s_maxId = -1;
            s_entities.Clear();
        }
        
        private const int UNDEFINED_INDEX = -1;

        /// <inheritdoc cref="IEntity.OnStateChanged"/>
        public event Action OnStateChanged;

        /// <summary>
        /// Indicates whether this entity has already been installed.
        /// </summary>
        public bool Installed => _installed;

#if ODIN_INSPECTOR
        [GUIColor(0f, 0.83f, 1f)]
        [HideInPlayMode]
#endif
        [Tooltip("If this option is enabled, the Install() method will be called on Awake()")]
        [SerializeField]
        private bool installOnAwake = true;

#if ODIN_INSPECTOR
        [PropertySpace(SpaceBefore = 0)]
        [GUIColor(1f, 0.92156863f, 0.015686275f)]
        [HideInPlayMode]
        [InfoBox(
            "WARINING: If you create Unity objects or another heavy objects in the Install() method, be sure to turn off!",
            InfoMessageType.Warning,
            nameof(installInEditMode))
        ]
#endif
        [Tooltip(
            "If this option is enabled, the Install() method will be called every time OnValidate is called in Edit Mode")]
        [SerializeField]
        private bool installInEditMode;

#if ODIN_INSPECTOR
        [HideInPlayMode]
#endif
        [Tooltip("Should dispose values when OnDestroy() called")]
        [SerializeField]
        private bool disposeValues = true;

#if ODIN_INSPECTOR
        [GUIColor(0f, 0.83f, 1f)]
#endif
        [Space]
        [SerializeField]
        private bool overrideName;

#if ODIN_INSPECTOR
        [ShowIf(nameof(overrideName))]
#endif
        [SerializeField]
        private string entityName;

#if ODIN_INSPECTOR
        [HideInPlayMode, SceneObjectsOnly]
#endif
        [Tooltip("Specify the installers that will put values and systems to this context")]
        [Space, SerializeField]
        private List<SceneEntityInstaller<E>> installers;

#if ODIN_INSPECTOR
        [HideInPlayMode, SceneObjectsOnly]
#endif
        [Tooltip("Specify child entities that will installed with this entity")]
        [Space, SerializeField]
        private List<SceneEntity<E>> children;

        private bool _installed;
        
        /// <summary>
        /// Installs all configured installers and child entities into this SceneEntity.
        /// </summary>
        public void Install()
        {
            if (_installed)
                return;
            
            _installed = true;

            E me = this as E;
            if (this.installers != null)
            {
                for (int i = 0, count = this.installers.Count; i < count; i++)
                {
                    SceneEntityInstaller<E> installer = this.installers[i];
                    if (installer != null)
                        installer.Install(me);
                    else
                        Debug.LogWarning("SceneEntity: Ops! Detected null installer!", this);
                }
            }

            if (this.children != null)
            {
                for (int i = 0, count = this.children.Count; i < count; i++)
                {
                    SceneEntity<E> child = this.children[i];
                    if (child != null)
                        child.Install();
                    else
                        Debug.LogWarning("SceneEntity: Ops! Detected null child entity!", this);
                }
            }
        }
        
        private void InitEntity()
        {
            string entityName = this.overrideName ? this.entityName : this.name;
            _entity = new E(entityName, _tagCapacity, _valueCapacity, _behaviourCapacity, this);
            s_sceneEntities.TryAdd(_entity, this);
        }

        /// <summary>
        /// Unity Awake callback. Automatically installs the entity if <c>installOnAwake</c> is true.
        /// </summary>
        protected virtual void Awake()
        {
            if (this.installOnAwake) this.Install();
        }

        /// <summary>
        /// Unity OnDestroy callback. Optionally disposes values and cleans up subscriptions.
        /// </summary>
        protected virtual void OnDestroy()
        {
            if (_entity == null)
                return;

            if (this.disposeValues) _entity.DisposeValues();
            _entity.UnsubscribeAll();
            s_sceneEntities.Remove(_entity);
        }

        /// <inheritdoc cref="object.ToString"/>
        public override string ToString() => this.Entity.ToString();

        /// <inheritdoc cref="object.Equals(object)"/>
        public override bool Equals(object obj) => obj is IEntity<E> entity && this.Entity.Id == entity.Id;

        /// <inheritdoc cref="object.GetHashCode"/>
        public override int GetHashCode()
        {
#if UNITY_EDITOR
            try
            {
                return this.id;
            }
            catch (UnityException)
            {
                return -1;
            }
#else
            return this.Entity.GetHashCode();
#endif
        }

 

        /// <summary>
        /// Marks the entity as not installed, allowing reinstallation.
        /// </summary>
        public void ResetInstalledFlag() => _installed = false;
        
        
        
        

        
    }
}