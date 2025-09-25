# 🧩 IRequest

```csharp
public interface IRequest : IAction
```

- **Description:** Represents a <b>parameterless</b> request action.
- **Inheritance:** [IAction](../Actions/IAction.md)

---

## 🔑 Properties

#### `Required`

```csharp
public bool Required { get; }
```

- **Description:** Indicates whether the request must be handled.

---

## 🏹 Methods

#### `Invoke()`

```csharp
public void Invoke();
```

- **Description:** Executes the request.
- **Note:** This method derived from `IAction`

#### `Consume()`

```csharp
public bool Consume();
```

- **Description:** Attempts to consume the request.
- **Returns:** `true` if successfully consumed; otherwise `false`.