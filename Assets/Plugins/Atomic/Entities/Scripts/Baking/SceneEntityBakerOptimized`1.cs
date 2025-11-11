using UnityEngine;

namespace Atomic.Entities
{
    public abstract class SceneEntityBakerOptimized<E, V> : SceneEntityBaker<E>
        where E : class, IEntity
        where V : EntityView<E>
    {
        [SerializeField]
        private V _view;
        
        [SerializeField]
        private ScriptableEntityFactory<E> _factory;

        [SerializeField]
        private EntityViewPool<E, V> _viewPool;

        protected override E Create()
        {
            E entity = _factory.Create();
            this.Install(entity);
            return entity;
        }

        protected override void Reset()
        {
            base.Reset();
            _view = this.GetComponent<V>();
            _viewPool = FindObjectOfType<EntityViewPool<E, V>>();
        }

        protected abstract void Install(E entity);
        
        protected override void Release()
        {
            _viewPool.Return(_view.Name, _view);
        }
    }
}
