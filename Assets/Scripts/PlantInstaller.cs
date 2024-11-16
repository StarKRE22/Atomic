using Atomic.Elements;
using Atomic.Entities;

namespace SampleGame
{
    public sealed class PlantInstaller : SceneEntityInstaller<Plant>
    {
        protected override void Install(Plant entity)
        {
            entity.AddTransform(this.transform);
        }
    }
}