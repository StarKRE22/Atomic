
<details>
  <summary>
    <h2 id="icompletesource">🧩 ICompleteSource</h2>
    <br> Represents a source that <b>can complete and notify listeners</b>.
  </summary>

<br>

```csharp
public interface ICompleteSource
```

---

### ⚡ Events

#### `OnCompleted`

```csharp
public event Action OnCompleted;  
```

- **Description:** Invoked when the source has completed.

---

### 🏹 Methods

#### `IsCompleted()`

```csharp
public bool IsCompleted();  
```

- **Description:** Returns whether the source has completed.
- **Returns:** `true` if completed; otherwise `false`.

</details>