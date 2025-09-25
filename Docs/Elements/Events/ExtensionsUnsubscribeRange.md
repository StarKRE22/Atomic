# 🧩 UnsubscribeRange Extensions

Provide utility methods for **batch unsubscribing** [IAction](../Actions/IActions.md) instances
from [ISignal](ISignals.md) sources.
Methods handle multiple actions at once, skipping null entries.

---

## 🏹 Methods

#### `UnsubscribeRange(ISignal, IEnumerable<IAction>)`

```csharp
public static void UnsubscribeRange(this ISignal it, IEnumerable<IAction> actions)
```

- **Description:** Unsubscribes multiple non-generic actions from a parameterless signal.
- **Parameters:**
    - `it` – The signal source.
    - `actions` – A collection of actions to unsubscribe.

#### `UnsubscribeRange<T>(ISignal<T>, IEnumerable<IAction<T>>)`

```csharp
public static void UnsubscribeRange<T>(this ISignal<T> it, IEnumerable<IAction<T>> actions)
```

- **Description:** Unsubscribes multiple generic actions (with one argument) from a signal source.
- **Type parameter:** `T` — the emitted value type.
- **Parameters:**
    - `it` – The signal source.
    - `actions` – A collection of actions to unsubscribe.

#### `UnsubscribeRange<T1,T2>(ISignal<T1,T2>, IEnumerable<IAction<T1,T2>>)`

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

#### `UnsubscribeRange<T1,T2,T3>(ISignal<T1,T2,T3>, IEnumerable<IAction<T1,T2,T3>>)`

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

#### `UnsubscribeRange<T1,T2,T3,T4>(ISignal<T1,T2,T3,T4>, IEnumerable<IAction<T1,T2,T3,T4>>)`

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

---

## 🗂 Examples of Usage

#### `ISignal` (no parameters)

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

#### `ISignal<T>` (with one parameter)

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

#### `ISignal<T1, T2>` (with two parameters)

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

#### `ISignal<T1, T2, T3>` (with three parameters)

```csharp
ISignal<IEntity, int, bool> attackSignal = ...;
var actions = new List<IAction<IEntity, int, bool>>
{
    new InlineAction<IEntity, int, bool>((entity, dmg, crit) => 
        Debug.Log($"{entity.Name} took {dmg} damage (Crit: {crit})")),
    new InlineAction<IEntity, int, bool>((entity, dmg, crit) => 
        Debug.Log($"Combat log updated for {entity.Name}"))
};

// Later, unsubscribe them
attackSignal.UnsubscribeRange(actions);
```

#### `ISignal<T1, T2, T3, T4>` (with four parameters)

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