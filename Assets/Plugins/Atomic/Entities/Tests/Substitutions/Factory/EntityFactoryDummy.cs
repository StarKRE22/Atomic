namespace Atomic.Entities
{
    public class EntityFactoryDummy : IEntityFactory<EntityDummy>
    {
        public EntityDummy Created;

        public EntityDummy Create()
        {
            this.Created = new EntityDummy();
            return this.Created;
        }
    }
}