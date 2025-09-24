# 🧩 ISignal Interfaces

Defines a family of contracts for **reactive event sources**. It provides a lightweight abstraction for subscribing to
notifications and reacting to events, optionally with arguments. When subscribing to a signal, the method returns
a [subscription](../Signals/Subscription.md) struct.

---

<details>
  <summary>
    <h2>🧩 ISignal</h2>
    <br> Represents a signal that can notify subscribers of events <b>without passing any data</b>.
  </summary>

<br>

```csharp
public interface ISignal
```

---

### 🏹 Methods

#### `Subscribe(Action)`

```csharp
public Subscription Subscribe(Action action)
```

- **Description:** Subscribes an action to be invoked whenever the signal is triggered.
- **Parameter:** `action` – The delegate to be called when the value changes.
- **Returns:** The active [subscription](../Signals/Subscription.md#subscription) that can be used to dispose of it.

#### `Unsubscribe(Action)`

```csharp
public void Unsubscribe(Action action)
```

- **Description:** Removes a previously registered action so it will no longer be invoked when the signal is triggered.
- **Parameters:** `action` – The delegate to remove from the subscription list.

</details>

---

<details>
  <summary>
    <h2>🧩 ISignal&lt;T&gt;</h2>
    <br> Represents a signal that notifies subscribers with a <b>single value</b>.
  </summary>

```csharp
public interface ISignal<T>
```

- **Type parameter:** `T` — the emitted value type.

---

### 🏹 Methods

#### `Subscribe(Action<T>)`

```csharp
public Subscription<T> Subscribe(Action<T> action)
```

- **Description:** Subscribes an action to be invoked whenever the signal is triggered.
- **Parameter:** `action` – The delegate to be called when the value changes.
- **Returns:** The active [subscription](../Signals/Subscription.md#subscriptiont) that can be used to dispose of it.

#### `Unsubscribe(Action<T>)`

```csharp
public void Unsubscribe(Action<T> action)
```

- **Description:** Removes a previously registered action so it will no longer be invoked when the signal is triggered.
- **Parameters:** `action` – The delegate to remove from the subscription list.

</details>

---

<details>
  <summary>
    <h2>🧩 ISignal&lt;T1, T2&gt;</h2>
    <br> Represents a signal that notifies subscribers with <b>two values</b>.
  </summary>

```csharp
public interface ISignal<T1, T2>
```

- **Type parameters:**
    - `T1` — the first emitted value
    - `T2` — the second emitted value

---

### 🏹 Methods

#### `Subscribe(Action<T1, T2>)`

```csharp
public Subscription<T1, T2> Subscribe(Action<T1, T2> action)
```

- **Description:** Subscribes an action to be invoked whenever the signal is triggered.
- **Parameter:** `action` – The delegate to be called when the value changes.
- **Returns:**  The active [subscription](../Signals/Subscription.md#subscriptiont1-t2) that can be used to dispose of
  it.

#### `Unsubscribe(Action<T1, T2>)`

```csharp
public void Unsubscribe(Action<T1, T2> action)
```

- **Description:** Removes a previously registered action so it will no longer be invoked when the signal is triggered.
- **Parameters:** `action` – The delegate to remove from the subscription list.

</details>

---

<details>
  <summary>
    <h2>🧩 ISignal&lt;T1, T2, T3&gt;</h2>
    <br> Represents a signal that notifies subscribers with <b>three values</b>.
  </summary>

```csharp
public interface ISignal<T1, T2, T3>
```

- **Type parameters:**
    - `T1` — the first emitted value
    - `T2` — the second emitted value
    - `T3` — the third emitted value

---

### 🏹 Methods

#### `Subscribe(Action<T1, T2, T3>)`

```csharp
public Subscription<T1, T2, T3> Subscribe(Action<T1, T2, T3> action)
```

- **Description:** Subscribes an action to be invoked whenever the signal is triggered.
- **Parameter:** `action` – The delegate to be called when the value changes.
- **Returns:** The active [subscription](../Signals/Subscription.md#subscriptiont1-t2-t3) that can be used to dispose of
  it.

#### `Unsubscribe(Action<T1, T2, T3>)`

```csharp
public void Unsubscribe(Action<T1, T2, T3> action)
```

- **Description:** Removes a previously registered action so it will no longer be invoked when the signal is triggered.
- **Parameters:** `action` – The delegate to remove from the subscription list.

</details>

---

<details>
  <summary>
    <h2>🧩 ISignal&lt;T1, T2, T3, T4&gt;</h2>
    <br> Represents a signal that notifies subscribers with <b>four values</b>.
  </summary>

```csharp
public interface ISignal<T1, T2, T3, T4>
```

- **Description:** Represents a signal that notifies subscribers with **four values**.
- **Type parameters:**
    - `T1` — the first emitted value
    - `T2` — the second emitted value
    - `T3` — the third emitted value
    - `T4` — the fourth emitted value

---

### 🏹 Methods

#### `Subscribe(Action<T1, T2, T3, T4>)`

```csharp
public Subscription<T1, T2, T3, T4> Subscribe(Action<T1, T2, T3, T4> action)
```

- **Description:** Subscribes an action to be invoked whenever the signal is triggered.
- **Parameter:** `action` – The delegate to be called when the value changes.
- **Returns:** The active [subscription](../Signals/Subscription.md#subscriptiont1-t2-t3-t4) that can be used to dispose
  of it.

#### `Unsubscribe(Action<T1, T2, T3, T4>)`

```csharp
public void Unsubscribe(Action<T1, T2, T3, T4> action)
```

- **Description:** Removes a previously registered action so it will no longer be invoked when the signal is triggered.
- **Parameters:** `action` – The delegate to remove from the subscription list.

</details>

---

## 🗂 Example of Usage

Below is an example of how to use `ISignal` for triggering a **sound effect** together with the `Atomic.Entities`
framework.

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