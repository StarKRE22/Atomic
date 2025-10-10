# 🧩 InlineAction&lt;T1, T2&gt;

Represents an action <b>with two parameters</b> that can be invoked.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Constructors](#-constructors)
        - [InlineAction(Action\<T1, T2>)](#inlineactionactiont1-t2)
    - [Methods](#-methods)
        - [Invoke(T1, T2)](#invoket1-t2)
        - [ToString()](#tostring)
    - [Operators](#-operators)
        - [InlineAction(Action\<T1, T2>)](#operator-inlineactiont1-t2actiont1-t2)

---

## 🗂 Example of Usage

Below is an example of using inline action for damage dealing to some character:

```csharp
IAction<Character, int> damageAction = new InlineAction<Character, int>(
    (character, damage) => character.TakeDamage(damage)
);

damageAction.Invoke(enemy, 5);
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public class InlineAction<T1, T2> : IAction<T1, T2>
```

- **Description:** Represents an action <b>with two parameters</b> that can be invoked.
- **Type parameters**
    - `T1` — the first argument
    - `T2` — the second argument
- **Inheritance:** [IAction&lt;T1, T2&gt;](IAction%602.md)
- **Note:** Supports Odin Inspector

---

### 🏗️ Constructors <div id="-constructors"></div>

#### `InlineAction(Action<T1, T2>)`

```csharp
public InlineAction(Action<T1, T2> action)
```

- **Description:** Initializes a new instance with the specified action.
- **Parameter:** `action` – The action to invoke.
- **Throws:** `ArgumentNullException` if `action` is null.

---

### 🏹 Methods

#### `Invoke(T1, T2)`

```csharp
public void Invoke(T1 arg1, T2 arg2)
```

- **Description:** Invokes the wrapped action with the specified arguments.
- **Parameters:**
    - `arg1` – The first argument
    - `arg2` – The second argument

#### `ToString()`

```csharp
public override string ToString();
```

- **Description:** Returns a string that represents the method name of action.
- **Returns:** A string representation of the method name of delegate.

---

### 🪄 Operators

#### `operator InlineAction<T1, T2>(Action<T1, T2>)`

```csharp
public static implicit operator InlineAction<T1, T2>(Action<T1, T2> action);
```

- **Description:** Implicitly converts a delegate of type `Action<T1, T2>` to a `InlineAction<T1, T2>`.
- **Type Parameters:**
    - `T1` — the first argument
    - `T2` — the second argument
- **Parameter:** `action` – the delegate to wrap.
- **Returns:** A new `InlineAction<T1, T2>` containing the specified delegate.