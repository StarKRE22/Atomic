using System.Collections.Generic;
using NUnit.Framework;

namespace Atomic.Entities
{
    public sealed class EntityTests
    {
        [Test]
        public void GetValueUnsafe_List()
        {
            //Arrange:
            const int key = 1;
            var list = new List<string> {"Vasya", "Petya"};
            
            Entity entity = new Entity();
            entity.AddValue(key, list);

            //Act:
            var @unsafe = entity.GetValueUnsafe<IList<string>>(key);
            @unsafe.Remove("Petya");

            //Assert:
            Assert.AreEqual(list, @unsafe);
            Assert.AreEqual(1, list.Count);
            Assert.IsFalse(list.Contains("Petya"));
        }
        
        [Test]
        public void GetValueUnsafe_Primitive()
        {
            //Arrange:
            const int key = 1;
            
            Entity entity = new Entity();
            entity.AddValue(key, 555);

            //Act:
            var @unsafe = entity.GetValueUnsafe<int>(key);

            //Assert:
            Assert.AreEqual(555, @unsafe);
        }
        
        
        [Test]
        public void GetValueUnsafe_RefString()
        {
            //Arrange:
            const int key = 1;
            
            Entity entity = new Entity();
            entity.AddValue(key, "Vasya");

            //Act:
            ref string @unsafe = ref entity.GetValueUnsafe<string>(key);
            @unsafe = "Petya";

            //Assert:
            Assert.AreEqual("Petya", entity.GetValueUnsafe<string>(key));
        }
        
        [Test]
        public void GetValueUnsafe_RefPrimitive()
        {
            //Arrange:
            const int key = 1;
            
            Entity entity = new Entity();
            entity.AddValue(key, 555);

            //Act:
            ref int @unsafe = ref entity.GetValueUnsafe<int>(key);
            @unsafe = 777;

            //Assert:
            Assert.AreEqual(777, entity.GetValueUnsafe<int>(key));
        }
    }
}