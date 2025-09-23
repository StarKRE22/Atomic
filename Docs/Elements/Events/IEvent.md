# 🧩 IEvent Interfaces

The **IEvent** interfaces define a family of contracts for **reactive events** that can be both **observed** and *
*invoked**. They combine the capabilities of [ISignal](../Signals/ISignal.md) and [IAction](../Actions/IAction.md),
allowing subscription-based reactive tracking and direct action-based invocation.

---

<details>
  <summary>
    <h2>🧩 IEvent</h2>
    <br> Represents a <b>parameterless event</b> that can be subscribed to and invoked.
  </summary>

<br>

```csharp
public interface IEvent : ISignal, IAction
```

---

### 🏹 Methods

#### `Subscribe(Action)`

```csharp
public Subscription Subscribe(Action action)  
```

- **Description:** Subscribes an action to be invoked whenever the signal is triggered.
- **Parameter:** `action` – The delegate to be called when the value changes.
- **Returns:** A [Subscription](../Signals/Subscription.md#subscription) struct representing the active subscription.

#### `Unsubscribe(Action)`

```csharp
public void Unsubscribe(Action action)  
```

- **Description:** Removes a previously registered action so it will no longer be invoked when the signal is triggered.
- **Parameter:** `action` – The delegate to remove from the subscription list.

#### `Invoke()`

```csharp
public void Invoke();
```

- **Description:** Executes the event logic

</details>

---

<details>
  <summary>
    <h2>🧩 IEvent&lt;T1&gt;</h2>
    <br> Represents an event that emits <b>one parameter</b>.
  </summary>

<br>

```csharp
public interface IEvent<T> : ISignal<T>, IAction<T>
```

- **Type parameter:** `T` — The type of the event parameter.

---

### 🏹 Methods

#### `Subscribe(Action<T>)`

```csharp
public Subscription<T> Subscribe(Action<T> action)  
```

- **Description:** Subscribes an action to be invoked whenever the signal is triggered.
- **Parameter:** `action` – The delegate to be called when the value changes.
- **Returns:** A [Subscription&lt;T&gt;](../Signals/Subscription.md#subscriptiont) struct representing the active
  subscription.

#### `Unsubscribe(Action<T>)`

```csharp
public void Unsubscribe(Action<T> action)  
```

- **Description:** Removes a previously registered action so it will no longer be invoked when the signal is triggered.
- **Parameter:** `action` – The delegate to remove from the subscription list.

#### `Invoke(T)`

```csharp
public void Invoke(T arg);
```

- **Description:** Executes the event with the specified argument
- **Parameter:** `arg` — the input parameter

</details>

---

<details>
  <summary>
    <h2>🧩 IEvent&lt;T1, T2&gt;</h2>
    <br> Represents an event that emits <b>two parameters</b>.
  </summary>

<br>

```csharp
public interface IEvent<T1, T2> : ISignal<T1, T2>, IAction<T1, T2>
```

- **Type parameters:**
    - `T1` — The first argument
    - `T2` — The second argument

---

### 🏹 Methods

#### `Subscribe(Action<T1, T2>)`

```csharp
public Subscription<T1, T2> Subscribe(Action<T1, T2> action)  
```

- **Description:** Subscribes an action to be invoked whenever the signal is triggered.
- **Parameter:** `action` – The delegate to be called when the value changes.
- **Returns:** A [Subscription<T1, T2>](../Signals/Subscription.md#subscriptiont1-t2) struct representing the active
  subscription.

#### `Unsubscribe(Action<T1, T2>)`

```csharp
public void Unsubscribe(Action<T1, T2> action)  
```

- **Description:** Removes a previously registered action so it will no longer be invoked when the signal is triggered.
- **Parameter:** `action` – The delegate to remove from the subscription list.

#### `Invoke(T1, T2)`

```csharp
public void Invoke(T1 arg1, T2 arg2);
```

- **Description:** Executes the action with the specified arguments
- **Parameters:**
    - `arg1` — the first argument
    - `arg2` — the second argument

</details>

---

<details>
  <summary>
    <h2>🧩 IEvent&lt;T1, T2, T3&gt;</h2>
    <br> Represents an event that emits <b>three parameters</b>.
  </summary>

<br>

```csharp
public interface IEvent<T1, T2, T3> : ISignal<T1, T2, T3>, IAction<T1, T2, T3>
```

- **Type parameters:**
    - `T1` — The first argument
    - `T2` — The second argument
    - `T3` — The third argument

---

### 🏹 Methods

#### `Subscribe(Action<T1, T2, T3>)`

```csharp
public Subscription<T1, T2, T3> Subscribe(Action<T1, T2, T3> action)  
```

- **Description:** Subscribes an action to be invoked whenever the signal is triggered.
- **Parameter:** `action` – The delegate to be called when the value changes.
- **Returns:** A [Subscription<T1, T2, T3>](../Signals/Subscription.md#subscriptiont1-t2-t3) struct representing the
  active subscription.

#### Unsubscribe(Action<T1, T2, T3>)

```csharp
public void Unsubscribe(Action<T1, T2, T3> action)  
```

- **Description:** Removes a previously registered action so it will no longer be invoked when the signal is triggered.
- **Parameter:** `action` – The delegate to remove from the subscription list.

#### `Invoke(T1, T2, T3)`

```csharp
public void Invoke(T1 arg1, T2 arg2, T3 arg3);
```

- **Description:** Executes the event with the specified arguments
- **Parameters:**
    - `arg1` — the first argument
    - `arg2` — the second argument
    - `arg3` — the third argument

</details>

---

<details>
  <summary>
    <h2>🧩 IEvent&lt;T1, T2, T3, T4&gt;</h2>
    <br> Represents an event that emits <b>four parameters</b>.
  </summary>

<br>

```csharp
public interface IEvent<T1, T2, T3, T4> : ISignal<T1, T2, T3, T4>, IAction<T1, T2, T3, T4>
```
- **Type parameters:**
    - `T1` — The first argument
    - `T2` — The second argument
    - `T3` — The third argument
    - `T4` — The fourth argument

### 🏹 Methods

#### `Subscribe(Action<T1, T2, T3, T4>)`

```csharp
public Subscription<T1, T2, T3, T4> Subscribe(Action<T1, T2, T3, T4> action)  
```

- **Description:** Subscribes an action to be invoked whenever the signal is triggered.
- **Parameter:** `action` – The delegate to be called when the value changes.
- **Returns:** A [Subscription<T1, T2, T3, T4>](../Signals/Subscription.md#subscriptiont1-t2-t3-t4) struct representing
  the active subscription.

#### `Unsubscribe(Action<T1, T2, T3, T4>)`

```csharp
public void Unsubscribe(Action<T1, T2, T3, T4> action)  
```

- **Description:** Removes a previously registered action so it will no longer be invoked when the signal is triggered.
- **Parameter:** `action` – The delegate to remove from the subscription list.

#### `Invoke(T1, T2, T3, T4)`

```csharp
public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
```

- **Description:** Executes the event with the specified arguments
- **Parameters:**
    - `arg1` — the first argument
    - `arg2` — the second argument
    - `arg3` — the third argument

</details>

---

## 🗂 Example of Usage

Below is an example of how to use `IEvent` for triggering a **sound effect** together with the `Atomic.Entities`
framework.

#### 1. Create an entity with a `FireEvent` property

```csharp
var entity = new Entity("Character");
entity.AddValue("FireEvent", new BaseEvent()); //IEvent
entity.AddValue("AudioSource", audioSource);
```

#### 2. Use `FireEvent` through the `ISignal` interface

```csharp
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

#### 3. Invoke `FireEvent` through the `IAction` interface

```csharp
IAction fireEvent = entity.GetValue<IAction>("FireEvent");
fireEvent.Invoke();
```