# 🧩 Subscribe Signal Extensions

Provide utility methods for **subscribing** [IAction](../Actions/IActions.md) instances to [ISignal](ISignals.md)
sources.

---

#### `Subscribe(ISignal, IAction)`

```csharp
public static Subscription Subscribe(this ISignal it, IAction action)
```

- **Description:** Subscribes a non-generic action to a parameterless signal.
- **Parameters:**
    - `it` – The signal source.
    - `action` – The action to subscribe.
- **Returns:** [Subscription](Subscription.md) – Represents the subscription.
- **Example of Usage:**

  ```csharp
  ISignal fireSignal = ...
  fireSignal.Subscribe(new InlineAction(() => Debug.Log("OnFire")));
  ``` 

---

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
- **Example of Usage:**

  ```csharp
  ISignal<IEntity> pickUpSignal = ...;
  pickUpSignal.Subscribe(new InlineAction(pickUp => Debug.Log($"OnPickUp: {pickUp.Name}")));
  ```  

---

#### `Subscribe<T1, T2>(ISignal<T1,T2>, IAction<T1,T2>)`

```csharp
public static Subscription<T1, T2> Subscribe<T1, T2>(this ISignal<T1, T2> it, IAction<T1, T2> action)
```

- **Description:** Subscribes a two-argument action to a signal.
- **Type parameters:**
    - `T1` — the first emitted value
    - `T2` — the second emitted value
- **Parameters:**
    - `it` – The signal source.
    - `action` – The action to subscribe.
- **Returns:** [Subscription<T1, T2>](Subscription%602.md) – Represents the subscription.
- **Example of Usage:**

   ```csharp
  ISignal<IEntity, int> hitSignal = ...
  hitSignal.Subscribe(new InlineAction<IEntity, int>((entity, damage) => 
      Debug.Log($"{entity.Name} received {damage} damage")));
  ```

---

#### `Subscribe<T1,T2,T3>(ISignal<T1,T2,T3>, IAction<T1,T2,T3>)`

```csharp
public static Subscription<T1, T2, T3> Subscribe<T1, T2, T3>(this ISignal<T1, T2, T3> it, IAction<T1, T2, T3> action)
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
- **Example of Usage:**

  ```csharp
  ISignal<IEntity, int, bool> attackSignal = ...;
  attackSignal.Subscribe(new InlineAction<IEntity, int, bool>((entity, damage, critical) =>
        Debug.Log($"{entity.Name} dealt {damage} damage (Critical: {critical})")));
  ```

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
- **Example of Usage:**

  ```csharp
  ISignal<IEntity, int, bool, Vector3> shootSignal = ...
  shootSignal.Subscribe(new InlineAction<IEntity, int, bool, Vector3>((entity, ammo, success, position) =>
        Debug.Log($"{entity.Name} fired {ammo} bullets (Success: {success}) at {position}")));
  ```
