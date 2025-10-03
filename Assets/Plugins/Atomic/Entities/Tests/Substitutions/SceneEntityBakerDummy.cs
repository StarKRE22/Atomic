using UnityEngine;

namespace Atomic.Entities
{
    [AddComponentMenu("")]
    public class SceneEntityBakerDummy : SceneEntityFactoryProxy<EntityDummy>
    {
        public static int CreateCallCount;

        protected override void Install(EntityDummy entity)
        {
            CreateCallCount++;
        }
        
        protected void Awake()
        {
            if (_factory == null)
                _factory = ScriptableObject.CreateInstance<ScriptableEntityFactoryStub>();
        }
    }
    
    public class ScriptableEntityFactoryStub : ScriptableEntityFactory<EntityDummy>
    {
        public override EntityDummy Create()
        {
            return new EntityDummy(); // простая пустая сущность
        }
    }

}