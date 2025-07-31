using NUnit.Framework;

namespace Atomic.Entities
{
    public sealed partial class SceneEntityTests
    {
        [Test]
        public void GetHashCode_Equals_InstanceId()
        {
            var entity1 = SceneEntity.Create("1");
            Assert.AreEqual(entity1.InstanceID, entity1.GetHashCode());
        }
    }
}