# 🧩 ThreadSafeVariable&lt;T&gt;

A **thread-safe wrapper** around a value of type `T` that uses locking to ensure safe concurrent access from multiple threads.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
  - [Type](#-type)
  - [Constructors](#-constructors)
    - [ThreadSafeVariable()](#threadsafevariable)
    - [ThreadSafeVariable(T)](#threadsafevariablet)
  - [Properties](#-properties)
    - [Value](#value)
  - [Methods](#-methods)
    - [ToString()](#tostring)
- [Thread Safety](#-thread-safety)

---

## 🗂 Example of Usage

```csharp
// Create a thread-safe variable
var sharedHealth = new ThreadSafeVariable<int>(100);

// Access from multiple threads
Task.Run(() =>
{
    sharedHealth.Value -= 10; // Thread-safe write
    Console.WriteLine($"Health: {sharedHealth.Value}"); // Thread-safe read
});

// Using the variable as an interface
IVariable<int> health = new ThreadSafeVariable<int>(100);
health.Value = 50; // Thread-safe via interface
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public class ThreadSafeVariable<T> : IVariable<T>
```

- **Description:** A **thread-safe wrapper** around a value of type `T` that uses locking to ensure safe concurrent access from multiple threads.
- **Inheritance:** [IVariable&lt;T&gt;](IVariable.md)
- **Type Parameter:** `T` – The type of the value to store.
- **Notes:** Uses `lock` for thread safety. Not serializable and does not support Unity serialization or Odin Inspector.
- **See also:** [Variable&lt;T&gt;](BaseVariable.md), [ReactiveVariable&lt;T&gt;](ReactiveVariable.md)

---

<div id="-constructors"></div>

### 🏗️ Constructors

#### `ThreadSafeVariable()`

```csharp
public ThreadSafeVariable()
```

- **Description:** Initializes a new instance with the default value of `T`.

#### `ThreadSafeVariable(T)`

```csharp
public ThreadSafeVariable(T value)
```

- **Description:** Initializes a new instance with a specified initial value `value`.
- **Parameter:** `value` – The initial value to initialize the instance with.

---

### 🔑 Properties

#### `Value`

```csharp
public T Value { get; set; }
```

- **Description:** Gets or sets the current value in a thread-safe manner.
- **Access:** Read-write
- **Thread Safety:** Both getter and setter are protected by a `lock`. The setter also performs equality checks to avoid unnecessary updates.

---

### 🏹 Methods

#### `ToString()`

```csharp
public override string ToString();
```

- **Description:** Returns a string that represents the current value in a thread-safe manner.
- **Returns:** A string representation of the current value.

---

## 🔒 Thread Safety

This class is designed for scenarios where a variable may be accessed from multiple threads simultaneously. All public members are protected by a `lock` to prevent race conditions.

**Key characteristics:**
- **Atomic operations:** Both read and write operations are atomic with respect to other locked operations.
- **Equality checking:** The setter uses `EqualityComparer<T>.Default` to avoid unnecessary updates when the value hasn't changed.
- **Blocking:** Calls to `Value` will block if another thread holds the lock. Use with caution to avoid deadlocks.
- **Not serializable:** This class is not marked `[Serializable]` and does not support Unity serialization.

**When to use:**
- Shared state between background threads and the main thread
- Simple value containers that need thread safety without reactivity
- Scenarios where `ReactiveVariable<T>` is not needed but thread safety is required

**When not to use:**
- Single-threaded scenarios (use `Variable<T>` instead)
- When you need change notifications (use `ReactiveVariable<T>` or `ThreadSafeReactiveVariable<T>`)
- When you need Unity serialization (use `Variable<T>` or `ReactiveVariable<T>`)

**See also:** [ThreadSafeReactiveVariable&lt;T&gt;](ThreadSafeReactiveVariable.md) for a thread-safe reactive variable with change notifications.