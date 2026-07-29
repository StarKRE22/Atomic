namespace Atomic.Entities
{
    public class MonoEntityAspectSpy : MonoEntityAspect
    {
        public bool Applied { get; private set; }
        
        public bool Discarded { get; private set; }

        public override void Apply(IEntity entity) => Applied = true;
        
        public override void Discard(IEntity entity) => Discarded = true;
    }
}