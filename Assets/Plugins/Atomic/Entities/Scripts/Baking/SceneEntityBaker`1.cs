#if UNITY_5_3_OR_NEWER
using UnityEngine;

namespace Atomic.Entities
{
    /// <summary>
    /// Base class for MonoBehaviour-based "bakers" that convert a scene GameObject into a native C# <see cref="IEntity"/> instance.
    /// </summary>
    /// <typeparam name="E">The type of entity produced by this baker. Must implement <see cref="IEntity"/>.</typeparam>
    /// <remarks>
    /// This class is intended to be attached to a GameObject in the Unity scene. 
    /// When <see cref="Bake"/> is called, it creates a new entity and destroys the GameObject.
    /// Derived classes must implement <see cref="Create"/> to construct the entity.
    /// </remarks>
    [HelpURL("https://github.com/StarKRE22/Atomic/blob/main/Docs/Entities/Baking/SceneEntityBaker%601.md")]
    public abstract partial class SceneEntityBaker<E> : MonoBehaviour where E : IEntity
    {
        /// <summary>
        /// Creates a new entity by calling <see cref="Create"/> and destroys the GameObject this baker is attached to.
        /// </summary>
        /// <returns>A new instance of <typeparamref name="E"/>.</returns>
        public E Bake()
        {
            E entity = this.Create();
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
        /// Constructs a new entity instance of type <typeparamref name="E"/>.
        /// </summary>
        /// <returns>A newly created <typeparamref name="E"/> entity.</returns>
        /// <remarks>
        /// Must be implemented by derived classes. This is where the entity's initialization logic should be placed.
        /// </remarks>
        protected abstract E Create();
    }
}
#endif