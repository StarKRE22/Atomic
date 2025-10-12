# 🧩 ISignal Unsubscribe Extensions

Provide utility methods for **unsubscribing** [IAction](../Actions/IActions.md) instances from [ISignal](ISignals.md)
sources.

---

## 🏹 Methods


#### `Unsubscribe(Action)`

```csharp
public void Unsubscribe(Action action)
```

- **Description:** Removes a previously registered action so it will no longer be invoked when the signal is triggered.
- **Parameters:** `action` – The delegate to remove from the subscription list.


#### `Unsubscribe(Action)`

```csharp
public void Unsubscribe(Action action)  
```

- **Description:** Removes a previously registered action so it will no longer be invoked when the signal is triggered.
- **Parameter:** `action` – The delegate to remove from the subscription list.


#### `Unsubscribe(Action<T>)`

```csharp
public void Unsubscribe(Action<T> action)
```

- **Description:** Removes a previously registered action so it will no longer be invoked when the signal is triggered.
- **Parameters:** `action` – The delegate to remove from the subscription list.


#### `Unsubscribe(Action<T1, T2>)`

```csharp
public void Unsubscribe(Action<T1, T2> action)
```

- **Description:** Removes a previously registered action so it will no longer be invoked when the signal is triggered.
- **Parameters:** `action` – The delegate to remove from the subscription list.


#### `Unsubscribe(Action<T1, T2, T3>)`

```csharp
public void Unsubscribe(Action<T1, T2, T3> action)
```

- **Description:** Removes a previously registered action so it will no longer be invoked when the signal is triggered.
- **Parameters:** `action` – The delegate to remove from the subscription list.


#### `Unsubscribe(Action<T1, T2, T3, T4>)`

```csharp
public void Unsubscribe(Action<T1, T2, T3, T4> action)
```

- **Description:** Removes a previously registered action so it will no longer be invoked when the signal is triggered.
- **Parameters:** `action` – The delegate to remove from the subscription list.




#### `Unsubscribe(ISignal, IAction)`

```csharp
public static void Unsubscribe(this ISignal it, IAction action)
```

- **Description:** Unsubscribes a non-generic action from a parameterless signal.
- **Parameters:**
    - `it` – The signal source.
    - `action` – The action to unsubscribe.

#### `Unsubscribe<T>(ISignal<T>, IAction<T>)`

```csharp
public static void Unsubscribe<T>(this ISignal<T> it, IAction<T> action)
```

- **Description:** Unsubscribes a generic action with one argument from a signal source.
- **Type parameter:** `T` — the emitted value type.
- **Parameters:**
    - `it` – The signal source.
    - `action` – The action to unsubscribe.

#### `Unsubscribe<T1, T2>(ISignal<T1,T2>, IAction<T1,T2>)`

```csharp
public static void Unsubscribe<T1, T2>(this ISignal<T1, T2> it, IAction<T1, T2> action)
```

- **Description:** Unsubscribes a two-argument action from a signal.
- **Type parameters:**
    - `T1` — the first emitted value
    - `T2` — the second emitted value
- **Parameters:**
    - `it` – The signal source.
    - `action` – The action to unsubscribe.

#### `Unsubscribe<T1,T2,T3>(ISignal<T1,T2,T3>, IAction<T1,T2,T3>)`

```csharp
public static void Unsubscribe<T1, T2, T3>(
    this ISignal<T1, T2, T3> it,
    IAction<T1, T2, T3> action
)
```

- **Description:** Unsubscribes a three-argument action from a signal.
- **Type parameters:**
    - `T1` — the first emitted value
    - `T2` — the second emitted value
    - `T3` — the third emitted value
- **Parameters:**
    - `it` – The signal source.
    - `action` – The action to unsubscribe.

#### `Unsubscribe<T1,T2,T3,T4>(ISignal<T1,T2,T3,T4>, IAction<T1,T2,T3,T4>)`

```csharp
public static void Unsubscribe<T1, T2, T3, T4>(
    this ISignal<T1, T2, T3, T4> it,
    IAction<T1, T2, T3, T4> action
)
```

- **Description:** Unsubscribes a four-argument action from a signal.
- **Type parameters:**
    - `T1` — the first emitted value
    - `T2` — the second emitted value
    - `T3` — the third emitted value
    - `T4` — the fourth emitted value
- **Parameters:**
    - `it` – The signal source.
    - `action` – The action to unsubscribe.

---

## 🗂 Examples of Usage

#### `ISignal` (no parameters)

```csharp
ISignal fireSignal = ...;
var action = new InlineAction(() => Debug.Log("OnFire"));
fireSignal.Subscribe(action);

// Later, unsubscribe
fireSignal.Unsubscribe(action);
```

#### `ISignal<T>` (with one parameter)

```csharp
ISignal<IEntity> pickUpSignal = ...;
var action = new InlineAction<IEntity>(entity => Debug.Log($"PickUp {entity.Name}"));
pickUpSignal.Subscribe(action);

// Later, unsubscribe
pickUpSignal.Unsubscribe(action);
```

#### `ISignal<T1, T2>` (with two parameters)

```csharp
ISignal<IEntity, int> hitSignal = ...;
var action = new InlineAction<IEntity, int>((entity, damage) =>
    Debug.Log($"{entity.Name} received {damage} damage"));

hitSignal.Subscribe(action);

// Later, unsubscribe
hitSignal.Unsubscribe(action);
```

#### `ISignal<T1, T2, T3>` (with three parameters)

```csharp
ISignal<IEntity, int, bool> attackSignal = ...;
var action = new InlineAction<IEntity, int, bool>((entity, damage, critical) =>
    Debug.Log($"{entity.Name} dealt {damage} damage (Critical: {critical})"));
attackSignal.Subscribe(action);

// Later, unsubscribe
attackSignal.Unsubscribe(action);
```

#### `ISignal<T1, T2, T3, T4>` (with four parameters)

```csharp
ISignal<IEntity, int, bool, Vector3> shootSignal = ...;
var action = new InlineAction<IEntity, int, bool, Vector3>((entity, ammo, success, position) =>
    Debug.Log($"{entity.Name} fired {ammo} bullets (Success: {success}) at {position}"));
shootSignal.Subscribe(action);

// Later, unsubscribe
shootSignal.Unsubscribe(action);
```