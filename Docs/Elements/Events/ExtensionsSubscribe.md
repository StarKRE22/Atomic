# 🧩 Subscribe Extensions

Provide utility methods for **subscribing** [IAction](../Actions/IActions.md) instances to [ISignal](ISignals.md)
sources.

---

## Examples of Usage

When subscribing to a signal, the method returns
a [Subscription](Subscriptions.md) struct.



## 🏹 Methods



#### `Subscribe(Action)`

```csharp
public Subscription Subscribe(Action action)  
```

- **Description:** Subscribes an action to be invoked whenever the signal is triggered.
- **Parameter:** `action` – The delegate to be called when the value changes.
- **Returns:** A [subscription](Subscription.md) struct representing the active subscription.

#### `Unsubscribe(Action)`

```csharp
public void Unsubscribe(Action action)  
```

- **Description:** Removes a previously registered action so it will no longer be invoked when the signal is triggered.
- **Parameter:** `action` – The delegate to remove from the subscription list.


#### `Subscribe(Action<T1, T2>)`

```csharp
public Subscription<T1, T2> Subscribe(Action<T1, T2> action)
```

- **Description:** Subscribes an action to be invoked whenever the signal is triggered.
- **Parameter:** `action` – The delegate to be called when the value changes.
- **Returns:**  The active [subscription](Subscription%602.md) that can be used to dispose of it.

#### `Unsubscribe(Action<T1, T2>)`

```csharp
public void Unsubscribe(Action<T1, T2> action)
```

- **Description:** Removes a previously registered action so it will no longer be invoked when the signal is triggered.
- **Parameters:** `action` – The delegate to remove from the subscription list.

#### `Subscribe(Action<T>)`

```csharp
public Subscription<T> Subscribe(Action<T> action)
```

- **Description:** Subscribes an action to be invoked whenever the signal is triggered.
- **Parameter:** `action` – The delegate to be called when the value changes.
- **Returns:** The active [subscription](Subscription%601.md) that can be used to dispose of it.

#### `Unsubscribe(Action<T>)`

```csharp
public void Unsubscribe(Action<T> action)
```

- **Description:** Removes a previously registered action so it will no longer be invoked when the signal is triggered.
- **Parameters:** `action` – The delegate to remove from the subscription list.

### 3️⃣ Non-generic Subscription <div id="ex-3"></div>

```csharp
```csharp
// Assume we have an instance of fire event
ISignal fireEvent = ...

// Assume we have an AudioSource with fire AudioClip 
AudioSource fireSFX = ...

// Subscribe on the event    
Subscription subscription = fireEvent.Subscribe(fireSFX.Play);

// Later, dispose to unsubscribe
subscription.Dispose();
```

### 4️⃣ Generic Subscription <div id="ex-4"></div>

```csharp
//Assume we have an instance of ISignal
ISignal<T> signal = ...

//Subscribe on the signal
Subscription<T> subscription = signal.Subscribe<T>(lambda);

// Later, dispose to unsubscribe
subscription.Dispose();
```


## 🏹 Methods

#### `Subscribe(ISignal, IAction)`

```csharp
public static Subscription Subscribe(this ISignal it, IAction action)
```

- **Description:** Subscribes a non-generic action to a parameterless signal.
- **Parameters:**
    - `it` – The signal source.
    - `action` – The action to subscribe.
- **Returns:** [Subscription](Subscription.md) – Represents the subscription.

#### `Subscribe<T>(ISignal<T>, IAction<T>)`

```csharp
public static Subscription<T> Subscribe<T>(this ISignal<T> it, IAction<T> action)
```

- **Description:** Subscribes a generic action with one argument to a signal source.
- **Type parameter:** `T` — the emitted value type.
- **Parameters:**
    - `it` – The signal source.
    - `action` – The action to subscribe.
- **Returns:** [Subscription&lt;T&gt;](Subscription%601.md) – Represents the subscription.

#### `Subscribe<T1, T2>(ISignal<T1,T2>, IAction<T1,T2>)`

```csharp
public static Subscription<T1, T2> Subscribe<T1, T2>(
    this ISignal<T1, T2> it, 
    IAction<T1, T2> action
)
```

- **Description:** Subscribes a two-argument action to a signal.
- **Type parameters:**
    - `T1` — the first emitted value
    - `T2` — the second emitted value
- **Parameters:**
    - `it` – The signal source.
    - `action` – The action to subscribe.
- **Returns:** [Subscription<T1, T2>](Subscription%602.md) – Represents the subscription.

#### `Subscribe<T1,T2,T3>(ISignal<T1,T2,T3>, IAction<T1,T2,T3>)`

```csharp
public static Subscription<T1, T2, T3> Subscribe<T1, T2, T3>(
    this ISignal<T1, T2, T3> it,
    IAction<T1, T2, T3> action
)
```

- **Description:** Subscribes a three-argument action to a signal.
- **Type parameters:**
    - `T1` — the first emitted value
    - `T2` — the second emitted value
    - `T3` — the third emitted value
- **Parameters:**
    - `it` – The signal source.
    - `action` – The action to subscribe.
- **Returns:** [Subscription<T1, T2, T3>](Subscription%603.md) – Represents the subscription.

#### `Subscribe<T1,T2,T3,T4>(ISignal<T1,T2,T3,T4>, IAction<T1,T2,T3,T4>)`

```csharp
public static Subscription<T1, T2, T3, T4> Subscribe<T1, T2, T3, T4>(
    this ISignal<T1, T2, T3, T4> it, 
    IAction<T1, T2, T3, T4> action
)
```

- **Description:** Subscribes a four-argument action to a signal.
- **Type parameters:**
    - `T1` — the first emitted value
    - `T2` — the second emitted value
    - `T3` — the third emitted value
    - `T4` — the fourth emitted value

- **Parameters:**
    - `it` – The signal source.
    - `action` – The action to subscribe.
- **Returns:** [Subscription<T1, T2, T3, T4>](Subscription%604.md) – Represents the subscription.

---

## 🗂 Examples of Usage

#### `ISignal` (no parameters)
```csharp
ISignal fireSignal = ...
fireSignal.Subscribe(new InlineAction(() => Debug.Log("OnFire")));
```

#### `ISignal<T>` (with one parameter)
```csharp
ISignal<IEntity> pickUpSignal = ...;
pickUpSignal.Subscribe(new InlineAction(
    pickUp => Debug.Log($"OnPickUp: {pickUp.Name}")));
```

#### `ISignal<T1, T2>` (with two parameters)
```csharp
ISignal<IEntity, int> hitSignal = ...
hitSignal.Subscribe(new InlineAction<IEntity, int>((entity, damage) =>
    Debug.Log($"{entity.Name} received {damage} damage")));
```

#### `ISignal<T1, T2, T3>` (with three parameters)
```csharp
ISignal<IEntity, int, bool> attackSignal = ...;
attackSignal.Subscribe(new InlineAction<IEntity, int, bool>((entity, damage, critical) =>
    Debug.Log($"{entity.Name} dealt {damage} damage (Critical: {critical})")));
```

#### `ISignal<T1, T2, T3, T4>` (with four parameters)
```csharp
ISignal<IEntity, int, bool, Vector3> shootSignal = ...
shootSignal.Subscribe(new InlineAction<IEntity, int, bool, Vector3>((entity, ammo, success, position) =>
    Debug.Log($"{entity.Name} fired {ammo} bullets (Success: {success}) at {position}")));
```
