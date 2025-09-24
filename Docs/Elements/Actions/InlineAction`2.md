# 🧩 InlineAction&lt;T1, T2&gt;

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

## 🏗️ Constructors

#### `InlineAction(Action<T1, T2>)`

```csharp
public InlineAction(Action<T1, T2> action)
```

- **Description:** Initializes a new instance with the specified action.
- **Parameter:** `action` – The action to invoke.
- **Throws:** `ArgumentNullException` if `action` is null.

---

## 🏹 Methods

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

## 🪄 Operators

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

---

## 🗂 Example of Usage

```csharp
IAction<Character, int> damageAction = new InlineAction<Character, int>(
    (character, damage) => character.TakeDamage(damage)
);

damageAction.Invoke(enemy, 5);
```

