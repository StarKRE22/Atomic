# 🧩 InlineAction&lt;T1, T2, T3&gt;

Represents an action with three parameters that can be invoked.

---

## 📑 Table of Contents

- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Constructors](#-constructors)
        - [InlineAction(Action\<T1, T2, T3>)](#inlineactionactiont1-t2-t3)
    - [Methods](#-methods)
        - [Invoke(T1, T2, T3)](#invoket1-t2-t3)
        - [ToString()](#tostring)
    - [Operators](#-operators)
        - [InlineAction(Action\<T1, T2, T3>)](#operator-inlineactiont1-t2-t3actiont1-t2-t3)

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public class InlineAction<T1, T2, T3> : IAction<T1, T2, T3>
```

- **Description:** Represents an action with three parameters that can be invoked.
- **Type parameters**
    - `T1` — the first argument
    - `T2` — the second argument
    - `T3` — the third argument
- **Inheritance:** [IAction&lt;T1, T2, T3&gt;](IAction%603.md)
- **Note:** Supports Odin Inspector

---

### 🏗️ Constructors <div id="-constructors"></div>

#### `InlineAction(Action<T1, T2, T3>)`

```csharp
public InlineAction(Action<T1, T2, T3> action)
```

- **Description:** Initializes a new instance with the specified action.
- **Parameter:** `action` – The action to invoke.
- **Throws:** `ArgumentNullException` if `action` is null.

---

### 🏹 Methods

#### `Invoke(T1, T2, T3)`

```csharp
public void Invoke(T1 arg1, T2 arg2, T3 arg3)
```

- **Description:** Invokes the wrapped action with the specified arguments.
- **Parameters:**
    - `arg1` – The first argument
    - `arg2` – The second argument
    - `arg3` – The third argument

#### `ToString()`

```csharp
public override string ToString();
```

- **Description:** Returns a string that represents the method name of action.
- **Returns:** A string representation of the method name of delegate.

---

### 🪄 Operators

#### `operator InlineAction<T1, T2, T3>(Action<T1, T2, T3>)`

```csharp
public static implicit operator InlineAction<T1, T2, T3>(Action<T1, T2, T3> action);
```

- **Description:** Implicitly converts a delegate of type `Action<T1, T2, T3>` to a `InlineAction<T1, T2, T3>`.
- **Type Parameters:**
    - `T1` — the first argument
    - `T2` — the second argument
    - `T3` — the third argument
- **Parameter:** `action` – the delegate to wrap.
- **Returns:** A new `InlineAction<T1, T2, T3>` containing the specified delegate.