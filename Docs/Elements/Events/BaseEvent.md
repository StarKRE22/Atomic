# 🧩 BaseEvent Classes

The **BaseEvent** classes provide **parameterless and generic reactive events** that can be **subscribed to, invoked, and disposed**. They implement the corresponding [IEvent](IEvent.md) interfaces and allow both reactive tracking and action-based invocation.

---

## 🧩 BaseEvent
```csharp
public class BaseEvent : IEvent, IDisposable
```
- **Description:** Represents a **parameterless event** that can be subscribed to and invoked.

### Events

#### `OnEvent`
```csharp
event Action OnEvent
```
- **Description:** Triggered whenever the event raises.
---

### Methods

#### `Subscribe(Action)`
```csharp
Subscription Subscribe(Action action)  
```
- **Description:** Subscribes an action to be invoked whenever the event is triggered.
- **Parameter:** `action` – The delegate to be called when the value changes.
- **Returns:** A [Subscription](../Signals/Subscription.md#subscription) struct representing the active subscription.

#### `Unsubscribe(Action)`
```csharp
void Unsubscribe(Action action)  
```
- **Description:** Removes a previously registered action so it will no longer be invoked when the event is triggered.
- **Parameters:** `action` – The delegate to remove from the subscription list.

#### `Invoke()`
```csharp
void Invoke();
```
- **Description:** Executes the event logic

#### `Dispose()`
```csharp
public void Dispose()
```
- **Description:** Clears all subscriptions for this event.
---

## 🧩 BaseEvent&lt;T&gt;
```csharp
public class BaseEvent<T> : IEvent<T>, IDisposable
```
- **Description:** Represents an event that emits **one parameter**.
- **Type parameter:** `T` — The type of the event argument.

### Methods

#### `Subscribe(Action<T>)`
```csharp
public Subscription<T> Subscribe(Action<T> action)
```
- **Description:** Subscribes a handler to the event.
- **Parameter:** `action` – The delegate to invoke when the event triggers.
- **Returns:** A [Subscription<T>](../Signals/Subscription.md#subscriptiont) representing the active subscription.

#### `Unsubscribe(Action<T>)`
```csharp
public void Unsubscribe(Action<T> action)
```
- **Description:** Removes a previously registered handler from the event.
- **Parameters:** `action` – The delegate to remove from the subscription list.

#### `Invoke(T)`
```csharp
public void Invoke(T arg)
```
- **Description:** Triggers the event with the specified argument.
- **Parameter:** `arg` — The input parameter.

#### `Dispose()`
```csharp
public void Dispose()
```
- **Description:** Clears all subscriptions for this event.
---

## 🧩 BaseEvent<T1, T2>
```csharp
public class BaseEvent<T1, T2> : IEvent<T1, T2>, IDisposable
```
- **Description:** Represents an event that emits **two parameters**.
- **Type parameters:**
    - `T1` — The first argument
    - `T2` — The second argument

### Methods

#### `Subscribe(Action<T1, T2>)`
```csharp
public Subscription<T1, T2> Subscribe(Action<T1, T2> action)
```
- **Description:** Subscribes a handler to the event.
- **Parameter:** `action` – The delegate to invoke when the event triggers.
- **Returns:** A [Subscription<T1, T2>](../Signals/Subscription.md#subscriptiont1-t2) representing the active subscription.

#### `Unsubscribe(Action<T1, T2>)`
```csharp
public void Unsubscribe(Action<T1, T2> action)
```
- **Description:** Removes a previously registered handler from the event.
- **Parameters:** `action` – The delegate to remove from the subscription list.

#### `Invoke(T1, T2)`
```csharp
public void Invoke(T1 arg1, T2 arg2)
```
- **Description:** Triggers the event with the specified arguments.
- **Parameters:**
    - `arg1` — The first argument
    - `arg2` — The second argument

#### `Dispose()`
```csharp
public void Dispose()
```
- **Description:** Clears all subscriptions for this event.
---

## 🧩 BaseEvent<T1, T2, T3>
```csharp
public class BaseEvent<T1, T2, T3> : IEvent<T1, T2, T3>, IDisposable
```
- **Description:** Represents an event that emits **three parameters**.
- **Type parameters:**
    - `T1` — The first argument
    - `T2` — The second argument
    - `T3` — The third argument

### Methods

#### `Subscribe(Action<T1, T2, T3>)`
```csharp
public Subscription<T1, T2, T3> Subscribe(Action<T1, T2, T3> action)
```
- **Description:** Subscribes a handler to the event.
- **Parameter:** `action` – The delegate to invoke when the event triggers.
- **Returns:** A [Subscription<T1, T2, T3>](../Signals/Subscription.md#subscriptiont1-t2-t3) representing the active subscription.

#### `Unsubscribe(Action<T1, T2, T3>)`
```csharp
public void Unsubscribe(Action<T1, T2, T3> action)
```
- **Description:** Removes a previously registered handler from the event.
- **Parameters:** `action` – The delegate to remove from the subscription list.

#### `Invoke(T1, T2, T3)`
```csharp
public void Invoke(T1 arg1, T2 arg2, T3 arg3)
```
- **Description:** Triggers the event with the specified arguments.
- **Parameters:**
    - `arg1` — The first argument
    - `arg2` — The second argument
    - `arg3` — The third argument

#### `Dispose()`
```csharp
public void Dispose()
```
- **Description:** Clears all subscriptions for this event.

---

## 🧩 BaseEvent<T1, T2, T3, T4>
```csharp
public class BaseEvent<T1, T2, T3, T4> : IEvent<T1, T2, T3, T4>, IDisposable
```
- **Description:** Represents an event that emits **four parameters**.
- **Type parameters:**
    - `T1` — The first argument
    - `T2` — The second argument
    - `T3` — The third argument
    - `T4` — The fourth argument

### Methods

#### `Subscribe(Action<T1, T2, T3, T4>)`
```csharp
public Subscription<T1, T2, T3, T4> Subscribe(Action<T1, T2, T3, T4> action)
```
- **Description:** Subscribes a handler to the event.
- **Parameter:** `action` – The delegate to invoke when the event triggers.
- **Returns:** A [Subscription<T1, T2, T3, T4>](../Signals/Subscription.md#subscriptiont1-t2-t3-t4) representing the active subscription.

#### `Unsubscribe(Action<T1, T2, T3, T4>)`
```csharp
public void Unsubscribe(Action<T1, T2, T3, T4> action)
```
- **Description:** Removes a previously registered handler from the event.
- **Parameters:** `action` – The delegate to remove from the subscription list.

#### `Invoke(T1, T2, T3, T4)`
```csharp
public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
```
- **Description:** Triggers the event with the specified arguments.
- **Parameters:**
    - `arg1` — The first argument
    - `arg2` — The second argument
    - `arg3` — The third argument
    - `arg4` — The fourth argument

#### `Dispose()`
```csharp
public void Dispose()
```
- **Description:** Clears all subscriptions for this event.
---

## 🗂 Example of Usage
Below is an example of how to use `BaseEvent` for triggering a **sound effect** together with the `Atomic.Entities` framework.

```csharp
// Create an entity with a "FireEvent" property
var entity = new Entity("Character");
entity.AddValue("FireEvent", new BaseEvent());
entity.AddValue("AudioSource", audioSource);
```

```csharp
// Use "FireEvent" through the ISignal interface
[Serializable]
public class FireSFXBehaviour : IEntityInit, IEntityDispose
{
    [Serializable]
    private AudioClip _fireSFX;

    private AudioSource _audioSource;
    private ISignal _fireSignal; //IEvent
    
    public void Init(IEntity entity)
    {
        _audioSource = entity.GetValue<AudioSource>("AudioSource");
        _fireSignal = entity.GetValue<ISignal>("FireEvent");
        _fireSignal.Subscribe(this.OnFire);
    }
    
    public void Dispose(IEntity entity)
    {
        _fireSignal.Unsubscribe(this.OnFire);
    }
    
    private void OnFire()
    {
        _audioSource.PlayOneShot(_fireSFX);
    }
}
```

```csharp
//Invoke "FireEvent"
IAction fireEvent = entity.GetValue<IAction>("FireEvent");
fireEvent.Invoke();
```