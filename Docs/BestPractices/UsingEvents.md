# 📌 Using Events with Entities

This section demonstrates how to use [BaseEvent](../Elements/Events/BaseEvent.md) to **trigger a sound effect** in
combination with the [Atomic.Entities](../Entities/Manual.md) framework.

---


## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
  - [Create an Entity with a FireEvent](#1-create-an-entity-with-a-fireevent)
  - [Use FireEvent via the ISignal Interface](#2-use-fireevent-via-the-isignal-interface)
  - [Trigger the Event via the IAction Interface](#3-trigger-the-event-via-the-iaction-interface)
  - [Result](#4-result)
- [Conclusion](#-conclusion)
- [Benefits](#-benefits)

---

## 🗂 Example of Usage

### 1. Create an Entity with a FireEvent

Define an entity and add a `BaseEvent` value that will represent your event signal.

```csharp
var entity = new Entity("Character");
entity.AddValue("FireEvent", new BaseEvent());
```

---

### 2. Use FireEvent via the ISignal Interface

Implement a component that subscribes to the event and reacts when it’s fired — for example, by playing a sound effect.

```csharp
[Serializable]
public class FireSFXBehaviour : IEntityInit, IEntityDispose
{
    [SerializeField] private AudioClip _fireSFX;
    [SerializeField] private AudioSource _audioSource;

    private ISignal _fireSignal;

    public void Init(IEntity entity)
    {
        _fireSignal = entity.GetValue<ISignal>("FireEvent");
        _fireSignal.Subscribe(OnFire);
    }

    public void Dispose(IEntity entity)
    {
        _fireSignal.Unsubscribe(OnFire);
    }

    private void OnFire()
    {
        _audioSource.PlayOneShot(_fireSFX);
    }
}
```

> [!TIP]
> `ISignal` provides a clean way to listen to entity events without coupling logic directly to event instances.

---

### 3. Trigger the Event via the IAction Interface

Finally, invoke the event from anywhere in your code using the `IAction` interface.

```csharp
IAction fireEvent = entity.GetValue<IAction>("FireEvent");
fireEvent.Invoke();
```

---

### 4. Result

When `fireEvent.Invoke()` is called, the `FireSFXBehaviour` component automatically plays the assigned sound clip —
providing an elegant, event-driven architecture for your entities.

---

## 🏁 Conclusion

- [BaseEvent](../Elements/Events/BaseEvents.md) enables **clean, decoupled event-driven communication** between entity components.
- The [ISignal](../Elements/Events/ISignals.md) interface allows components to **listen and react** to specific events in a controlled, modular way.
- The [IAction](../Elements/Actions/IActions.md) interface provides a **unified and type-safe mechanism** for invoking those events from anywhere in the system.
- This combination creates a **powerful and reusable event architecture** inside the [Atomic.Entities](../Entities/Manual.md) framework.
- Ideal for integrating **gameplay reactions** such as sound effects, animations, or visual effects without tight coupling between systems.

---

## ✅ Benefits

- Promotes **decoupled architecture** — logic is separated into independent, event-driven components.
- Improves **reusability** — behaviors can easily be attached to different entities without refactoring.
- Increases **maintainability** — event flow is explicit and centralized through `BaseEvent`.
- Supports **scalability** — new reactions or actions can be added without modifying existing code.
- Enhances **clarity and debugging** — makes event connections easier to trace and understand.  
