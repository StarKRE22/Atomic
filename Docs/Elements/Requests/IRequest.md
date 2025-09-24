
---

<details>
  <summary>
    <h2>🧩 IRequest</h2>
    <br> Represents a <b>parameterless</b> request action.
  </summary>

<br>

```csharp
public interface IRequest : IAction
```

---

### 🔑 Properties

#### `Required`

```csharp
public bool Required { get; }
```

- **Description:** Indicates whether the request must be handled.

---

### 🏹 Methods

#### `Invoke()`

```csharp
public void Invoke();
```

- **Description:** Executes the request.
- **Note:** This method derived from [IAction.Invoke()](../Actions/IAction.md#invoke)

#### `Consume()`

```csharp
public bool Consume();
```

- **Description:** Attempts to consume the request.
- **Returns:** `true` if successfully consumed; otherwise `false`.

</details>
