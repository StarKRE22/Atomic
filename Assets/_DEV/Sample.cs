using System;
using Atomic.Elements;
using UnityEngine;

namespace _DEV
{
    public class Sample : MonoBehaviour
    {
        TriggerEvents triggerEvents;

        private void Start()
        {
            ISignal<Collider> onTriggerEnter = new InlineSignal<Collider>(
                subscribe: action => triggerEvents.OnEntered += action,
                unsubscribe: action => triggerEvents.OnEntered -= action
            );
            Subscription<Collider> subscription =
                onTriggerEnter.Subscribe(collider => Debug.Log($"On Trigger Entered: {collider.name}"));
            
            subscription.Dispose();
        }
    }
}