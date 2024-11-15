#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace Atomic.API
{
    public sealed class SampleEntityAPIConfig : IEntityAPIConfig
    {
        public string Namespace => "SampleGame";
        public string ClassName => "TagAPI";
        public string DirectoryPath => "Assets/Scripts/Codegen";

        public IEnumerable<string> GetImports()
        {
            yield return "UnityEngine";
        }

        public IEnumerable<string> GetTags()
        {
            yield return "Character";
            yield return "Enemy";
            yield return "Coin";
        }

        public IDictionary<string, Type> GetValues()
        {
            return new Dictionary<string, Type>
            {
                {"Health", typeof(IValue<int>)},
                {"Damage", typeof(IValue<int>)},
                {"Speed", typeof(IValue<float>)},
                {"Transform", typeof(Transform)},
                {"GameObject", typeof(GameObject)},
                {"Rigidbody", typeof(Rigidbody)}
            };
        }
    }
}

#endif