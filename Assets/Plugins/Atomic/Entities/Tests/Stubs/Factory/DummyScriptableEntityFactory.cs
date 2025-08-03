namespace Atomic.Entities
{
    
    public class DummyScriptableEntityFactory : ScriptableEntityFactory<DummyEntity>
    {
        public override DummyEntity Create() => new();
    }
}