#if UNITY_5_3_OR_NEWER
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif
using UnityEngine;

namespace Atomic.Entities
{
    /// <summary>
    /// Base class for MonoBehaviour-based "bakers" that convert a scene GameObject into a native C# <see cref="IEntity"/> instance.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity produced by this baker. Must implement <see cref="IEntity"/>.</typeparam>
    /// <typeparam name="TArgs">The type of construction arguments passed to the entity factory.</typeparam>
    /// <remarks>
    /// This class is intended to be attached to a GameObject in the Unity scene. 
    /// When <see cref="Bake"/> is called, it creates a new entity and destroys the GameObject.
    /// Derived classes must implement <see cref="Create"/> to construct the entity.
    /// </remarks>
    [HelpURL("https://github.com/StarKRE22/Atomic/blob/main/Docs/Entities/Baking/MonoEntityBaker%601.md")]
    public abstract partial class MonoEntityBaker<TEntity, TArgs> : MonoBehaviour
        where TEntity : IEntity
        where TArgs : IArgs
    {
        
#if ODIN_INSPECTOR
        [PropertyOrder(1200)]
        [FoldoutGroup("Optimization")]
        [HideInPlayMode]
#endif
        [Header("Optimization")]
        [Tooltip("Initial number of tags to assign to the entity")]
        [SerializeField]
        private int tagCapacity;

#if ODIN_INSPECTOR
        [PropertyOrder(1200)]
        [FoldoutGroup("Optimization")]
        [HideInPlayMode]
#endif
        [Tooltip("Initial number of values to assign to the entity")]
        [SerializeField]
        private int valueCapacity;

#if ODIN_INSPECTOR
        [PropertyOrder(1200)]
        [FoldoutGroup("Optimization")]
        [HideInPlayMode]
#endif
        [Tooltip("Initial number of behaviours to assign to the entity")]
        [SerializeField]
        private int behaviourCapacity;

#if ODIN_INSPECTOR
        [PropertyOrder(1200)]
        [PropertySpace]
        [LabelText("Extra Settings")]
#endif
        [SerializeField]
        private Entity.Settings settings;
        
        /// <summary>
        /// Creates a new entity by calling <see cref="Create"/> and destroys the GameObject this baker is attached to.
        /// </summary>
        /// <returns>A new instance of <typeparamref name="TEntity"/>.</returns>
        public TEntity Bake(TArgs args)
        {
            TEntity entity = this.Create(
                this.tagCapacity,
                this.valueCapacity,
                this.behaviourCapacity,
                this.settings,
                args
            );
            this.Release();
            return entity;
        }

        /// <summary>
        /// Handles cleanup after the entity has been created.
        /// </summary>
        /// <remarks>
        /// The default implementation destroys the GameObject this baker is attached to.
        /// Override this method if you need to preserve the GameObject 
        /// or perform additional teardown logic.
        /// </remarks>
        protected virtual void Release()
        {
            Destroy(this.gameObject);
        }

        /// <summary>
        /// Constructs a new entity instance of type <typeparamref name="TEntity"/>.
        /// </summary>
        /// <returns>A newly created <typeparamref name="TEntity"/> entity.</returns>
        /// <remarks>
        /// Must be implemented by derived classes. This is where the entity's initialization logic should be placed.
        /// </remarks>
        protected abstract TEntity Create(
            int tagCapacity,
            int valueCapacity,
            int behaviourCapacity,
            Entity.Settings settings,
            TArgs args
        );
    }
}
#endif