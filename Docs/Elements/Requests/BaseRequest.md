# 🧩 BaseRequest

```csharp
public class BaseRequest : IRequest
```

- **Description:** Represents a <b>parameterless</b> request action.
- **Inheritance:** [IRequest](IRequest.md)
- **Note:** Supports Odin Inspector

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

#### `Consume()`

```csharp
public bool Consume();
```

- **Description:** Attempts to consume the request.
- **Returns:** `true` if successfully consumed; otherwise `false`.