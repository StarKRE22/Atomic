using Atomic.Entities;

namespace SampleGame
{
    public sealed class CharacterInstaller : SceneEntityInstaller
    {
        public override void Install(IEntity entity)
        {
            entity.AddValue("Transform", this.transform);
            entity.AddValue("Health", 5);
            entity.AddValue("Speed", 3);
            entity.AddTag("Character");
        }
    }
}