# 🧩 Signal Interfaces

The **ISignal** interfaces define a family of contracts for **reactive event sources**. They provide a lightweight abstraction for subscribing to notifications and reacting to events, optionally with arguments.

---

## 🧩 ISignal

```csharp
public interface ISignal
```
- **Description:** Represents a signal that can notify subscribers of events **without passing any data**.

### Methods

#### `Subscribe(Action)`
```csharp
Subscription Subscribe(Action action)
```
- **Description:** Subscribes an action to be invoked whenever the signal is triggered.
- **Parameter:** `action` – The delegate to be called when the value changes.
- **Returns:** A [Subscription](../Signals/Subscription.md#subscription) struct representing the active subscription.

#### `Unsubscribe(Action)`
```csharp
void Unsubscribe(Action action)
```
- **Description:** Removes a previously registered action so it will no longer be invoked when the signal is triggered.
- **Parameters:** `action` – The delegate to remove from the subscription list.
----

## 🧩 ISignal&lt;T&gt;

```csharp
public interface ISignal<T>
```
- **Description:** Represents a signal that notifies subscribers with a **single value**.
- **Type parameter:** `T` — the emitted value type.

### Methods

#### `Subscribe(Action<T>)`
```csharp
Subscription<T> Subscribe(Action<T> action)
```
- **Description:** Subscribes an action to be invoked whenever the signal is triggered.
- **Parameter:** `action` – The delegate to be called when the value changes.
- **Returns:** A [Subscription&lt;T&gt;](../Signals/Subscription.md#subscriptiont) struct representing the active subscription.

#### `Unsubscribe(Action<T>)`
```csharp
void Unsubscribe(Action<T> action)
```
- **Description:** Removes a previously registered action so it will no longer be invoked when the signal is triggered.
- **Parameters:** `action` – The delegate to remove from the subscription list.
---

## 🧩 ISignal&lt;T1, T2&gt;
```csharp
public interface ISignal<T1, T2>
```
- **Description:** Represents a signal that notifies subscribers with **two values**.
- **Type parameters:**
  - `T1` — the first emitted value
  - `T2` — the second emitted value

### Methods

#### `Subscribe(Action<T1, T2>)`
```csharp
Subscription<T1, T2> Subscribe(Action<T1, T2> action)
```
- **Description:** Subscribes an action to be invoked whenever the signal is triggered.
- **Parameter:** `action` – The delegate to be called when the value changes.
- **Returns:** A [Subscription<T1, T2>](../Signals/Subscription.md#subscriptiont1-t2) struct representing the active subscription.

#### `Unsubscribe(Action<T1, T2>)`
```csharp
void Unsubscribe(Action<T1, T2> action)
```
- **Description:** Removes a previously registered action so it will no longer be invoked when the signal is triggered.
- **Parameters:** `action` – The delegate to remove from the subscription list.
---

## 🧩 ISignal&lt;T1, T2, T3&gt;
```csharp
public interface ISignal<T1, T2, T3>
```
- **Description:** Represents a signal that notifies subscribers with **three values**.
- **Type parameters:**
  - `T1` — the first emitted value
  - `T2` — the second emitted value
  - `T3` — the third emitted value

### Methods

#### `Subscribe(Action<T1, T2, T3>)`
```csharp
Subscription<T1, T2, T3> Subscribe(Action<T1, T2, T3> action)
```
- **Description:** Subscribes an action to be invoked whenever the signal is triggered.
- **Parameter:** `action` – The delegate to be called when the value changes.
- **Returns:** A [Subscription<T1, T2, T3>](../Signals/Subscription.md#subscriptiont1-t2-t3) struct representing the active subscription.

#### `Unsubscribe(Action<T1, T2, T3>)`
```csharp
void Unsubscribe(Action<T1, T2, T3> action)
```
- **Description:** Removes a previously registered action so it will no longer be invoked when the signal is triggered.
- **Parameters:** `action` – The delegate to remove from the subscription list.
---

## 🧩 ISignal&lt;T1, T2, T3, T4&gt;
```csharp
public interface ISignal<T1, T2, T3, T4>
```
- **Description:** Represents a signal that notifies subscribers with **four values**.
- **Type parameters:**
  - `T1` — the first emitted value
  - `T2` — the second emitted value
  - `T3` — the third emitted value
  - `T4` — the fourth emitted value

### Methods

#### `Subscribe(Action<T1, T2, T3, T4>)`
```csharp
Subscription<T1, T2, T3, T4> Subscribe(Action<T1, T2, T3, T4> action)
```
- **Description:** Subscribes an action to be invoked whenever the signal is triggered.
- **Parameter:** `action` – The delegate to be called when the value changes.
- **Returns:** A [Subscription<T1, T2, T3, T4>](../Signals/Subscription.md#subscriptiont1-t2-t3-t4) struct representing the active subscription.

#### `Unsubscribe(Action<T1, T2, T3, T4>)`
```csharp
void Unsubscribe(Action<T1, T2, T3, T4> action)
```
- **Description:** Removes a previously registered action so it will no longer be invoked when the signal is triggered.
- **Parameters:** `action` – The delegate to remove from the subscription list.
---

## 🗂 Example of Usage
Below is an example of how to use `ISignal` for triggering a **sound effect** together with the `Atomic.Entities` framework.

```csharp
// Create an entity with a "FireEvent" signal property
var entity = new Entity("Character");
entity.AddValue("FireEvent", new BaseEvent()); //ISignal
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
    private ISignal _fireSignal;
    
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