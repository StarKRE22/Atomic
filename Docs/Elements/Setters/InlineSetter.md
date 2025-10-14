# 🧩 InlineSetter&lt;T&gt;

Provides a wrapper around a standard `System.Action<T>` delegate

---

## 📑 Table of Contents

- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Constructors](#-constructors)
        - [InlineSetter(Action\<T>)](#inlinesetteractiont)
    - [Properties](#-properties)
        - [Value](#value)
    - [Methods](#-methods)
        - [Invoke(T)](#invoket)
        - [ToString()](#tostring)
    - [Operators](#-operators)
        - [InlineSetter\<T>(Action\<T>)](#operator-inlinesettertactiont)

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public class InlineSetter<T> : ISetter<T>
```

- **Description:** Provides a wrapper around a standard `System.Action<T>` delegate
- **Inheritance:** [ISetter&lt;T&gt;](ISetter.md)
- **Type Parameter:** `T` – the type of the value to be set.
- **Note:** Supports Odin Inspector

---

### 🏗️ Constructors <div id="-constructors"></div>

#### `InlineSetter(Action<T>)`

```csharp
public InlineSetter(Action<T> action)
```

- **Description:** Initializes a new instance of `InlineSetter<T>` with the specified action.
- **Parameter:** `action` — The action to invoke when the value is set.
- **Throws:** `ArgumentNullException` if `action` is null.

### 🔑 Properties

#### `Value`

```csharp
public T Value { set; }
```

- **Description:** Assigns the provided value.
- **Parameter:** `value` — the new value to be set.

### 🏹 Methods

#### `Invoke(T)`

```csharp
public void Invoke(T arg);
```

- **Description:** Invokes the setter by assigning the provided value.
- **Parameter:** `arg` — the value to set.
- **Notes:** Default implementation comes from [IAction&lt;T&gt;](../Actions/IAction%601.md).

#### `ToString()`

```csharp
public override string ToString();
```

- **Description:** Returns a string representing the method name of the underlying action.
- **Returns:** A string representation of the method name of the delegate.

### 🪄 Operators

#### `operator InlineSetter<T>(Action<T>)`

```csharp
public static implicit operator InlineSetter<T>(Action<T> action);
```

- **Description:** Implicitly converts a delegate of type `Action<T>` to an `InlineSetter<T>`.
- **Parameter:** `action` — The delegate to wrap.
- **Returns:** A new `InlineSetter<T>` instance containing the specified delegate.