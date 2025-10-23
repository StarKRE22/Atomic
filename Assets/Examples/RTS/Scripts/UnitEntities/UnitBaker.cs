using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [RequireComponent(typeof(UnitView))]
    public abstract class UnitBaker : SceneEntityBaker<IUnitEntity>
    {
        [SerializeField]
        private UnitFactory _factory;

        protected sealed override IUnitEntity Create()
        {
            IUnitEntity entity = _factory.Create();
            this.Install(entity);
            return entity;
        }

        protected abstract void Install(IUnitEntity entity);

        //Instead of destroying GameObject we can put the prefab into EntityViewPool ;)
        protected override void Release()
        {
            UnitView unitView = this.GetComponent<UnitView>();
            UnitViewPool viewPool = EntryPoint.Instance.ViewPool;
            viewPool.Return(unitView);
        }
    }
}