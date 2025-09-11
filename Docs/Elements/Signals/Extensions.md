# 🧩 Signal Extensions

The **Extensions** class provides utility methods for **subscribing** and **unsubscribing** [IAction](IAction.md) instances to and from [ISignal](ISignal.md) sources. It also supports **batch operations** (`SubscribeRange` / `UnsubscribeRange`) that handle multiple actions at once, skipping null entries.

---

## 🔹 Subscribe Methods

### `Subscribe(this ISignal, IAction)`
```csharp
public static Subscription Subscribe(this ISignal it, IAction action)
```
- **Description:** Subscribes a non-generic action to a parameterless signal.
- **Parameters:**
    - `it` – The signal source.
    - `action` – The action to subscribe.
- **Returns:** [Subscription](Subscription.md/#-subscription) – Represents the subscription.
- **Example of Usage:**

  ```csharp
  ISignal fireSignal = ...
  fireSignal.Subscribe(new InlineAction(() => Debug.Log("OnFire")));
  ``` 

### `Subscribe<T>(this ISignal<T>, IAction<T>)`
```csharp
public static Subscription<T> Subscribe<T>(this ISignal<T> it, IAction<T> action)
```
- **Description:** Subscribes a generic action with one argument to a signal source.
- **Type parameter:** `T` — the emitted value type.
- **Parameters:**
    - `it` – The signal source.
    - `action` – The action to subscribe.
- **Returns:** [Subscription&lt;T&gt;](Subscription.md/#-subscriptiont) – Represents the subscription.
- **Example of Usage:**

  ```csharp
  ISignal<IEntity> pickUpSignal = ...;
  pickUpSignal.Subscribe(new InlineAction(pickUp => Debug.Log($"OnPickUp: {pickUp.Name}")));
  ```  

### `Subscribe<T1, T2>(this ISignal<T1,T2>, IAction<T1,T2>)`
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
- **Returns:** [Subscription<T1, T2>](Subscription.md/#-subscriptiont1-t2) – Represents the subscription.
- **Example of Usage:**

   ```csharp
  ISignal<IEntity, int> hitSignal = ...
  hitSignal.Subscribe(new InlineAction<IEntity, int>((entity, damage) => 
      Debug.Log($"{entity.Name} received {damage} damage")));
  ```

### `Subscribe<T1,T2,T3>(this ISignal<T1,T2,T3>, IAction<T1,T2,T3>)`
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
- **Returns:** [Subscription<T1, T2, T3>](Subscription.md/#-subscriptiont1-t2-t3) – Represents the subscription.
- **Example of Usage:**
   
  ```csharp
  ISignal<IEntity, int, bool> attackSignal = ...;
  attackSignal.Subscribe(new InlineAction<IEntity, int, bool>((entity, damage, critical) =>
        Debug.Log($"{entity.Name} dealt {damage} damage (Critical: {critical})")));
  ```

### `Subscribe<T1,T2,T3,T4>(this ISignal<T1,T2,T3,T4>, IAction<T1,T2,T3,T4>)`
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
- **Returns:** [Subscription<T1, T2, T3, T4>](Subscription.md/#-subscriptiont1-t2-t3-t4) – Represents the subscription.
- **Example of Usage:**

  ```csharp
  ISignal<IEntity, int, bool, Vector3> shootSignal = ...
  shootSignal.Subscribe(new InlineAction<IEntity, int, bool, Vector3>((entity, ammo, success, position) =>
        Debug.Log($"{entity.Name} fired {ammo} bullets (Success: {success}) at {position}")));
  ```

---

## 🔹 Unsubscribe Methods

### `Unsubscribe(this ISignal, IAction)`
```csharp
public static void Unsubscribe(this ISignal it, IAction action)
```
- **Description:** Unsubscribes a non-generic action from a parameterless signal.
- **Parameters:**
    - `it` – The signal source.
    - `action` – The action to unsubscribe.
- **Example of Usage:**

  ```csharp
  ISignal fireSignal = ...;
  var action = new InlineAction(() => Debug.Log("OnFire"));
  fireSignal.Subscribe(action);
  
  //Later, unsubscribe
  fireSignal.Unsubscribe(action);
  ```

### `Unsubscribe<T>(this ISignal<T>, IAction<T>)`
```csharp
public static void Unsubscribe<T>(this ISignal<T> it, IAction<T> action)
```
- **Description:** Unsubscribes a generic action with one argument from a signal source.
- **Type parameter:** `T` — the emitted value type.
- **Parameters:**
    - `it` – The signal source.
    - `action` – The action to unsubscribe.
- **Example of Usage:**

  ```csharp
  ISignal<IEntity> pickUpSignal = ...;
  var action = new InlineAction<IEntity>(entity => Debug.Log($"PickUp {entity.Name}"));
  pickUpSignal.Subscribe(action);
  
  //Later, unsubscribe
  pickUpSignal.Unsubscribe(action);
  ```

### `Unsubscribe<T1, T2>(this ISignal<T1,T2>, IAction<T1,T2>)`
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
- **Example of Usage:**

  ```csharp
  ISignal<IEntity, int> hitSignal = ...;
  var action = new InlineAction<IEntity, int>((entity, damage) =>
        Debug.Log($"{entity.Name} received {damage} damage"));
  hitSignal.Subscribe(action);
  
  //Later, unsubscribe
  hitSignal.Unsubscribe(action);
  ```

### `Unsubscribe<T1,T2,T3>(this ISignal<T1,T2,T3>, IAction<T1,T2,T3>)`
```csharp
public static void Unsubscribe<T1, T2, T3>(this ISignal<T1, T2, T3> it, IAction<T1, T2, T3> action)
```
- **Description:** Unsubscribes a three-argument action from a signal.
- **Type parameters:**
    - `T1` — the first emitted value
    - `T2` — the second emitted value
    - `T3` — the third emitted value
- **Parameters:**
    - `it` – The signal source.
    - `action` – The action to unsubscribe.
- **Example of Usage:**

  ```csharp
  ISignal<IEntity, int, bool> attackSignal = ...;
  var action = new InlineAction<IEntity, int, bool>((entity, damage, critical) =>
        Debug.Log($"{entity.Name} dealt {damage} damage (Critical: {critical})"));
  attackSignal.Subscribe(action);
  
  //Later, unsubscribe
  attackSignal.Unsubscribe(action);
  ```

### `Unsubscribe<T1,T2,T3,T4>(this ISignal<T1,T2,T3,T4>, IAction<T1,T2,T3,T4>)`
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
- **Example of Usage:**

  ```csharp
  ISignal<IEntity, int, bool, Vector3> shootSignal = ...;
  var action = new InlineAction<IEntity, int, bool, Vector3>((entity, ammo, success, position) =>
        Debug.Log($"{entity.Name} fired {ammo} bullets (Success: {success}) at {position}"));
  shootSignal.Subscribe(action);
  
  //Later, unsubscribe
  shootSignal.Unsubscribe(action);
  ```
---

## 🔹 SubscribeRange Methods

### `SubscribeRange(this ISignal, IEnumerable<IAction>)`
```csharp
public static void SubscribeRange(this ISignal it, IEnumerable<IAction> actions)
```
- **Description:** Subscribes multiple non-generic actions to a parameterless signal.
- **Parameters:**
    - `it` – The signal source.
    - `actions` – A collection of actions to subscribe.
- **Example of Usage:**

  ```csharp
  ISignal fireSignal = ...;
  var actions = new List<IAction>
  {
      new InlineAction(() => Debug.Log("🔥 Fire VFX")),
      new InlineAction(() => Debug.Log("🔊 Fire SFX"))
  };
  fireSignal.SubscribeRange(actions);
  ```

### `SubscribeRange<T>(this ISignal<T>, IEnumerable<IAction<T>>)`
```csharp
public static void SubscribeRange<T>(this ISignal<T> it, IEnumerable<IAction<T>> actions)
```
- **Description:** Subscribes multiple generic actions (with one argument) to a signal source.
- **Type parameter:** `T` — the emitted value type.
- **Parameters:**
    - `it` – The signal source.
    - `actions` – A collection of actions to subscribe.
- **Example of Usage:**

  ```csharp
  ISignal<IEntity> pickUpSignal = ...;
  var actions = new List<IAction<IEntity>>
  {
      new InlineAction<IEntity>(entity => Debug.Log($"Picked up {entity.Name}")),
      new InlineAction<IEntity>(entity => Debug.Log($"Inventory updated with {entity.Name}"))
  };
  pickUpSignal.SubscribeRange(actions);
  ```

### `SubscribeRange<T1,T2>(this ISignal<T1,T2>, IEnumerable<IAction<T1,T2>>)`
```csharp
public static void SubscribeRange<T1, T2>(this ISignal<T1, T2> it, IEnumerable<IAction<T1, T2>> actions)
```
- **Description:** Subscribes multiple two-argument actions to a signal.
- **Type parameters:**
    - `T1` — the first emitted value
    - `T2` — the second emitted value
- **Parameters:**
    - `it` – The signal source.
    - `actions` – A collection of actions to subscribe.
- **Example of Usage:**

  ```csharp
  ISignal<IEntity, int> hitSignal = ...;
  var actions = new List<IAction<IEntity, int>>
  {
      new InlineAction<IEntity, int>((entity, damage) => Debug.Log($"{entity.Name} lost {damage} HP")),
      new InlineAction<IEntity, int>((entity, damage) => Debug.Log($"UI updated for {entity.Name}"))
  };
  hitSignal.SubscribeRange(actions);
  ```

### `SubscribeRange<T1,T2,T3>(this ISignal<T1,T2,T3>, IEnumerable<IAction<T1,T2,T3>>)`
```csharp
public static void SubscribeRange<T1, T2, T3>(this ISignal<T1, T2, T3> it, IEnumerable<IAction<T1, T2, T3>> actions)
```
- **Description:** Subscribes multiple three-argument actions to a signal.
- **Type parameters:**
    - `T1` — the first emitted value
    - `T2` — the second emitted value
    - `T3` — the third emitted value
- **Parameters:**
    - `it` – The signal source.
    - `actions` – A collection of actions to subscribe.
- **Example of Usage:**

  ```csharp
  ISignal<IEntity, int, bool> attackSignal = ...;
  var actions = new List<IAction<IEntity, int, bool>>
  {
      new InlineAction<IEntity, int, bool>((entity, dmg, crit) => Debug.Log($"{entity.Name} took {dmg} damage (Crit: {crit})")),
      new InlineAction<IEntity, int, bool>((entity, dmg, crit) => Debug.Log($"Combat log updated for {entity.Name}"))
  };
  attackSignal.SubscribeRange(actions);
  ```

### `SubscribeRange<T1,T2,T3,T4>(this ISignal<T1,T2,T3,T4>, IEnumerable<IAction<T1,T2,T3,T4>>)`
```csharp
public static void SubscribeRange<T1, T2, T3, T4>(
    this ISignal<T1, T2, T3, T4> it,
    IEnumerable<IAction<T1, T2, T3, T4>> actions
)
```
- **Description:** Subscribes multiple four-argument actions to a signal.
- **Type parameters:**
    - `T1` — the first emitted value
    - `T2` — the second emitted value
    - `T3` — the third emitted value
    - `T4` — the fourth emitted value
- **Parameters:**
    - `it` – The signal source.
    - `actions` – A collection of actions to subscribe.
- **Example of Usage:**

  ```csharp
  ISignal<IEntity, int, bool, Vector3> shootSignal = ...;
  var actions = new List<IAction<IEntity, int, bool, Vector3>>
  {
      new InlineAction<IEntity, int, bool, Vector3>((entity, ammo, success, pos) =>
          Debug.Log($"{entity.Name} fired {ammo} bullets (Success: {success}) at {pos}")),
      
      new InlineAction<IEntity, int, bool, Vector3>((entity, ammo, success, pos) =>
          Debug.Log($"Recoil effect triggered at {pos}"))
  };
  shootSignal.SubscribeRange(actions);
  ```
---

## 🔹 UnsubscribeRange Methods

### `UnsubscribeRange(this ISignal, IEnumerable<IAction>)`
```csharp
public static void UnsubscribeRange(this ISignal it, IEnumerable<IAction> actions)
```
- **Description:** Unsubscribes multiple non-generic actions from a parameterless signal.
- **Parameters:**
    - `it` – The signal source.
    - `actions` – A collection of actions to unsubscribe.
- **Example of Usage:**

  ```csharp
  ISignal fireSignal = ...;
  var actions = new List<IAction>
  {
      new InlineAction(() => Debug.Log("🔥 Fire VFX")),
      new InlineAction(() => Debug.Log("🔊 Fire SFX"))
  };

  // Later, unsubscribe all
  fireSignal.UnsubscribeRange(actions);
  ```

### `UnsubscribeRange<T>(this ISignal<T>, IEnumerable<IAction<T>>)`
```csharp
public static void UnsubscribeRange<T>(this ISignal<T> it, IEnumerable<IAction<T>> actions)
```
- **Description:** Unsubscribes multiple generic actions (with one argument) from a signal source.
- **Type parameter:** `T` — the emitted value type.
- **Parameters:**
    - `it` – The signal source.
    - `actions` – A collection of actions to unsubscribe.
- **Example of Usage:**

  ```csharp
  ISignal<IEntity> pickUpSignal = ...;
  var actions = new List<IAction<IEntity>>
  {
      new InlineAction<IEntity>(entity => Debug.Log($"Picked up {entity.Name}")),
      new InlineAction<IEntity>(entity => Debug.Log($"Inventory updated with {entity.Name}"))
  };

  // Later, unsubscribe them
  pickUpSignal.UnsubscribeRange(actions);
  ```

### `UnsubscribeRange<T1,T2>(this ISignal<T1,T2>, IEnumerable<IAction<T1,T2>>)`
```csharp
public static void UnsubscribeRange<T1, T2>(this ISignal<T1, T2> it, IEnumerable<IAction<T1, T2>> actions)
```
- **Description:** Unsubscribes multiple two-argument actions from a signal.
- **Type parameters:**
    - `T1` — the first emitted value
    - `T2` — the second emitted value
- **Parameters:**
    - `it` – The signal source.
    - `actions` – A collection of actions to unsubscribe.
- **Example of Usage:**

  ```csharp
  ISignal<IEntity, int> hitSignal = ...;
  var actions = new List<IAction<IEntity, int>>
  {
      new InlineAction<IEntity, int>((entity, damage) => Debug.Log($"{entity.Name} lost {damage} HP")),
      new InlineAction<IEntity, int>((entity, damage) => Debug.Log($"UI updated for {entity.Name}"))
  };

  // Later, unsubscribe them
  hitSignal.UnsubscribeRange(actions);
  ```

### `UnsubscribeRange<T1,T2,T3>(this ISignal<T1,T2,T3>, IEnumerable<IAction<T1,T2,T3>>)`
```csharp
public static void UnsubscribeRange<T1, T2, T3>(this ISignal<T1, T2, T3> it, IEnumerable<IAction<T1, T2, T3>> actions)
```
- **Description:** Unsubscribes multiple three-argument actions from a signal.
- **Type parameters:**
    - `T1` — the first emitted value
    - `T2` — the second emitted value
    - `T3` — the third emitted value
- **Parameters:**
    - `it` – The signal source.
    - `actions` – A collection of actions to unsubscribe.
- **Example of Usage:**

  ```csharp
  ISignal<IEntity, int, bool> attackSignal = ...;
  var actions = new List<IAction<IEntity, int, bool>>
  {
      new InlineAction<IEntity, int, bool>((entity, dmg, crit) => Debug.Log($"{entity.Name} took {dmg} damage (Crit: {crit})")),
      new InlineAction<IEntity, int, bool>((entity, dmg, crit) => Debug.Log($"Combat log updated for {entity.Name}"))
  };

  // Later, unsubscribe them
  attackSignal.UnsubscribeRange(actions);
  ```

### `UnsubscribeRange<T1,T2,T3,T4>(this ISignal<T1,T2,T3,T4>, IEnumerable<IAction<T1,T2,T3,T4>>)`
```csharp
public static void UnsubscribeRange<T1, T2, T3, T4>(
    this ISignal<T1, T2, T3, T4> it,
    IEnumerable<IAction<T1, T2, T3, T4>> actions
)
```
- **Description:** Unsubscribes multiple four-argument actions from a signal.
- **Type parameters:**
    - `T1` — the first emitted value
    - `T2` — the second emitted value
    - `T3` — the third emitted value
    - `T4` — the fourth emitted value
- **Parameters:**
    - `it` – The signal source.
    - `actions` – A collection of actions to unsubscribe.
- **Example of Usage:**

  ```csharp
  ISignal<IEntity, int, bool, Vector3> shootSignal = ...;
  var actions = new List<IAction<IEntity, int, bool, Vector3>>
  {
      new InlineAction<IEntity, int, bool, Vector3>((entity, ammo, success, pos) =>
          Debug.Log($"{entity.Name} fired {ammo} bullets (Success: {success}) at {pos}")),
      
      new InlineAction<IEntity, int, bool, Vector3>((entity, ammo, success, pos) =>
          Debug.Log($"Recoil effect triggered at {pos}"))
  };

  // Later, unsubscribe them
  shootSignal.UnsubscribeRange(actions);
  ```
---

## 📝 Notes
- **Null Safety** – Both single and batch methods safely skip `null` actions.
- **Performance** – Methods are aggressively inlined.
- **Batch Execution** – `SubscribeRange` and `UnsubscribeRange` are useful for collections.