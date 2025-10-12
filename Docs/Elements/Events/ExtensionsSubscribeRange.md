# 🧩 ISignal SubscribeRange Extensions

Provide utility methods for **batch subscribing** [IAction](../Actions/IActions.md) instances to [ISignal](ISignals.md) sources.
Methods handle multiple actions at once, skipping null entries.

---

## 📑 Table of Contents

<ul>
  <li>
    <summary><a href="#-examples-of-usage">Examples of Usage</a></summary>
    <ul>
      <li><a href="#ex1">Non-generic Usage</a></li>
      <li><a href="#ex2">Generic Usage</a></li>
    </ul>
  </li>

  <li>
    <summary><a href="#-api-reference">API Reference</a></summary>
    <ul>
      <li><a href="#-type">Type</a></li>
      <li><a href="#-methods">Methods</a></li>
      <ul>
        <li>
          <details>
            <summary>IEnumerable</summary>
            <ul>
              <li><a href="#subscriberangeisignal-ienumerableiaction">SubscribeRange(ISignal, IEnumerable&lt;IAction&gt;)</a></li>
              <li><a href="#subscriberangetisignalt-ienumerableiactiont">SubscribeRange&lt;T&gt;(ISignal&lt;T&gt;, IEnumerable&lt;IAction&lt;T&gt;&gt;)</a></li>
              <li><a href="#subscriberanget1-t2isignalt1-t2-ienumerableiactiont1-t2">SubscribeRange&lt;T1, T2&gt;(ISignal&lt;T1, T2&gt;, IEnumerable&lt;IAction&lt;T1, T2&gt;&gt;)</a></li>
              <li><a href="#subscriberanget1-t2-t3isignalt1-t2-t3-ienumerableiactiont1-t2-t3">SubscribeRange&lt;T1, T2, T3&gt;(ISignal&lt;T1, T2, T3&gt;, IEnumerable&lt;IAction&lt;T1, T2, T3&gt;&gt;)</a></li>
              <li><a href="#subscriberanget1-t2-t3-t4isignalt1-t2-t3-t4-ienumerableiactiont1-t2-t3-t4">SubscribeRange&lt;T1, T2, T3, T4&gt;(ISignal&lt;T1, T2, T3, T4&gt;, IEnumerable&lt;IAction&lt;T1, T2, T3, T4&gt;&gt;)</a></li>
            </ul>
          </details>
        </li>
      </ul>
    </ul>
  </li>
</ul>


---

## 🗂 Examples of Usage

### 1️⃣ Non-generic Usage <div id="ex1"></div>

```csharp
ISignal fireSignal = ...;
var actions = new List<IAction>
{
    new InlineAction(() => Debug.Log("🔥 Fire VFX")),
    new InlineAction(() => Debug.Log("🔊 Fire SFX"))
};
fireSignal.SubscribeRange(actions);
```

### 2️⃣ Generic Usage <div id="ex2"></div>

```csharp
ISignal<IEntity> pickUpSignal = ...;
var actions = new List<IAction<IEntity>>
{
    new InlineAction<IEntity>(entity => Debug.Log($"Picked up {entity.Name}")),
    new InlineAction<IEntity>(entity => Debug.Log($"Inventory updated with {entity.Name}"))
};
pickUpSignal.SubscribeRange(actions);
```

<!--

#### `ISignal<T1, T2>` (with two parameters)

```csharp
ISignal<IEntity, int> hitSignal = ...;
var actions = new List<IAction<IEntity, int>>
{
    new InlineAction<IEntity, int>((entity, damage) => Debug.Log($"{entity.Name} lost {damage} HP")),
    new InlineAction<IEntity, int>((entity, damage) => Debug.Log($"UI updated for {entity.Name}"))
};
hitSignal.SubscribeRange(actions);
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
attackSignal.SubscribeRange(actions);
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
shootSignal.SubscribeRange(actions);
```
-->

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public static class Extensions
```

---

### 🏹 Methods

<div id="subscriberangeisignal-ienumerableiaction"></div>

#### `SubscribeRange(ISignal, IEnumerable<IAction>)`

```csharp
public static void SubscribeRange(this ISignal it, IEnumerable<IAction> actions)
```

- **Description:** Subscribes multiple non-generic actions to a parameterless signal.
- **Parameters:**
    - `it` – The signal source.
    - `actions` – A collection of actions to subscribe.

<div id="subscriberangetisignalt-ienumerableiactiont"></div>

#### `SubscribeRange<T>(ISignal<T>, IEnumerable<IAction<T>>)`

```csharp
public static void SubscribeRange<T>(this ISignal<T> it, IEnumerable<IAction<T>> actions)
```

- **Description:** Subscribes multiple generic actions (with one argument) to a signal source.
- **Type parameter:** `T` — the emitted value type.
- **Parameters:**
    - `it` – The signal source.
    - `actions` – A collection of actions to subscribe.

<div id="subscriberanget1-t2isignalt1-t2-ienumerableiactiont1-t2"></div>

#### `SubscribeRange<T1,T2>(ISignal<T1,T2>, IEnumerable<IAction<T1,T2>>)`

```csharp
public static void SubscribeRange<T1, T2>(
    this ISignal<T1, T2> it,
    IEnumerable<IAction<T1, T2>> actions
)
```

- **Description:** Subscribes multiple two-argument actions to a signal.
- **Type parameters:**
    - `T1` — the first emitted value
    - `T2` — the second emitted value
- **Parameters:**
    - `it` – The signal source.
    - `actions` – A collection of actions to subscribe.

<div id="subscriberanget1-t2-t3isignalt1-t2-t3-ienumerableiactiont1-t2-t3"></div>

#### `SubscribeRange<T1,T2,T3>(ISignal<T1,T2,T3>, IEnumerable<IAction<T1,T2,T3>>)`

```csharp
public static void SubscribeRange<T1, T2, T3>(
    this ISignal<T1, T2, T3> it,
    IEnumerable<IAction<T1, T2, T3>> actions
)
```

- **Description:** Subscribes multiple three-argument actions to a signal.
- **Type parameters:**
    - `T1` — the first emitted value
    - `T2` — the second emitted value
    - `T3` — the third emitted value
- **Parameters:**
    - `it` – The signal source.
    - `actions` – A collection of actions to subscribe.

<div id="subscriberanget1-t2-t3-t4isignalt1-t2-t3-t4-ienumerableiactiont1-t2-t3-t4"></div>

#### `SubscribeRange<T1,T2,T3,T4>(ISignal<T1,T2,T3,T4>, IEnumerable<IAction<T1,T2,T3,T4>>)`

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