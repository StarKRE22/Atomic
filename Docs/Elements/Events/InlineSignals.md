# 🧩 InlineSignals

Provides a wrapper for reactive event source. Implements the corresponding [ISignal](ISignals.md) interface and allow
entities to **subscribe / unsubscribe** from events, optionally with parameters. When subscribing to a signal, the
method returns a [Subscription](Subscriptions.md) struct.

There are several inline signals, depending on the number of arguments they take:

- [InlineSignal](InlineSignal.md) — Signal without parameters.
- [InlineSignal&lt;T&gt;](InlineSignal%601.md) — Signal that takes one argument.
- [InlineSignal&lt;T1, T2&gt;](InlineSignal%602.md) — Signal that takes two arguments.
- [InlineSignal&lt;T1, T2, T3&gt;](InlineSignal%603.md) — Signal that takes two arguments.
- [InlineSignal&lt;T1, T2, T3, T4&gt;](InlineSignal%604.md) — Signal that takes two arguments.

---


## 🗂 Example of Usage

The following example demonstrates how to wrap the `OnEntered` event from
the [TriggerEvents](../UnityComponents/TriggerEvents.md) class into a `Signal<Collider>`:

```csharp
//Assume we have the "TriggerEvents" component on a gameObject
TriggerEvents triggerEvents = gameObject.GetComponent<TriggerEvents>();

// Wrap the Unity event into an InlineSignal
ISignal<Collider> onTriggerEnter = new InlineSignal<Collider>(
    subscribe: action => triggerEvents.OnEntered += action,
    unsubscribe: action => triggerEvents.OnEntered -= action
);

// Subscribe to the signal
Subscription<Collider> subscription = onTriggerEnter.Subscribe(
    collider => Debug.Log($"On Trigger Entered: {collider.name}")
);

// Later, dispose to unsubscribe
subscription.Dispose();
```