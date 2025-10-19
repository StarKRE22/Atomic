using Atomic.Entities;
using UnityEngine;

namespace _DEV
{
    
    
    [CreateAssetMenu(
        fileName = "OrcEntityFactory",
        menuName = "Example/New OrcEntityFactory"
    )]
    public class OrcEntityFactory : ScriptableEntityFactory
    {
        protected override void Install(IEntity entity)
        {
            entity.AddTag("Orc");
        }
    }
}