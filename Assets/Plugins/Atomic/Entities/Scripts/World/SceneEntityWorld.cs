using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    [AddComponentMenu("Atomic/Entities/Entity World")]
    [DisallowMultipleComponent]
    [DefaultExecutionOrder(-1000)]
    public sealed class SceneEntityWorld : MonoBehaviour, IEntityWorld
    {
        #region Main

        private readonly EntityWorld _world = new();

        [SerializeField]
        private bool autoRefresh = true;

        [Space]
        [SerializeField]
        private bool unityName = true;

        [SerializeField]
        private bool scanEntities = true;

#if ODIN_INSPECTOR
        [ShowIf(nameof(scanEntities))]
#endif
        [SerializeField]
        private bool includeInactiveOnScan = true;

#if ODIN_INSPECTOR
        [FoldoutGroup("Debug")]
        [ShowInInspector, ReadOnly]
#endif
        public string Name
        {
            get => _world.Name;
            set => _world.Name = value;
        }

        private void Awake()
        {
            if (this.unityName)
            {
                _world.Name = name;
            }
        }

        private void Start()
        {
            if (this.scanEntities)
            {
                this.AddAllEntitiesFromScene(this.includeInactiveOnScan);
            }
        }

        private void OnValidate()
        {
#if UNITY_EDITOR
            if (this.autoRefresh && !EditorApplication.isPlaying && !EditorApplication.isCompiling)
            {
                try
                {
                    this.RefreshInEditMode();
                }
                catch (Exception)
                {
                    // ignored
                }
            }
#endif
        }

#if ODIN_INSPECTOR
        [FoldoutGroup("Debug")]
        [PropertyOrder(95)]
        [Button("Refresh"), HideInPlayMode]
        [GUIColor(0f, 0.83f, 1f)]
        [PropertySpace(SpaceAfter = 8, SpaceBefore = 8)]
#endif
        private void RefreshInEditMode()
        {
            _world.Name = name;
            this.AddAllEntitiesFromScene();
        }

        #endregion

        #region Entities

        public event Action OnStateChanged
        {
            add => _world.OnStateChanged += value;
            remove => _world.OnStateChanged -= value;
        }

        public event Action<IEntity> OnEntityAdded
        {
            add => _world.OnEntityAdded += value;
            remove => _world.OnEntityAdded -= value;
        }

        public event Action<IEntity> OnEntityDeleted
        {
            add => _world.OnEntityDeleted += value;
            remove => _world.OnEntityDeleted -= value;
        }

#if ODIN_INSPECTOR
        [FoldoutGroup("Debug")]
        [ShowInInspector, ReadOnly]
#endif
        public int EntityCount
        {
            get { return _world.EntityCount; }
        }

#if ODIN_INSPECTOR
        [FoldoutGroup("Debug")]
        [ShowInInspector, ReadOnly]
#endif
        public IReadOnlyList<IEntity> Entities
        {
            get { return _world.Entities; }
        }

        public IEntity GetEntityWithTag(int tag)
        {
            return _world.GetEntityWithTag(tag);
        }

        public IReadOnlyList<IEntity> GetEntitiesWithTag(int tag)
        {
            return _world.GetEntitiesWithTag(tag);
        }

        public int GetEntitiesWithTag(int tag, IEntity[] results)
        {
            return _world.GetEntitiesWithTag(tag, results);
        }

        public IEntity GetEntityWithValue(int valueId)
        {
            return _world.GetEntityWithValue(valueId);
        }

        public IReadOnlyList<IEntity> GetEntitiesWithValue(int valueId)
        {
            return _world.GetEntitiesWithValue(valueId);
        }

        public int GetEntitiesWithValue(int valueId, IEntity[] results)
        {
            return _world.GetEntitiesWithValue(valueId, results);
        }

#if ODIN_INSPECTOR
        [Title("Entities")]
        [Button]
#endif
        public bool HasEntity(IEntity entity)
        {
            return _world.HasEntity(entity);
        }

        public int CopyEntitiesTo(IEntity[] results)
        {
            return _world.CopyEntitiesTo(results);
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public bool AddEntity(IEntity entity)
        {
            return _world.AddEntity(entity);
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public bool DelEntity(IEntity entity)
        {
            return _world.DelEntity(entity);
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public void ClearEntities()
        {
            _world.ClearEntities();
        }

        #endregion

        #region Lifecycle

#if ODIN_INSPECTOR
        [Title("Lifecycle")]
        [Button]
#endif
        public void InitEntities()
        {
            _world.InitEntities();
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public void EnableEntities()
        {
            _world.EnableEntities();
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public void DisableEntities()
        {
            _world.DisableEntities();
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public void DisposeEntities()
        {
            _world.DisposeEntities();
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public void UpdateEntities(float deltaTime)
        {
            _world.UpdateEntities(deltaTime);
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public void FixedUpdateEntities(float deltaTime)
        {
            _world.FixedUpdateEntities(deltaTime);
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public void LateUpdateEntities(float deltaTime)
        {
            _world.LateUpdateEntities(deltaTime);
        }

        #endregion

        #region Static

        public static SceneEntityWorld Instantiate(
            string name = null,
            bool scanEntities = false,
            params IEntity[] entities
        )
        {
            GameObject gameObject = new GameObject(name);
            SceneEntityWorld world = gameObject.AddComponent<SceneEntityWorld>();
            world.Name = name;
            world.scanEntities = scanEntities;

            world.AddEntities(entities);
            return world;
        }

        #endregion
    }
}