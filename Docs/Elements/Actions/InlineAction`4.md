# 🧩 InlineAction&lt;T1, T2, T3, T4&gt;

Represents an action <b>with four parameters</b> that can be invoked.

---

## 📑 Table of Contents

- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Constructors](#-constructors)
        - [InlineAction(Action\<T1, T2, T3, T4>)](#inlineactionactiont1-t2-t3-t4)
    - [Methods](#-methods)
        - [Invoke(T1, T2, T3, T4)](#invoket1-t2-t3-t4)
        - [ToString()](#tostring)
    - [Operators](#-operators)
        - [InlineAction(Action\<T1, T2, T3, T4>)](#operator-inlineactiont1-t2-t3-t4actiont1-t2-t3-t4)

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public class InlineAction<T1, T2, T3, T4> : IAction<T1, T2, T3, T4>
```

- **Description:** Represents an action <b>with four parameters</b> that can be invoked.
- **Type parameters**
    - `T1` — the first argument
    - `T2` — the second argument
    - `T3` — the third argument
    - `T4` — the fourth argument
- **Inheritance:** [IAction&lt;T1, T2, T3, T4&gt;](IAction%604.md)
- **Note:** Supports Odin Inspector

---

### 🏗️ Constructors <div id="-constructors"></div>

#### `InlineAction(Action<T1, T2, T3, T4>)`

```csharp
public InlineAction(Action<T1, T2, T3, T4> action)
```

- **Description:** Initializes a new instance with the specified action.
- **Parameter:** `action` – The action to invoke.
- **Throws:** `ArgumentNullException` if `action` is null.

---

### 🏹 Methods

#### `Invoke(T1, T2, T3, T4)`

```csharp
public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
```

- **Description:** Invokes the wrapped action with the specified arguments.
- **Parameters:**
    - `arg1` – The first argument
    - `arg2` – The second argument
    - `arg3` – The third argument
    - `arg4` – The fourth argument

#### `ToString()`

```csharp
public override string ToString();
```

- **Description:** Returns a string that represents the method name of action.
- **Returns:** A string representation of the method name of delegate.

---

### 🪄 Operators

#### `operator InlineAction<T1, T2, T3, T4>(Action<T1, T2, T3, T4>)`

```csharp
public static implicit operator InlineAction<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action);
```

- **Description:** Implicitly converts a delegate of type `Action<T1, T2, T3, T4>` to a `InlineAction<T1, T2, T3, T4>`.
- **Type Parameters:**
    - `T1` — the first argument
    - `T2` — the second argument
    - `T3` — the third argument
    - `T4` — the third argument
- **Parameter:** `action` – the delegate to wrap.
- **Returns:** A new `InlineAction<T1, T2, T3, T4>` containing the specified delegate.