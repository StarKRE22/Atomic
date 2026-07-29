// using NUnit.Framework;
// using UnityEngine;
//
// namespace Atomic.Entities
// {
//     public partial class EntityWorldViewTests
//     {
//         private GameObject _go;
//         private EntityWorldView world;
//         private Transform _viewport;
//
//         [OneTimeSetUp]
//         public void OneTimeSetup()
//         {
//             _go = new GameObject("Collection");
//             _viewport = new GameObject("Viewport").transform;
//             _viewport.parent = _go.transform.parent;
//             
//             _pool = _go.AddComponent<EntityViewPool>();
//             _pool.RegisterPrefab("Player", new GameObject("Player").AddComponent<EntityView>());
//             _pool.RegisterPrefab("Enemy", new GameObject("Enemy").AddComponent<EntityView>());
//         }
//
//         [OneTimeTearDown]
//         public void OneTimeTearDown()
//         {
//             Object.DestroyImmediate(_viewport);
//             Object.DestroyImmediate(_go);
//         }
//
//         [SetUp]
//         public void Setup()
//         {
//             world = _go.AddComponent<EntityWorldView>();
//             world.viewport = _viewport;
//             world.viewPool = _pool;
//         }
//
//         [TearDown]
//         public void Teardown()
//         {
//             Object.DestroyImmediate(world);
//         }
//     }
// }