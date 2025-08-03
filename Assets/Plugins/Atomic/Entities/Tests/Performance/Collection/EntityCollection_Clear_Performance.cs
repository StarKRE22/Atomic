#if UNITY_6000
using System.Collections.Generic;
using NUnit.Framework;
using Unity.PerformanceTesting;

namespace Atomic.Entities
{
    public class EntityCollection_Clear_Performance
    {
        [Test, Performance]
        public void Clear_EntityCollection()
        {
            var entities = new List<Entity>();
            for (int i = 0; i < 1000; i++)
                entities.Add(new Entity());

            var collection = new EntityCollection<Entity>();
            Measure
                .Method(collection.Clear)
                .SetUp(() => collection.AddRange(entities))
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("EntityCollection.Clear()", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void Clear_List()
        {
            var entities = new List<Entity>();
            for (int i = 0; i < 1000; i++)
                entities.Add(new Entity());

            var list = new List<Entity>();
            Measure
                .Method(list.Clear)
                .SetUp(() => list.AddRange(entities))
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("List.Clear()", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void Clear_HashSet()
        {
            var entities = new List<Entity>();
            for (int i = 0; i < 1000; i++)
                entities.Add(new Entity());

            var hashSet = new HashSet<Entity>();
            Measure
                .Method(hashSet.Clear)
                .SetUp(() => hashSet.UnionWith(entities))
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("HashSet.Clear()", SampleUnit.Microsecond))
                .Run();
        }
    }
}
#endif