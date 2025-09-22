using NUnit.Framework;
using Unity.PerformanceTesting;

namespace Atomic.Entities
{
    public partial class EntityPerformance
    {
        
        #region GetValue

        [Test, Performance]
        public void GetValue_AsObject()
        {
            var entity = new Entity();
            for (int i = 0; i < N; i++)
                entity.AddValue(i, Dummy);

            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                    {
                        object unused = entity.GetValue(i);
                    }
                })
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("Time", SampleUnit.Microsecond))
                .Run();
        }


        [Test, Performance]
        public void GetValue_AsReference()
        {
            var entity = new Entity();
            for (int i = 0; i < N; i++)
                entity.AddValue(i, SAMPLE);

            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                    {
                        string unused = entity.GetValue<string>(i);
                    }
                })
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("Time", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void GetValue_Primitive()
        {
            var entity = new Entity();
            for (int i = 0; i < N; i++)
                entity.AddValue(i, 777);

            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                    {
                        int unused = entity.GetValue<int>(i);
                    }
                })
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("Time", SampleUnit.Microsecond))
                .Run();
        }


        [Test, Performance]
        public void GetValueUnsafe_AsReference()
        {
            var entity = new Entity();
            for (int i = 0; i < N; i++)
                entity.AddValue(i, SAMPLE);

            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                    {
                        string unused = entity.GetValueUnsafe<string>(i);
                    }
                })
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("Time", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void GetValueUnsafe_AsPrimitive()
        {
            var entity = new Entity();
            for (int i = 0; i < N; i++)
                entity.AddValue(i, 777);

            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                    {
                        int unused = entity.GetValueUnsafe<int>(i);
                    }
                })
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("Time", SampleUnit.Microsecond))
                .Run();
        }

        #endregion

        #region TryGetValue

        [Test, Performance]
        public void TryGetValue_Object()
        {
            var entity = new Entity();
            for (int i = 0; i < N; i++)
                entity.AddValue(i, Dummy);

            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                    {
                        _ = entity.TryGetValue(i, out object unused);
                    }
                })
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("TryGetValue (object)", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void TryGetValue_AsReference()
        {
            var entity = new Entity();
            for (int i = 0; i < N; i++)
                entity.AddValue(i, SAMPLE);

            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                    {
                        _ = entity.TryGetValue(i, out string unused);
                    }
                })
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("TryGetValue<T> (string)", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void TryGetValue_AsPrimitive()
        {
            var entity = new Entity();
            for (int i = 0; i < N; i++)
                entity.AddValue(i, 777);

            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                    {
                        bool success = entity.TryGetValue(i, out int unused);
                    }
                })
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("TryGetValue<T> (int)", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void TryGetValueUnsafe_AsReference()
        {
            var entity = new Entity();
            for (int i = 0; i < N; i++)
                entity.AddValue(i, SAMPLE);

            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                    {
                        bool success = entity.TryGetValueUnsafe(i, out string unused);
                    }
                })
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("TryGetValueUnsafe<T> (string)", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void TryGetValueUnsafe_AsPrimitive()
        {
            var entity = new Entity();
            for (int i = 0; i < N; i++)
                entity.AddValue(i, 777);

            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                    {
                        bool success = entity.TryGetValueUnsafe(i, out int unused);
                    }
                })
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("TryGetValueUnsafe<T> (int)", SampleUnit.Microsecond))
                .Run();
        }

        #endregion
        
        
        #region AddValue

        [Test, Performance]
        public void AddValue_AsObject()
        {
            var entity = new Entity(valueCapacity: N);

            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                        entity.AddValue(i, Dummy);
                })
                .CleanUp(entity.ClearValues)
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("AddValue (object-ref)", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void AddValue_AsPrimitive()
        {
            var entity = new Entity();

            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                    {
                        entity.AddValue(i, 777);
                    }
                })
                .CleanUp(entity.ClearValues)
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("AddValue (object-int)", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void AddValue_AsReference()
        {
            var entity = new Entity();

            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                    {
                        entity.AddValue(i, SAMPLE);
                    }
                })
                .CleanUp(entity.ClearValues)
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("AddValue<T> (string)", SampleUnit.Microsecond))
                .Run();
        }

        #endregion

        #region SetValue

        [Test, Performance]
        public void SetValue_AsObject()
        {
            var entity = new Entity();

            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                    {
                        entity.SetValue(i, Dummy);
                    }
                })
                .CleanUp(entity.ClearValues)
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("Time", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void SetValue_AsPrimitive()
        {
            var entity = new Entity();

            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                    {
                        entity.SetValue(i, 777);
                    }
                })
                .CleanUp(entity.ClearValues)
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("Time", SampleUnit.Microsecond))
                .Run();
        }

        [Test, Performance]
        public void SetValue_AsReference()
        {
            var entity = new Entity();

            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                    {
                        entity.SetValue(i, SAMPLE);
                    }
                })
                .CleanUp(entity.ClearValues)
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("Time", SampleUnit.Microsecond))
                .Run();
        }

        #endregion
        
        [Test, Performance]
        public void HasValue()
        {
            var entity = new Entity();
            for (int i = 0; i < N; i++)
                entity.AddValue(i, SAMPLE);

            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                    {
                        _ = entity.HasValue(i);
                    }
                })
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("Time", SampleUnit.Microsecond))
                .Run();
        }
        
         #region DelValue

        [Test, Performance]
        public void DelValue_AsObject()
        {
            var entity = new Entity();

            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                    {
                        entity.DelValue(i);
                    }
                })
                .SetUp(() =>
                {
                    for (int i = 0; i < N; i++)
                        entity.AddValue(i, Dummy);
                })
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("DelValue (existing)", SampleUnit.Microsecond))
                .Run();
        }
        
        [Test, Performance]
        public void DelValue_AsPrimitive()
        {
            var entity = new Entity();

            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                    {
                        entity.DelValue(i);
                    }
                })
                .SetUp(() =>
                {
                    for (int i = 0; i < N; i++)
                        entity.AddValue(i, 777);
                })
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("DelValue (existing)", SampleUnit.Microsecond))
                .Run();
        }
        
           
        [Test, Performance]
        public void DelValue_AsReference()
        {
            var entity = new Entity();

            Measure.Method(() =>
                {
                    for (int i = 0; i < N; i++)
                    {
                        entity.DelValue(i);
                    }
                })
                .SetUp(() =>
                {
                    for (int i = 0; i < N; i++)
                        entity.AddValue(i, SAMPLE);
                })
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("DelValue (existing)", SampleUnit.Microsecond))
                .Run();
        }

        #endregion
        
        [Test, Performance]
        public void ClearValues()
        {
            var entity = new Entity();

            Measure
                .Method(entity.ClearValues)
                .SetUp(() =>
                {
                    for (int i = 0; i < N; i++)
                        entity.AddValue(i, Dummy);
                })
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("ClearValues (full)", SampleUnit.Microsecond))
                .Run();
        }
        
        
        [Test, Performance]
        public void EnumerateReferenceValues()
        {
            var entity = new Entity();
            for (int i = 0; i < N; i++)
                entity.AddValue(i, Dummy);

            Measure.Method(() =>
                {
                    Entity.ValueEnumerator enumerator = entity.GetValueEnumerator();
                    while (enumerator.MoveNext())
                    {
                        _ = enumerator.Current;
                    }
                })
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("EnumerateTags", SampleUnit.Microsecond))
                .Run();
        }
        
        [Test, Performance]
        public void EnumeratePrimitiveValues()
        {
            var entity = new Entity();
            for (int i = 0; i < N; i++)
                entity.AddValue(i, 777);

            Measure.Method(() =>
                {
                    Entity.ValueEnumerator enumerator = entity.GetValueEnumerator();
                    while (enumerator.MoveNext())
                    {
                        _ = enumerator.Current;
                    }
                })
                .WarmupCount(10)
                .MeasurementCount(30)
                .SampleGroup(new SampleGroup("EnumerateTags", SampleUnit.Microsecond))
                .Run();
        }
    }
}