using System;

namespace Atomic.Entities
{
    /// <summary>
    /// Marks a behavior class to indicate that its entity lifecycle callbacks
    /// (Init, Enable, Disable, Dispose) should also be invoked during Unity Editor mode.
    /// This attribute is intended for use only on types implementing <see cref="IBehaviour"/>.
    /// </summary>
    /// 
    /// <remarks>
    /// Useful for simulating runtime logic inside the Unity Editor without entering Play Mode.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class EditModeBehaviourAttribute : Attribute
    {
    }
}