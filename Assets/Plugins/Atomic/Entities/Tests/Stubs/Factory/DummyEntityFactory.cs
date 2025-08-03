namespace Atomic.Entities
{
    public class DummyEntityFactory : IEntityFactory<DummyEntity>
    {
        public DummyEntity Created;

        public DummyEntity Create()
        {
            Created = new DummyEntity();
            return Created;
        }
    }
}