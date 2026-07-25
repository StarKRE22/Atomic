#if UNITY_5_3_OR_NEWER
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Atomic.Entities
{
    /// <summary>
    /// Non-generic proxy component for exposing and interacting with a <see cref="MonoEntity"/> in the Unity scene.
    /// </summary>
    /// <remarks>
    /// This component serves as a non-generic version of <see cref="MonoEntityProxy{E}"/> and is intended
    /// for convenience when working with base <see cref="MonoEntity"/> types.
    /// </remarks>
    [HelpURL("https://github.com/StarKRE22/Atomic/blob/main/Docs/Entities/Entities/MonoEntityProxy.md")]
    [AddComponentMenu("Atomic/Entities/Entity Proxy")]
    [DisallowMultipleComponent]
    public class MonoEntityProxy : MonoEntityProxy<MonoEntity>
    {
    }
}
#endif