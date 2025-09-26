# 🧩 IPauseSource

```csharp
public interface IPauseSource
```

- **Description:**  Represents a source that <b>can be paused and resumed</b>.

---

## ⚡ Events

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

## 🏹 Methods

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