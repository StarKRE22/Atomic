using UnityEngine;

namespace Atomic.Entities
{
    [AddComponentMenu("")]
    public class DummySceneEntityBaker : SceneEntityBaker<DummyEntity>
    {
        public static int CreateCallCount;
        
        protected override DummyEntity Bake()
        {
            CreateCallCount++;
            return new DummyEntity();
        }
    }
}