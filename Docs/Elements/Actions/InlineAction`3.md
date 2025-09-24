
<details>
  <summary>
    <h2>🧩 InlineAction&lt;T1, T2, T3&gt;</h2>
    <br> Represents an action <b>with three parameters</b> that can be invoked.
  </summary>

<br>

```csharp
public class InlineAction<T1, T2, T3> : IAction<T1, T2, T3>
```

- **Description:** Represents an action with three parameters that can be invoked.
- **Type parameters**
    - `T1` — the first argument
    - `T2` — the second argument
    - `T3` — the third argument

---

### 🏗️ Constructors

#### `InlineAction(Action<T1, T2, T3> action)`

```csharp
public InlineAction(Action<T1, T2, T3> action)
```

- **Description:** Initializes a new instance with the specified action.
- **Parameter:** `action` – The action to invoke.
- **Throws:** `ArgumentNullException` if `action` is null.

---

### 🏹 Methods

#### `Invoke(T1 arg1, T2 arg2, T3 arg3)`

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

---

### 🗂 Example of Usage

```csharp
var moveResourcesAction = new InlineAction<Storage, Storage, int>((source, destination, amount) => 
{
    source.SpendResources(amount);
    destination.EarnResources(amount);
});

moveResourcesAction.Invoke(storageA, storageB, 100);
```

</details>