# 🧩 ReactiveProxyVariable&lt;T&gt;

Represents a **reactive proxy variable** that delegates reading, writing, and subscription operations
to external handlers.

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public class ReactiveProxyVariable<T> : IReactiveVariable<T>
```

- **Description:** Represents a **reactive proxy variable** that delegates reading, writing, and subscription operations
  to external handlers.
- **Inheritance:** [IReactiveVariable&lt;T&gt;](IReactiveVariable.md)
- **Type Parameter:** `T` – The type of the value being proxied.
- **Notes:** Supports Odin Inspector 


> [!TIP]
> This is useful when you need to **wrap an existing data source or event system** and expose it through
  the unified `IReactiveVariabl<T>` interface.

---

### 🏗️ Constructor

```csharp
public ReactiveProxyVariable(Func<T> getter, Action<T> setter)
```

- **Description:** Initializes a new instance of `ProxyVariable<T>` using the provided getter and setter functions.
- **Parameters:**
    - `getter` – A function to retrieve the value.
    - `setter` – An action to update the value.
    - `subscribe` – An action to handle the subscription.
    - `unsubscribe` – An action to handle the unsubscription.
- **Throws:** `ArgumentNullException` if either `getter`, `setter` `subscription` or `unsubscription` is null.

---

### 🔑 Properties

#### `Value`

```csharp
public T Value { get; set; }
```

- **Description:** Gets or sets the current value.
- **Access:** Read-write

---

### 🏹 Methods

#### `Invoke()`

```csharp
public T Invoke()
```

- **Description:** Invokes the variable and returns its current value.
- **Returns:** The current value of type `T`.

#### `Invoke(T arg)`

```csharp
public void Invoke(T arg)
```

- **Description:** Sets the value of the variable to the provided argument.
- **Parameter:** `arg` – The new value to assign to the variable.

#### `Subscribe(Action)`

```csharp
public Subscription<T> Subscribe(Action action)  
```

- **Description:** Subscribes an action to be invoked whenever the signal is triggered.
- **Parameter:** `action` – The delegate to be called when the value changes.
- **Returns:** A [Subscription&lt;T&gt;](../Events/Subscription%601.md) struct representing the active
  subscription.

#### `Unsubscribe(Action)`

```csharp
public void Unsubscribe(Action action)  
```

- **Description:** Removes a previously registered action so it will no longer be invoked when the signal is triggered.
- **Parameters:** `action` – The delegate to remove from the subscription list.

---

### 👷‍♂️ Builder

`ReactiveProxyVariable<T>` provides a **fluent builder** for convenience

```csharp
IReactiveVariable<int> variable = ReactiveProxyVariable<int>
    .StartBuild()
    .WithGetter(() => someInt)
    .WithSetter(v => someInt = v)
    .WithSubscribe(cb => myEvent += cb)
    .WithUnsubscribe(cb => myEvent -= cb)
    .Build();
```

---

## 📝 Notes

Below are some notes on when to use `ReactiveProxyVariable<T>`:

- Integrating external or third-party APIs (e.g., Unity’s `Transform`, networking states).
- Adapting existing properties / fields to `IReactiveVariable<T>` without refactoring.
- Testing: Makes it easy to substitute mock getters / setters in unit tests.