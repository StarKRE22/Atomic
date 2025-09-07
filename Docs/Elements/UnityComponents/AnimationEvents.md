# 🧩️ AnimationEvents

`AnimationEvents` is a Unity `MonoBehaviour` that bridges **Unity animation events** to C# event subscriptions.  
It allows you to listen to animation events without hardcoding method names in the inspector.

Attach this component to a `GameObject` with an `Animator` or `Animation` component to use it.

---

## Description

- Animation events in Unity can call the **private** `ReceiveEvent(string)` method from the animation timeline.
- Events are dispatched:
    - Through the **generic event** `OnEvent` (string-based)
    - Through **strongly-typed subscriptions** using `Subscribe` and `Unsubscribe`.
- This design keeps animation logic **loosely coupled** and avoids multiple MonoBehaviour scripts with hardcoded handlers.

---

## Events

- `event Action<string> OnEvent` – invoked whenever any animation event sends a string message to this component.  
  The string contains the event name or key from the animation timeline.

---

## Methods

### ReceiveEvent(string message)

- **Private method**, intended to be called by Unity animation events.
- Invokes all handlers subscribed to `message` and raises `OnEvent`.
- Parameters:
    - `message` – the string key passed from the animation timeline.

### Subscribe(string evt, Action action)

- Subscribes a C# `Action` to a specific animation event key.
- Multiple actions can be registered for the same event.
- Parameters:
    - `evt` – the animation event key to listen for.
    - `action` – the action to invoke when the event occurs.

### Unsubscribe(string evt, Action action)

- Removes a previously subscribed action from an animation event key.
- Safe to call even if the action was not registered.
- Parameters:
    - `evt` – the animation event key.
    - `action` – the action to remove.

---

## Example Usage

```csharp
public class Character : MonoBehaviour
{
    public AnimationEvents animEvents;

    private void Awake()
    {
        animEvents.Subscribe("JumpStart", OnJumpStart);
        animEvents.Subscribe("JumpEnd", OnJumpEnd);
        animEvents.OnEvent += OnAnyAnimationEvent;
    }

    private void OnDestroy()
    {
        animEvents.Unsubscribe("JumpStart", OnJumpStart);
        animEvents.Unsubscribe("JumpEnd", OnJumpEnd);
        animEvents.OnEvent -= OnAnyAnimationEvent;
    }

    private void OnJumpStart() => Debug.Log("Jump started!");
    private void OnJumpEnd() => Debug.Log("Jump ended!");
    private void OnAnyAnimationEvent(string evt) => Debug.Log($"Event triggered: {evt}");
}
```
---
### Behavior
- Handles animation events without requiring hardcoded method names in the Unity inspector.
- Supports multiple subscribers per event key.
- Provides both **generic** (`OnEvent`) and **strongly-typed** (`Subscribe`) notifications.
- Keeps animation logic **decoupled** and flexible for runtime changes.