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
                    foreach (var t in entities)
                        collection.Add(t);
                })
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("EntityCollection.Add()"))
                .Run();
        }

        [Test, Performance]
        public void Add_List()
        {
            var entities = CreateEntities(1000);

            Measure.Method(() =>
                {
                    var list = new List<Entity>();
                    foreach (var t in entities)
                        list.Add(t);
                })
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("List.Add()"))
                .Run();
        }

        [Test, Performance]
        public void Add_HashSet()
        {
            var entities = CreateEntities(1000);

            Measure.Method(() =>
                {
                    var set = new HashSet<Entity>();
                    foreach (var t in entities)
                        set.Add(t);
                })
                .WarmupCount(5)
                .MeasurementCount(20)
                .SampleGroup(new SampleGroup("HashSet.Add()"))
                .Run();
        }
    }
}