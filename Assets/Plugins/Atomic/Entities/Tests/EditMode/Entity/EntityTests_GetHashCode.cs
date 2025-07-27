using NUnit.Framework;

namespace Atomic.Entities
{
    public sealed partial class EntityTests
    {
        [Test]
        public void GetHashCode_Equals_InstanceId()
        {
            var entity1 = new Entity("1");
            Assert.AreEqual(entity1.InstanceID, entity1.GetHashCode());
        }
    }
}