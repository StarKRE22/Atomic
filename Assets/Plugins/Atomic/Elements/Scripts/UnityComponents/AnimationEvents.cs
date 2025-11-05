#if UNITY_5_3_OR_NEWER
using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Atomic.Elements
{
    /// <summary>
    /// A <see cref="MonoBehaviour"/> that bridges Unity animation events to C# event subscriptions.
    /// </summary>
    /// <remarks>
    /// Attach this component to a <see cref="GameObject"/> with an <see cref="Animator"/> or <see cref="Animation"/> 
    /// to listen for Unity animation events without hard-wiring method names in the inspector.
    /// 
    /// Unity animation events can call the private <see cref="ReceiveEvent(string)"/> method with a string parameter
    /// from the animation timeline. These messages are dispatched both through the strongly-typed subscription
    /// system (<see cref="Subscribe"/>, <see cref="Unsubscribe"/>) and through the generic <see cref="OnEvent"/> event.
    /// 
    /// This allows you to keep animation logic loosely coupled and avoids the need for multiple MonoBehaviour
    /// scripts with hardcoded event handlers.
    /// </remarks>
    [AddComponentMenu("Atomic/Elements/Animation Events")]
    [DisallowMultipleComponent]
    [HelpURL("https://github.com/StarKRE22/Atomic/blob/main/Docs/Elements/Utils/AnimationEvents.md")]
    public sealed class AnimationEvents : MonoBehaviour
    {
        /// <summary>
        /// Invoked whenever an animation event sends a string message to this component.
        /// The string parameter contains the event name or key as configured in the animation.
        /// </summary>
        public event Action<string> OnEvent;

        /// <summary>
        /// Internal mapping between animation event keys and their associated handlers.
        /// </summary>
        private readonly Dictionary<string, Action> _handlers = new(StringComparer.Ordinal);

        /// <summary>
        /// Unity callback for receiving animation events.
        /// This method is intended to be called by Unity's animation system via the Animation Event inspector.
        /// </summary>
        /// <param name="message">
        /// The event key passed from the animation timeline. 
        /// Matches the <paramref name="evt"/> string used in <see cref="Subscribe"/>.
        /// </param>
        [UsedImplicitly]
        public void ReceiveEvent(string message)
        {
            if (_handlers.TryGetValue(message, out Action handler))
                handler?.Invoke();

            this.OnEvent?.Invoke(message);
        }

        /// <summary>
        /// Subscribes an action to a specific animation event key.
        /// Multiple actions can be registered for the same event.
        /// </summary>
        /// <param name="evt">The animation event key to listen for.</param>
        /// <param name="action">The action to invoke when the event is received.</param>
        public void Subscribe(string evt, Action action)
        {
            if (string.IsNullOrEmpty(evt))
                throw new ArgumentNullException(nameof(evt));
            
            if (action == null)
                throw new ArgumentNullException(nameof(action));
            
            if (_handlers.TryGetValue(evt, out Action handler))
                handler += action;
            else
                handler = action;

            _handlers[evt] = handler;
        }

        /// <summary>
        /// Unsubscribes an action from a specific animation event key.
        /// If the action was not registered for this event, nothing happens.
        /// </summary>
        /// <param name="evt">The animation event key to stop listening for.</param>
        /// <param name="action">The action to remove from the event's handler list.</param>
        public void Unsubscribe(string evt, Action action)
        {
            if (string.IsNullOrEmpty(evt) || action == null)
                return;

            if (!_handlers.TryGetValue(evt, out Action handler)) 
                return;
            
            handler -= action;

            if (handler != null)
                _handlers[evt] = handler;
            else
                _handlers.Remove(evt); // remove key if no more handlers
        }
    }
}
#endif