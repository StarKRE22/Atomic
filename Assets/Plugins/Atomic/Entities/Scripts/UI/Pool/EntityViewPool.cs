namespace Atomic.Entities
{
    public class EntityViewPool : EntityViewPool<string, IEntity, EntityView>
    {
        protected override string GetKey(EntityView view) => view.Name;
    }
}