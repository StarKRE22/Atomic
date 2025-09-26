# 🧩 IPeriod

```csharp
public interface IPeriod : 
    IStartSource,
    IPauseSource,
    IStateSource<PeriodState>,
    ITimeSource,
    IProgressSource,
    IDurationSource,
    ITickSource;
```

- **Description:** Represents a **looping cycle timer interface** that supports starting, pausing, resuming, progress
  tracking, state
  change notifications, and emits an event each time the period completes.

- **Inheritance:** [IStartSource](IStartSource.md),  [IPauseSource](IPauseSource.md), [IStateSource](IStateSource.md), 
  [ITimeSource](ITimeSource.md), [IDurationSource](IDurationSource.md), [IProgressSource](IProgressSource.md),
  [ITickSource](ITickSource.md).
- **Notes:** [PeriodState](PeriodState.md) represents current state of a period
---

## ⚡ Events

#### `OnPeriod`

```csharp
public event Action OnPeriod;
```

- **Description:** Invoked each time a period completes and automatically restarts.
- **Remarks:** Can be used to trigger repeating game logic or system events.

#### `OnStarted`

```csharp
public event Action OnStarted;
```

- **Description:** Invoked when the period timer starts running.
- **Remarks:** Triggered whenever `Start()` is called and the timer begins counting.

#### `OnPaused`

```csharp
public event Action OnPaused;
```

- **Description:** Raised when the period timer is paused.
- **Remarks:** Triggered whenever `Pause()` is called.

#### `OnResumed`

```csharp
public event Action OnResumed;
```

- **Description:** Raised when the period timer resumes from a paused state.

#### `OnTimeChanged`

```csharp
public event Action<float> OnTimeChanged;
```

- **Description:** Invoked whenever the current time of the period changes.
- **Parameter:** `float` — the current time in seconds.

#### `OnProgressChanged`

```csharp
public event Action<float> OnProgressChanged;
```

- **Description:** Raised when normalized progress changes (0–1).
- **Parameter:** `float` — current progress, normalized between 0 and 1.

#### `OnDurationChanged`

```csharp
public event Action<float> OnDurationChanged;
```

- **Description:** Invoked whenever the total duration of the period changes.
- **Parameter:** `float` — new period duration in seconds.

#### `OnStateChanged`

```csharp
public event Action<PeriodState> OnStateChanged;
```

- **Description:** Invoked whenever the period’s internal state changes.
- **Parameters:** [PeriodState](PeriodState.md) — the new state of the period.

---

## 🏹 Methods

#### `void Start()`

```csharp
public void Start();
```

- **Description:** Starts the period from the beginning.
- **Remarks:** Triggers `OnStarted` event.

#### `void Stop()`

```csharp
public void Stop();
```

- **Description:** Stops the period and resets the current time.
- **Remarks:** Triggers `OnStateChanged`.

#### `IsStarted()`

```csharp
public bool IsStarted();
```

- **Description:** Returns whether the period timer is currently running.
- **Returns:** `true` if running; otherwise `false`.

#### `Pause()`

```csharp
public void Pause();
```

- **Description:** Pauses the period timer.
- **Remarks:** Triggers `OnPaused`.

#### `Resume()`

```csharp
public void Resume();
```

- **Description:** Resumes the period timer from paused state.
- **Remarks:** Triggers `OnResumed`.

#### `IsPaused()`

```csharp
public bool IsPaused();
```

- **Description:** Returns whether the period timer is currently paused.
- **Returns:** `true` if paused; otherwise `false`.

#### `GetTime()`

```csharp
public float GetTime();
```

- **Description:** Returns the current time of the period.
- **Returns:** Time in seconds.

#### `SetTime(float)`

```csharp
public void SetTime(float time);
```

- **Description:** Sets the current time of the period.
- **Remarks:** Triggers `OnTimeChanged` and `OnProgressChanged`.

#### `GetDuration()`

```csharp
public float GetDuration();
```

- **Description:** Returns the duration of each period.
- **Returns:** Duration in seconds.

#### `SetDuration(float)`

```csharp
public void SetDuration(float duration);
```

- **Description:** Sets a new period duration.
- **Remarks:** Triggers `OnDurationChanged` and `OnProgressChanged`.

#### `GetProgress()`

```csharp
public float GetProgress();
```

- **Description:** Returns the normalized progress of the period.
- **Returns:** Value between 0 and 1.

#### `SetProgress(float)`

```csharp
public void SetProgress(float progress);
```

- **Description:** Sets normalized progress and updates current time accordingly.
- **Remarks:** Triggers `OnTimeChanged` and `OnProgressChanged`.

#### `GetState()`

```csharp
public PeriodState GetState();
```

- **Description:** Returns the current state of the period.
- **Returns:** [PeriodState](PeriodState.md) — e.g., `IDLE`, `PLAYING`, `PAUSED`.
- **Remarks:** Can be used along with `OnStateChanged` for state tracking.

#### `Tick(float)`

```csharp
public void Tick(float deltaTime);
```

- **Description:** Updates the period by a specified time increment.
- **Parameter:** `deltaTime` — time in seconds to advance the period.
- **Remarks:** Triggers `OnTimeChanged`, `OnProgressChanged`, and `OnPeriod` when a cycle completes.