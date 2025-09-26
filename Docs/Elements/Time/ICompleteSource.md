# 🧩 ICompleteSource

```csharp
public interface ICompleteSource
```

- **Description:** Represents a source that <b>can complete and notify listeners</b>.

---

## ⚡ Events

#### `OnCompleted`

```csharp
public event Action OnCompleted;  
```

- **Description:** Invoked when the source has completed.

---

## 🏹 Methods

#### `IsCompleted()`

```csharp
public bool IsCompleted();  
```

- **Description:** Returns whether the source has completed.
- **Returns:** `true` if completed; otherwise `false`.