using NUnit.Framework;

namespace Atomic.Entities
{
    public sealed partial class MonoEntityTests
    {
        [Test]
        public void GetHashCode_Equals_InstanceId()
        {
            var entity1 = MonoEntity.Create("1");
            Assert.AreEqual(entity1.InstanceID, entity1.GetHashCode());
        }
    }
}