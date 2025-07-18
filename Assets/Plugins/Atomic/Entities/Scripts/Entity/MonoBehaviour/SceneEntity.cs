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
    public partial class SceneEntity : MonoBehaviour, IEntity
    {
        /// <inheritdoc cref="IEntity.OnStateChanged"/>
        public event Action OnStateChanged
        {
            add => this.Entity.OnStateChanged += value;
            remove => this.Entity.OnStateChanged -= value;
        }

        /// <inheritdoc cref="IEntity.Name"/>
        public string Name
        {
            get => this.Entity.Name;
            set => this.Entity.Name = value;
        }

        /// <inheritdoc cref="IEntity.Id"/>
        public int Id
        {
            get => this.Entity.Id;
            set => this.Entity.Id = value;
        }

        /// <summary>
        /// Indicates whether this entity has already been installed.
        /// </summary>
        public bool Installed => _installed;
        
        /// <summary>
        /// Internal access to the wrapped <see cref="Entity"/>. Will create one if missing.
        /// </summary>
        internal Entity Entity
        {
            get
            {
                if (_entity == null) this.CreateEntity();
                return _entity;
            }
        }

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
        private List<SceneEntityInstaller> installers;

#if ODIN_INSPECTOR
        [HideInPlayMode, SceneObjectsOnly]
#endif
        [Tooltip("Specify child entities that will installed with this entity")]
        [Space, SerializeField]
        private List<SceneEntity> children;

        private Entity _entity;
        private bool _installed;
        
        /// <summary>
        /// Installs all configured installers and child entities into this SceneEntity.
        /// </summary>
        public void Install()
        {
            if (_installed)
                return;
            
            _installed = true;

            if (this.installers != null)
            {
                for (int i = 0, count = this.installers.Count; i < count; i++)
                {
                    SceneEntityInstaller installer = this.installers[i];
                    if (installer != null)
                        installer.Install(this);
                    else
                        Debug.LogWarning("SceneEntity: Ops! Detected null installer!", this);
                }
            }

            if (this.children != null)
            {
                for (int i = 0, count = this.children.Count; i < count; i++)
                {
                    SceneEntity child = this.children[i];
                    if (child != null)
                        child.Install();
                    else
                        Debug.LogWarning("SceneEntity: Ops! Detected null child entity!", this);
                }
            }
        }
        
        private void CreateEntity()
        {
            string entityName = this.overrideName ? this.entityName : this.name;
            _entity = new Entity(entityName, _tagCapacity, _valueCapacity, _behaviourCapacity, this);
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
        public override bool Equals(object obj) => obj is IEntity entity && this.Entity.Id == entity.Id;

        /// <inheritdoc cref="object.GetHashCode"/>
        public override int GetHashCode()
        {
#if UNITY_EDITOR
            try
            {
                return Entity.GetHashCode();
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
        /// Clears the internal state of the entity.
        /// </summary>
        public void Clear() => this.Entity.Clear();

        /// <summary>
        /// Marks the entity as not installed, allowing reinstallation.
        /// </summary>
        public void ResetInstalledFlag() => _installed = false;
    }
}