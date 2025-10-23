using Atomic.Entities;

namespace RTSGame
{
    public sealed class UnitViewPool : EntityViewPool<IUnitEntity, UnitView>
    {
        public void Return(UnitView view) => this.Return(view.Name, view);
    }
}