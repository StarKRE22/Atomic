#if UNITY_5_3_OR_NEWER
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    public abstract partial class SceneEntityFactoryProxy<E> : MonoBehaviour, IEntityFactory<E> where E : IEntity
    {


#if ODIN_INSPECTOR
        [PropertySpace(SpaceBefore = 0, SpaceAfter = 12)]
        [AssetsOnly]
#endif
        [Tooltip("Entity Factory that baker will override")]
        [SerializeField]
        protected internal ScriptableEntityFactory<E> _factory;

     
        protected abstract void Install(E entity);

        E IEntityFactory<E>.Create() => this.Bake();
    }
}
#endif