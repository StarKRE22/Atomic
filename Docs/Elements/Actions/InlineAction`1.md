# 🧩 InlineAction&lt;T&gt;

```csharp
public class InlineAction<T> : IAction<T>
```

- **Description:** Represents an action <b>with one parameter</b> that can be invoked.
- **Type parameter** `T` — the input parameter
- **Inheritance:** [IAction&lt;T&gt;](IAction%601.md)
- **Note:** Supports Odin Inspector

---

## 🏗️ Constructors

#### `InlineAction(Action<T>)`

```csharp
public InlineAction(Action<T> action)
```

- **Description:** Initializes a new instance with the specified action.
- **Parameter:** `action` – The action to invoke.
- **Throws:** `ArgumentNullException` if `action` is null.

---

## 🏹 Methods

#### `Invoke(T)`

```csharp
public void Invoke(T arg)
```

- **Description:** Invokes the wrapped action with the specified argument.
- **Parameter:** `arg` – The argument to pass to the action.

#### `ToString()`

```csharp
public override string ToString();
```

- **Description:** Returns a string that represents the method name of action.
- **Returns:** A string representation of the method name of delegate.

---

## 🪄 Operators

#### `operator InlineAction<T>(Action<T>)`

```csharp
public static implicit operator InlineAction<T>(Action<T> action);
```

- **Description:** Implicitly converts a delegate of type `Action<T>` to a `InlineAction<T>`.
- **Type Parameter:** `T` — input parameter.
- **Parameter:** `action` – the delegate to wrap.
- **Returns:** A new `InlineAction<T>` containing the specified delegate.

### 🗂 Example of Usage

```csharp
IAction destroyAction = new InlineAction<GameObject>(GameObject.Destroy);
destroyAction.Invoke(gameObject);
```

