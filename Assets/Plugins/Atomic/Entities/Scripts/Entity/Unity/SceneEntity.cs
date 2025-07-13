using System;
using System.Collections.Generic;
using UnityEditor;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

using UnityEngine;
using UnityEngine.Serialization;

namespace Atomic.Entities
{
    [AddComponentMenu("Atomic/Entities/Entity")]
    [DisallowMultipleComponent, DefaultExecutionOrder(-1000)]
    public partial class SceneEntity : MonoBehaviour, IEntity
    {
        public event Action OnStateChanged
        {
            add => this.Entity.OnStateChanged += value;
            remove => this.Entity.OnStateChanged -= value;
        }

        public string Name
        {
            get => this.Entity.Name;
            set => this.Entity.Name = value;
        }

        public int Id
        {
            get { return this.Entity.Id; }
            set { this.Entity.Id = value; }
        }

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
        [HideInPlayMode]
#endif
        [Tooltip("Specify the installers that will put values and systems to this context")]
        [Space, SerializeField]
        private List<SceneEntityInstaller> installers;

#if ODIN_INSPECTOR
        [HideInPlayMode]
#endif
        [Tooltip("Specify child entities that will installed with this entity")]
        [Space, SerializeField]
        private List<SceneEntity> children;

        private Entity _entity;
        private bool _installed;

        internal Entity Entity
        {
            get
            {
                if (_entity == null) this.CreateEntity();
                return _entity;
            }
        }

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

        protected virtual void Awake()
        {
            if (this.installOnAwake) this.Install();
        }

        protected virtual void OnDestroy()
        {
            if (_entity == null)
                return;

            if (this.disposeValues) _entity.DisposeValues();
            _entity.UnsubscribeAll();
            s_sceneEntities.Remove(_entity);
        }

        public override string ToString()
        {
            return this.Entity.ToString();
        }

        public override bool Equals(object obj)
        {
            return obj is IEntity entity && this.Entity.Id == entity.Id;
        }

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

        public void Clear()
        {
            this.Entity.Clear();
        }

        public void ResetInstall()
        {
            _installed = false;
        }
    }
}