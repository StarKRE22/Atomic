using UnityEngine;

namespace Atomic.Entities
{
    [AddComponentMenu("")]
    public class SceneEntityBakerDummy : SceneEntityBaker<EntityDummy>
    {
        public static int CreateCallCount;

        protected override EntityDummy Create()
        {
            CreateCallCount++;
            return new EntityDummy(); // простая пустая сущность
        }
    }
}