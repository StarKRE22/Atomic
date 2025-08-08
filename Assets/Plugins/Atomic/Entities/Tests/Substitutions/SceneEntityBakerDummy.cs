using UnityEngine;

namespace Atomic.Entities
{
    [AddComponentMenu("")]
    public class SceneEntityBakerDummy : SceneEntityBaker<EntityDummy>
    {
        public static int CreateCallCount;
        
        protected override EntityDummy Bake()
        {
            CreateCallCount++;
            return new EntityDummy();
        }
    }
}