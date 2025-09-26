
<details>
  <summary>
    <h2 id="ipausesource">🧩 IPauseSource</h2>
    <br> Represents a source that <b>can be paused and resumed</b>.
  </summary>

<br>

```csharp
public interface IPauseSource
```

---

### ⚡ Events

#### `OnPaused`

```csharp
public event Action OnPaused;  
```

- **Description:** Raised when the source is paused.

#### `OnResumed`

```csharp
public event Action OnResumed;  
```

- **Description:** Raised when the source is resumed.

---

### 🏹 Methods

#### `IsPaused()`

```csharp
public bool IsPaused();  
```

- **Description:** Returns true if the source is paused.
- **Returns:** `true` if paused; otherwise `false`.

#### `Pause()`

```csharp
public void Pause();  
```

- **Description:** Pauses the source.

#### `Resume()`

```csharp
public void Resume();  
```

- **Description:** Resumes the source.

</details>