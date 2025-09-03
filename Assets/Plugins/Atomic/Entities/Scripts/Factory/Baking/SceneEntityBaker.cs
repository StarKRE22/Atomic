#if UNITY_5_3_OR_NEWER
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    /// <summary>
    /// A non-generic alias for <see cref="SceneEntityBaker{IEntity}"/>.
    /// </summary>
    public abstract class SceneEntityBaker : SceneEntityBaker<IEntity>
    {
    }

    public abstract partial class SceneEntityBaker<E> : MonoBehaviour, IEntityFactory<E> where E : IEntity
    {
#if ODIN_INSPECTOR
        [GUIColor(0f, 0.83f, 1f)]
        [DisableInPlayMode]
#endif
        [Tooltip("Should destroy this Game Object after baking?")]
        [SerializeField]
        protected internal bool _destroyAfterBake = true;
        
#if ODIN_INSPECTOR
        [PropertySpace(SpaceBefore = 0, SpaceAfter = 12)]
        [AssetsOnly]
#endif
        [Tooltip("Entity Factory that baker will override")]
        [SerializeField]
        protected internal ScriptableEntityFactory<E> _factory;

        public E Bake()
        {
            E entity = _factory.Create();
            this.Install(entity);

            if (_destroyAfterBake)
                Destroy(this.gameObject);

            return entity;
        }

        protected abstract void Install(E entity);

        E IEntityFactory<E>.Create() => this.Bake();
    }
}
#endif