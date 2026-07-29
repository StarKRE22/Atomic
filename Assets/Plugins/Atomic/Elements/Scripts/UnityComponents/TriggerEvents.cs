#if UNITY_5_3_OR_NEWER
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Atomic.Elements
{
    /// <summary>
    /// Provides safe and consistent trigger notifications by wrapping Unity's
    /// <see cref="OnTriggerEnter"/>, <see cref="OnTriggerExit"/> and
    /// <see cref="OnTriggerStay"/> callbacks into managed C# events.
    ///
    /// This component:
    /// <list type="bullet">
    /// <item><description>Tracks currently entered colliders using a <see cref="HashSet{T}"/>.</description></item>
    /// <item><description>Avoids duplicate <c>Enter</c> and <c>Exit</c> events.</description></item>
    /// <item><description>Automatically clears all tracked colliders on <see cref="OnDisable"/> and fires proper exit events.</description></item>
    /// <item><description>Prevents stale (destroyed or disabled) collider references from remaining in the collection.</description></item>
    /// </list>
    /// 
    /// Ideal for gameplay systems that must reliably know what is inside
    /// an area at any moment: buffs, debuffs, trigger-based interactions, hazard zones, etc.
    /// </summary>
    [AddComponentMenu("Atomic/Elements/Trigger Events")]
    [DisallowMultipleComponent]
    [HelpURL("https://github.com/StarKRE22/Atomic/blob/main/Docs/Elements/Utils/TriggerEvents.md")]
    public sealed class TriggerEvents : MonoBehaviour
    {
        /// <summary>
        /// Occurs when a collider enters the trigger zone.
        /// Fired only once per collider until it exits.
        /// </summary>
        public event Action<Collider> OnEntered;

        /// <summary>
        /// Occurs when a collider exits the trigger zone.
        /// Fired only if the collider was previously registered as entered.
        /// </summary>
        public event Action<Collider> OnExited;

        /// <summary>
        /// Occurs every frame while a collider remains inside the trigger zone.
        /// This event is invoked only if listeners are subscribed.
        /// </summary>
        public event Action<Collider> OnStay;

        /// <summary>
        /// Gets a read-only collection of colliders currently inside the trigger zone.
        /// 
        /// The collection is automatically cleared when:
        /// <list type="bullet">
        /// <item><description>The trigger object is disabled.</description></item>
        /// <item><description>The other collider is destroyed.</description></item>
        /// </list>
        /// </summary>
        public IReadOnlyCollection<Collider> CurrentColliders => _currentColliders;

        private readonly HashSet<Collider> _currentColliders = new();

        /// <summary>
        /// Unity callback invoked when another collider enters the trigger.
        /// Registers the collider and raises the <see cref="OnEntered"/> event
        /// only if the collider was not already tracked.
        /// </summary>
        private void OnTriggerEnter(Collider other)
        {
            if (_currentColliders.Add(other))
                OnEntered?.Invoke(other);
        }

        /// <summary>
        /// Unity callback invoked when another collider exits the trigger.
        /// Removes the collider and raises the <see cref="OnExited"/> event
        /// only if the collider was previously tracked.
        /// </summary>
        private void OnTriggerExit(Collider other)
        {
            if (_currentColliders.Remove(other))
                OnExited?.Invoke(other);
        }

        /// <summary>
        /// Unity callback invoked each frame while another collider stays within this trigger.
        /// Raises <see cref="OnStay"/> only if listeners are attached.
        /// </summary>
        private void OnTriggerStay(Collider other)
        {
            if (OnStay != null)
                OnStay(other);
        }

        /// <summary>
        /// Ensures cleanup when this component or GameObject becomes disabled.
        /// 
        /// Unity does **not** guarantee that <see cref="OnTriggerExit"/> will be called for colliders
        /// when:
        /// <list type="bullet">
        /// <item><description>The collider is disabled.</description></item>
        /// <item><description>The collider is destroyed.</description></item>
        /// <item><description>The trigger is disabled.</description></item>
        /// <item><description>The scene is unloaded.</description></item>
        /// </list>
        /// 
        /// To maintain consistency, this method manually fires <see cref="OnExited"/> for all tracked colliders
        /// and clears the internal set.
        /// </summary>
        private void OnDisable()
        {
            if (_currentColliders.Count == 0)
                return;

            foreach (var col in _currentColliders)
                OnExited?.Invoke(col);

            _currentColliders.Clear();
        }
    }
}
#endif
