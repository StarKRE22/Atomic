
<details>
  <summary>
    <h2 id="iprogresssource">🧩 IProgressSource</h2>
    <br> Represents a source that <b>tracks progress (0–1) and notifies listeners</b>.
  </summary>

<br>

```csharp
public interface IProgressSource
```

---

### ⚡ Events

#### `OnProgressChanged`

```csharp
public event Action<float> OnProgressChanged;  
```

- **Description:** Raised when the progress changes.

---

### 🏹 Methods

#### `GetProgress()`

```csharp
public float GetProgress();  
```

- **Description:** Gets the current progress.
- **Returns:** Normalized progress (0–1).

#### `SetProgress(float)`

```csharp
public void SetProgress(float progress);  
```

- **Description:** Sets the current progress.
- **Parameter:** `progress` — Progress value (0–1).

</details>