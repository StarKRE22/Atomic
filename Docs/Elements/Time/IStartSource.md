
<details>
  <summary>
    <h2 id="istartsource">🧩 IStartSource</h2>
    <br> Represents a source that <b>can be started, stopped, and notify start/stop events</b>.
  </summary>

<br>

```csharp
public interface IStartSource
```

---

### ⚡ Events

#### `OnStarted`

```csharp
public event Action OnStarted;  
```

- **Description:** Raised when the source starts.

#### `OnStopped`

```csharp
public event Action OnStopped;  
```

- **Description:** Raised when the source stops.

---

### 🏹 Methods

#### `IsIdle()`

```csharp
public bool IsIdle();  
```

- **Description:** Returns `true` if the source has not started yet.

#### `IsStarted()`

```csharp
public bool IsStarted();  
```

- **Description:** Returns `true` if the source is running.

#### `Start(float)`

```csharp
public void Start(float time);  
```

- **Description:** Starts the source from a specific time.
- **Parameter:** `time` — Time (in seconds) to start from.

#### `Start()`

```csharp
public void Start();  
```

- **Description:** Starts the source from the default start time.

#### `Stop()`

```csharp
public void Stop();  
```

- **Description:** Stops the source and resets its time.

</details>