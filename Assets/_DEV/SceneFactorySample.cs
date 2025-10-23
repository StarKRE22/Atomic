using Atomic.Entities;
using BeginnerGame;
using RTSGame;
using ShooterGame;

namespace _DEV
{
    public class SceneFactorySample : SceneEntityFactory<IUnitEntity>
    {
        public override IUnitEntity Create()
        {
            return new UnitEntity("AAA");
        }
    }
}