# 🧩 ISignal Subscribe Extensions

Provide utility methods for **subscribing** [IAction](../Actions/IActions.md) instances to [ISignal](ISignals.md)
sources.

---

## 📑 Table of Contents

<ul>
  <li>
    <summary><a href="#-examples-of-usage">Examples of Usage</a></summary>
    <ul>
      <li><a href="#ex1">Action Subscription</a></li>
      <li><a href="#ex2">Action&lt;T&gt; Subscription</a></li>
      <li><a href="#ex3">IAction Subscription</a></li>
      <li><a href="#ex4">IAction&lt;T&gt; Subscription</a></li>
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
              <li><a href="#subscribeaction">Subscribe(Action)</a></li>
              <li><a href="#subscribeactiont">Subscribe(Action&lt;T&gt;)</a></li>
              <li><a href="#subscribeactiont1-t2">Subscribe(Action&lt;T1, T2&gt;)</a></li>
              <li><a href="#subscribeactiont1-t2-t3">Subscribe(Action&lt;T1, T2, T3&gt;)</a></li>
              <li><a href="#subscribeactiont1-t2-t3-t4">Subscribe(Action&lt;T1, T2, T3, T4&gt;)</a></li>
            </ul>
          </details>
        </li>
        <li>
          <details>
            <summary>IAction</summary>
            <ul>
              <li><a href="#subscribeisignal-iaction">Subscribe(ISignal, IAction)</a></li>
              <li><a href="#subscribetisignalt-iactiont">Subscribe&lt;T&gt;(ISignal&lt;T&gt;, IAction&lt;T&gt;)</a></li>
              <li><a href="#subscribet1-t2isignalt1-t2-iactiont1-t2">Subscribe&lt;T1, T2&gt;(ISignal&lt;T1, T2&gt;, IAction&lt;T1, T2&gt;)</a></li>
              <li><a href="#subscribet1-t2-t3isignalt1-t2-t3-iactiont1-t2-t3">Subscribe&lt;T1, T2, T3&gt;(ISignal&lt;T1, T2, T3&gt;, IAction&lt;T1, T2, T3&gt;)</a></li>
              <li><a href="#subscribet1-t2-t3-t4isignalt1-t2-t3-t4-iactiont1-t2-t3-t4">Subscribe&lt;T1, T2, T3, T4&gt;(ISignal&lt;T1, T2, T3, T4&gt;, IAction&lt;T1, T2, T3, T4&gt;)</a></li>
            </ul>
          </details>
        </li>
      </ul>
    </ul>
  </li>
</ul>


---

## 🗂 Examples of Usage

Below are examples of subscribing an `Action` delegate and [IAction](../Actions/Manual.md) instances to [ISignal](../Events/ISignals.md) instances:

### 1️⃣ Action Subscription <div id="ex1"></div>

```csharp
//Assume we have an instance of ISignal
ISignal signal = ...
    
//Assume we have an instance of Action 
Action action = ...

//Subscribe on the signal    
Subscription subscription = signal.Subscribe(action);

// Later, dispose to unsubscribe
subscription.Dispose();
```

### 2️⃣ Action\<T> Subscription <div id="ex2"></div>

```csharp
//Assume we have an instance of ISignal
ISignal<T> signal = ...
    
//Assume we have an instance of Action 
Action<T> action = ...

//Subscribe on the signal
Subscription<T> subscription = signal.Subscribe<T>(action);

// Later, dispose to unsubscribe
subscription.Dispose();
```

### 3️⃣ IAction Subscription <div id="ex3"></div>

```csharp
//Assume we have an instance of ISignal
ISignal signal = ...

//Assume we have an instance of IAction
IAction action = ...

//Subscribe on the signal
Subscription subscription = signal.Subscribe(action);

// Later, dispose to unsubscribe
subscription.Dispose();
```

### 4️⃣ IAction\<T> Subscription <div id="ex4"></div>
 
```csharp
//Assume we have an instance of ISignal
ISignal<T> signal = ...

//Assume we have an instance of IAction
IAction<T> action = ...

//Subscribe on the signal
Subscription<T> subscription = signal.Subscribe(acton);

// Later, dispose to unsubscribe
subscription.Dispose();
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public static class Extensions
```

---

### 🏹 Methods

#### `Subscribe(Action)`

```csharp
public Subscription Subscribe(Action action)
```

- **Description:** Subscribes an action to be invoked whenever the signal is triggered.
- **Parameter:** `action` – The delegate to be called when the value changes.
- **Returns:** The active [subscription](Subscription.md) that can be used to dispose of it.

#### `Subscribe(Action<T>)`

```csharp
public Subscription<T> Subscribe(Action<T> action)
```

- **Description:** Subscribes an action to be invoked whenever the signal is triggered.
- **Parameter:** `action` – The delegate to be called when the value changes.
- **Returns:** The active [subscription](Subscription%601.md) that can be used to dispose of it.

#### `Subscribe(Action<T1, T2>)`

```csharp
public Subscription<T1, T2> Subscribe(Action<T1, T2> action)
```

- **Description:** Subscribes an action to be invoked whenever the signal is triggered.
- **Parameter:** `action` – The delegate to be called when the value changes.
- **Returns:**  The active [subscription](Subscription%602.md) that can be used to dispose of it.

#### `Subscribe(Action<T1, T2, T3>)`

```csharp
public Subscription<T1, T2, T3> Subscribe(Action<T1, T2, T3> action)
```

- **Description:** Subscribes an action to be invoked whenever the signal is triggered.
- **Parameter:** `action` – The delegate to be called when the value changes.
- **Returns:** The active [subscription](Subscription%603.md) that can be used to dispose of it.

#### `Subscribe(Action<T1, T2, T3, T4>)`

```csharp
public Subscription<T1, T2, T3, T4> Subscribe(Action<T1, T2, T3, T4> action)
```

- **Description:** Subscribes an action to be invoked whenever the signal is triggered.
- **Parameter:** `action` – The delegate to be called when the value changes.
- **Returns:** The active [subscription](Subscription%604.md) that can be used to dispose
  of it.

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

<div id="subscribet1-t2isignalt1-t2-iactiont1-t2"></div>

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

<div id="subscribet1-t2-t3isignalt1-t2-t3-iactiont1-t2-t3"></div>


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

<div id="subscribet1-t2-t3-t4isignalt1-t2-t3-t4-iactiont1-t2-t3-t4"></div>


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