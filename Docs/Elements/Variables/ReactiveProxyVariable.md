# 🧩 InlineReactiveVariable&lt;T&gt;

Represents a **reactive proxy variable** that delegates reading, writing, and subscription operations
to external handlers. This is useful when you need to **wrap an existing data source or event system** and expose it
through the unified [IReactiveVariable\<T>](IReactiveVariable.md) interface.

---

## 📑 Table of Contents

- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Constructors](#-constructors)
        - [InlineReactiveVariable(Func<T>, Action<T>)](#inlinereactivevariablefunct-actiont)
    - [Events](#-events)
        - [OnEvent](#onevent)
    - [Properties](#-properties)
        - [Value](#value)
    - [Methods](#-methods)
        - [Invoke()](#invoke)
        - [Invoke(T)](#invoket)
        - [StartBuild()](#startbuild)
- [Notes](#-notes)

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public class InlineReactiveVariable<T> : IReactiveVariable<T>
```

- **Description:** Represents a **reactive proxy variable** that delegates reading, writing, and subscription operations
  to external handlers.
- **Inheritance:** [IReactiveVariable&lt;T&gt;](IReactiveVariable.md)
- **Type Parameter:** `T` – The type of the value being proxied.
- **Notes:** Supports Odin Inspector

---

<div id="-constructors"></div>

### 🏗️ Constructors

#### `InlineReactiveVariable(Func<T>, Action<T>)`

```csharp
public InlineReactiveVariable(Func<T> getter, Action<T> setter)
```

- **Description:** Initializes a new instance of `ProxyVariable<T>` using the provided getter and setter functions.
- **Parameters:**
    - `getter` – A function to retrieve the value.
    - `setter` – An action to update the value.
    - `subscribe` – An action to handle the subscription.
    - `unsubscribe` – An action to handle the unsubscription.
- **Throws:** `ArgumentNullException` if either `getter`, `setter` `subscription` or `unsubscription` is null.

---

### ⚡ Events

#### `OnEvent`

```csharp
event Action<T> OnEvent
```

- **Description:** Triggered whenever the value changes.
- **Parameter**: `T` – The new value after the change.
- **Note:** Allows subscribers to react to value changes in a reactive programming pattern.

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

#### `Invoke(T)`

```csharp
public void Invoke(T arg)
```

- **Description:** Sets the value of the variable to the provided argument.
- **Parameter:** `arg` – The new value to assign to the variable.

#### `StartBuild()`

```csharp
public static Builder StartBuild()
```

- **Description:** Begins building a proxy variable using a fluent builder pattern.
- **Returns** A new struct instance of the [Builder](ReactiveProxyVariableBuilder.md)

---

## 📝 Notes

- Integrating external or third-party APIs (e.g., Unity’s `Transform`, networking states).
- Adapting existing properties / fields to [IReactiveVariable\<T>](IReactiveVariable.md) without refactoring.
- Testing: Makes it easy to substitute mock getters / setters in unit tests.