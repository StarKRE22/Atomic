# 🧩 ISignal Unsubscribe Extensions

Provide utility methods for **unsubscribing** [IAction](../Actions/IActions.md) instances from [ISignal](ISignals.md)
sources.

---

## 📑 Table of Contents

<ul>
  <li>
    <summary><a href="#-examples-of-usage">Examples of Usage</a></summary>
    <ul>
      <li><a href="#ex1">Action Unsubscription</a></li>
      <li><a href="#ex2">Action&lt;T&gt; Unsubscription</a></li>
      <li><a href="#ex3">IAction Unsubscription</a></li>
      <li><a href="#ex4">IAction&lt;T&gt; Unsubscription</a></li>
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
            <summary>Action</summary>
            <ul>
              <li><a href="#unsubscribeaction">Unsubscribe(Action)</a></li>
              <li><a href="#unsubscribeactiont">Unsubscribe(Action&lt;T&gt;)</a></li>
              <li><a href="#unsubscribeactiont1-t2">Unsubscribe(Action&lt;T1, T2&gt;)</a></li>
              <li><a href="#unsubscribeactiont1-t2-t3">Unsubscribe(Action&lt;T1, T2, T3&gt;)</a></li>
              <li><a href="#unsubscribeactiont1-t2-t3-t4">Unsubscribe(Action&lt;T1, T2, T3, T4&gt;)</a></li>
            </ul>
          </details>
        </li>
        <li>
          <details>
            <summary>IAction</summary>
            <ul>
              <li><a href="#unsubscribeisignal-iaction">Unsubscribe(ISignal, IAction)</a></li>
              <li><a href="#unsubscribetisignalt-iactiont">Unsubscribe&lt;T&gt;(ISignal&lt;T&gt;, IAction&lt;T&gt;)</a></li>
              <li><a href="#unsubscribet1-t2isignalt1-t2-iactiont1-t2">Unsubscribe&lt;T1, T2&gt;(ISignal&lt;T1, T2&gt;, IAction&lt;T1, T2&gt;)</a></li>
              <li><a href="#unsubscribet1-t2-t3isignalt1-t2-t3-iactiont1-t2-t3">Unsubscribe&lt;T1, T2, T3&gt;(ISignal&lt;T1, T2, T3&gt;, IAction&lt;T1, T2, T3&gt;)</a></li>
              <li><a href="#unsubscribet1-t2-t3-t4isignalt1-t2-t3-t4-iactiont1-t2-t3-t4">Unsubscribe&lt;T1, T2, T3, T4&gt;(ISignal&lt;T1, T2, T3, T4&gt;, IAction&lt;T1, T2, T3, T4&gt;)</a></li>
            </ul>
          </details>
        </li>
      </ul>
    </ul>
  </li>
</ul>


---

## 🗂 Examples of Usage

Below are examples of unsubscribing an `Action` delegate and [IAction](../Actions/Manual.md) instances from [ISignal](../Events/ISignals.md) instances:

### 1️⃣ Action Unsubscription <div id="ex1"></div>

```csharp
// Assume we have an instance of ISignal
ISignal signal = ...
    
// Assume we have an instance of Action 
Action action = ...

// Unsubscribe from the signal    
signal.Unsubscribe(action);
```

### 2️⃣ Action\<T> Unsubscription <div id="ex2"></div>

```csharp
// Assume we have an instance of ISignal
ISignal<T> signal = ...
    
// Assume we have an instance of Action 
Action<T> action = ...

// Unsubscribe from the signal
signal.Unsubscribe(action);
```

### 3️⃣ IAction Unsubscription <div id="ex3"></div>

```csharp
// Assume we have an instance of ISignal
ISignal signal = ...

// Assume we have an instance of IAction
IAction action = ...

// Unsubscribe from the signal
signal.Unsubscribe(action);
```

### 4️⃣ IAction\<T> Unsubscription <div id="ex4"></div>

```csharp
//Assume we have an instance of ISignal
ISignal<T> signal = ...

//Assume we have an instance of IAction
IAction<T> action = ...

//Subscribe on the signal
signal.Unsubscribe(acton);
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public static class Extensions
```

---

### 🏹 Methods


#### `Unsubscribe(Action)`

```csharp
public void Unsubscribe(Action action)
```

- **Description:** Removes a previously registered action so it will no longer be invoked when the signal is triggered.
- **Parameters:** `action` – The delegate to remove from the subscription list.

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

---

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

<div id="unsubscribet1-t2isignalt1-t2-iactiont1-t2"></div>

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

<div id="unsubscribet1-t2-t3isignalt1-t2-t3-iactiont1-t2-t3"></div>

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

<div id="unsubscribet1-t2-t3-t4isignalt1-t2-t3-t4-iactiont1-t2-t3-t4"></div>

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

<!--

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
-->