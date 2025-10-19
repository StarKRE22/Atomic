using Atomic.Entities;

namespace _DEV
{
    public class CharacterFactory : SceneEntityFactory
    {
        protected override void Install(IEntity entity)
        {
            entity.AddTag("Character");
            entity.AddValue("Health", 200);
            entity.AddValue("Damage", 10);
            entity.AddValue("Speed", 5f);
        }
    }
}