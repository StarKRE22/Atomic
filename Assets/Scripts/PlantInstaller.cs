using Atomic.Elements;
using Atomic.Entities;

namespace SampleGame
{
    public sealed class PlantInstaller : SceneEntityInstaller<IPlant>
    {
        protected override void Install(IPlant entity)
        {
            entity.AddTransform(this.transform);
            entity.AddPlantState(1);
        }
    }
}


