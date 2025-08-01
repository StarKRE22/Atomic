using System.Collections.Generic;
using NUnit.Framework;
using Unity.PerformanceTesting;

namespace Atomic.Entities
{
    public class EntityCollection_Add_Performance
    {
        private static List<Entity> CreateEntities(int count)
        {
            var list = new List<Entity>(count);
            for (int i = 0; i < count; i++)
                list.Add(new Entity());
            
            return list;
        }

        [Test, Performance]
        public void Add_EntityCollection()
        {
            var entities = CreateEntities(1000);

            Measure.Method(() =>
                {
                    var collection = new EntityCollection<Entity>();
                    for (int i = 0; i < entities.Count; i++)
                        collection.Add(entities[i]);
                })
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("EntityCollection.Add()", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void Add_List()
        {
            var entities = CreateEntities(1000);

            Measure.Method(() =>
                {
                    var list = new List<Entity>();
                    for (int i = 0; i < entities.Count; i++)
                        list.Add(entities[i]);
                })
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("List.Add()", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void Add_HashSet()
        {
            var entities = CreateEntities(1000);

            Measure.Method(() =>
                {
                    var set = new HashSet<Entity>();
                    for (int i = 0; i < entities.Count; i++)
                        set.Add(entities[i]);
                })
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("HashSet.Add()", SampleUnit.Microsecond))
                .Run();
        }
    }
}