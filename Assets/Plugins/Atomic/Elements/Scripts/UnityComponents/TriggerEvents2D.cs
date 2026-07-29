#if UNITY_5_3_OR_NEWER
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Atomic.Elements
{
    /// <summary>
    /// Provides safe and consistent 2D trigger notifications by wrapping Unity's
    /// <see cref="OnTriggerEnter2D"/>, <see cref="OnTriggerExit2D"/> and
    /// <see cref="OnTriggerStay2D"/> callbacks into managed C# events.
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
    [AddComponentMenu("Atomic/Elements/Trigger Events 2D")]
    [DisallowMultipleComponent]
    [HelpURL("https://github.com/StarKRE22/Atomic/blob/main/Docs/Elements/Utils/TriggerEvents2D.md")]
    public sealed class TriggerEvents2D : MonoBehaviour
    {
        /// <summary>
        /// Occurs when a 2D collider enters the trigger area.
        /// Fired only once per collider until it exits.
        /// </summary>
        public event Action<Collider2D> OnEntered;

        /// <summary>
        /// Occurs when a 2D collider exits the trigger area.
        /// Fired only if the collider was previously registered as entered.
        /// </summary>
        public event Action<Collider2D> OnExited;

        /// <summary>
        /// Occurs every frame while a 2D collider remains inside the trigger area.
        /// This event is invoked only if listeners are subscribed.
        /// </summary>
        public event Action<Collider2D> OnStay;

        /// <summary>
        /// Gets a read-only collection of 2D colliders currently inside the trigger area.
        /// The collection is automatically cleared when the component is disabled
        /// or when the colliders are destroyed.
        /// </summary>
        public IReadOnlyCollection<Collider2D> CurrentColliders => _currentColliders;

        private readonly HashSet<Collider2D> _currentColliders = new();

        /// <summary>
        /// Unity callback invoked when another 2D collider enters the trigger.
        /// Registers the collider and raises the <see cref="OnEntered"/> event
        /// only if the collider was not already tracked.
        /// </summary>
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_currentColliders.Add(other))
                OnEntered?.Invoke(other);
        }

        /// <summary>
        /// Unity callback invoked when another 2D collider exits the trigger.
        /// Removes the collider and raises the <see cref="OnExited"/> event
        /// only if the collider was previously tracked.
        /// </summary>
        private void OnTriggerExit2D(Collider2D other)
        {
            if (_currentColliders.Remove(other))
                OnExited?.Invoke(other);
        }

        /// <summary>
        /// Unity callback invoked each frame while another 2D collider stays within this trigger.
        /// Raises <see cref="OnStay"/> only if listeners are attached.
        /// </summary>
        private void OnTriggerStay2D(Collider2D other)
        {
            if (OnStay != null)
                OnStay(other);
        }

        /// <summary>
        /// Ensures cleanup when this component or GameObject becomes disabled.
        /// 
        /// Unity does not guarantee that <see cref="OnTriggerExit2D"/> will be called for colliders
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

            foreach (Collider2D col in _currentColliders)
                OnExited?.Invoke(col);

            _currentColliders.Clear();
        }
    }
}
#endif