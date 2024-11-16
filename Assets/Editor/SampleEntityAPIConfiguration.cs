// #if UNITY_EDITOR
// using System;
// using System.Collections.Generic;
// using Atomic.Elements;
// using Atomic.Entities;
// using JetBrains.Annotations;
// using UnityEngine;
// // ReSharper disable RedundantNameQualifier
//
// namespace SampleGame
// {
//     [UsedImplicitly]
//     public sealed class SampleEntityAPIConfiguration : IEntityAPIConfiguration
//     {
//         public string Namespace => "SampleGame";
//         public string ClassName => "TagAPI";
//         public string DirectoryPath => "Assets/Scripts/Codegen";
//
//         public IEnumerable<string> GetImports()
//         {
//             yield return nameof(UnityEngine);
//             yield return nameof(Atomic.Elements);
//         }
//
//         public IEnumerable<string> GetTags()
//         {
//             yield return "Character";
//             yield return "Enemy";
//             yield return "Coin";
//         }
//
//         public IDictionary<string, Type> GetValues()
//         {
//             return new Dictionary<string, Type>
//             {
//                 {"Health", typeof(IValue<int>)},
//                 {"Damage", typeof(IValue<int>)},
//                 {"Speed", typeof(IValue<float>)},
//                 {"Transform", typeof(Transform)},
//                 {"GameObject", typeof(GameObject)},
//                 {"Rigidbody", typeof(Rigidbody)}
//             };
//         }
//     }
// }
//
// #endif