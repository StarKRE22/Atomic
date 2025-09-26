# 🧩 ITimer

```csharp
public interface ITimer :
    IStartSource,
    IPauseSource,
    ICompleteSource,
    IStateSource<TimerState>,
    ITimeSource,
    IDurationSource,
    IProgressSource,
    ITickSource
```

- **Description:** Represents a general-purpose **timer interface** that supports starting, pausing, resuming, stopping,
  progress tracking, and state change notifications.

- **Inheritance:** [IStartSource](IStartSource.md), [IPauseSource](IPauseSource.md),
  [ICompleteSource](ICompleteSource.md), [IStateSource](IStateSource.md), [ITimeSource](ITimeSource.md),
  [IDurationSource](IDurationSource.md), [IProgressSource](IProgressSource.md), [ITickSource](ITickSource.md).

- **Note:** [TimerState](TimerState.md) represents current state of the timer.

> [!TIP]
> Useful for gameplay timers, ability cooldowns, animation timers, and any system requiring precise time management.

---

## ⚡ Events

#### `OnStarted`

```csharp
public event Action OnStarted;
```

- **Description:** Invoked when the timer starts running.
- **Remarks:** Triggered whenever `Start()` is called and the timer begins counting.

#### `OnStopped`

```csharp
public event Action OnStopped;
```

- **Description:** Invoked when the timer is stopped.
- **Remarks:** Triggered whenever `Stop()` is called. The current time may be reset depending on implementation.

#### `OnPaused`

```csharp
public event Action OnPaused;
```

- **Description:** Raised when the timer is paused.
- **Remarks:** Triggered whenever `Pause()` is called. The timer stops progressing until `Resume()` is invoked.

#### `OnResumed`

```csharp
public event Action OnResumed;
```

- **Description:** Raised when the timer resumes from a paused state.
- **Remarks:** Triggered whenever `Resume()` is called. The timer continues counting from the paused time.

#### `OnCompleted`

```csharp
public event Action OnCompleted;
```

- **Description:** Invoked when the timer finishes (remaining time reaches zero or completes its duration).
- **Remarks:** Triggered once per completion. Can be used to notify gameplay logic that the timer ended.

#### `OnTimeChanged`

```csharp
public event Action<float> OnTimeChanged;
```

- **Description:** Invoked whenever the current time changes.
- **Remarks:** Useful to track the countdown or elapsed time.
- **Parameter:** `float` — the current time of the timer in seconds.

#### `OnDurationChanged`

```csharp
public event Action<float> OnDurationChanged;
```

- **Description:** Invoked whenever the total duration changes.
- **Parameter:** `float` — the new total duration in seconds.

#### `OnProgressChanged`

```csharp
public event Action<float> OnProgressChanged;
```

- **Description:** Raised when the normalized progress changes (0–1).
- **Remarks:** Can be used to update UI or trigger game logic based on progress.
- **Parameter:** `float` — the current progress, normalized between 0 and 1.

#### `OnStateChanged`

```csharp
public event Action<TimerState> OnStateChanged;
```

- **Description:** Invoked whenever the timer’s internal state changes.
- **Remarks:** States may include Idle, Playing, Paused, Completed depending on `TimerState` enum.
- **Parameter:** [TimerState](TimerState.md) — the new state of the timer.

---

## 🏹 Methods

#### `void Start()`

```csharp
public void Start();
```

- **Description:** Starts the timer from its default start time.
- **Remarks:** Triggers `OnStarted` event.

#### `void Start(float)`

```csharp
public void Start(float time);
```

- **Description:** Starts the timer from a specific time.
- **Parameter:** `time` — starting time in seconds.
- **Remarks:** Triggers `OnStarted` event.

#### `void Stop()`

```csharp
public void Stop();
```

- **Description:** Stops the timer and resets the current time.
- **Remarks:** Triggers `OnStopped` event.

#### `bool IsStarted()`

```csharp
public bool IsStarted();
```

- **Description:** Returns whether the timer is currently running.
- **Returns:** `true` if the timer is running; otherwise `false`.

#### `bool IsIdle()`

```csharp
public bool IsIdle();
```

- **Description:** Returns whether the timer has not started yet.
- **Returns:** `true` if idle; otherwise `false`.

#### `void Pause()`

```csharp
public void Pause();
```

- **Description:** Pauses the timer.
- **Remarks:** Triggers `OnPaused` event.

#### `void Resume()`

```csharp
public void Resume();
```

- **Description:** Resumes the timer from paused state.
- **Remarks:** Triggers `OnResumed` event.

#### `bool IsPaused()`

```csharp
public bool IsPaused();
```

- **Description:** Returns whether the timer is currently paused.
- **Returns:** `true` if paused; otherwise `false`.

#### `bool IsCompleted()`

```csharp
public bool IsCompleted();
```

- **Description:** Returns whether the timer has finished.
- **Returns:** `true` if completed; otherwise `false`.
- **Remarks:** Completion triggers `OnCompleted` event.

#### `float GetTime()`

```csharp
public float GetTime();
```

- **Description:** Returns the current timer value.
- **Returns:** Current time in seconds.

#### `void SetTime(float)`

```csharp
public void SetTime(float time);
```

- **Description:** Sets the current timer value.
- **Parameter:** `time` — the new time in seconds.
- **Remarks:** Triggers `OnTimeChanged` and `OnProgressChanged` if value changes.

#### `float GetDuration()`

```csharp
public float GetDuration();
```

- **Description:** Returns the total duration of the timer.
- **Returns:** Duration in seconds.

#### `void SetDuration(float)`

```csharp
public void SetDuration(float duration);
```

- **Description:** Sets a new total duration.
- **Parameter:** `duration` — total duration in seconds.
- **Remarks:** Triggers `OnDurationChanged` and `OnProgressChanged`.

#### `float GetProgress()`

```csharp
public float GetProgress();
```

- **Description:** Returns the normalized progress of the timer.
- **Returns:** Value between `0` and `1`.

#### `void SetProgress(float)`

```csharp
public void SetProgress(float progress);
```

- **Description:** Sets the normalized progress and updates the current time accordingly.
- **Parameter:** `progress` — normalized value between `0` and `1`.
- **Remarks:** Triggers `OnTimeChanged` and `OnProgressChanged`.

#### `TimerState GetState()`

```csharp
public TimerState GetState();
```

- **Description:** Returns the current internal state of the timer.
- **Returns:** [TimerState](TimerState.md) — e.g., Idle, Playing, Paused, Completed.
- **Remarks:** Can be used to track state transitions along with `OnStateChanged`.

#### `void Tick(float)`

```csharp
public void Tick(float deltaTime);
```

- **Description:** Updates the timer by a specified time increment.
- **Parameter:** `deltaTime` — time in seconds to advance the timer.
- **Remarks:** Triggers `OnTimeChanged`, `OnProgressChanged`, and `OnCompleted` as appropriate.