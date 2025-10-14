# 🧩 IPeriod

Represents a **looping cycle timer interface** that supports starting, pausing, resuming, progress
tracking, state change notifications, and emits an event each time the period completes.

---

## 📑 Table of Contents

<ul>
  <li><a href="#-example-of-usage">Example of Usage</a></li>
  <li>
    <a href="#-api-reference">API Reference</a>
    <ul>
      <li><a href="#-type">Type</a></li>
      <li>
        <details>
          <summary><a href="#-events">Events</a></summary>
          <ul>
            <li><a href="#onperiod">OnPeriod</a></li>
            <li><a href="#onstarted">OnStarted</a></li>
            <li><a href="#onpaused">OnPaused</a></li>
            <li><a href="#onresumed">OnResumed</a></li>
            <li><a href="#ontimechanged">OnTimeChanged</a></li>
            <li><a href="#onprogresschanged">OnProgressChanged</a></li>
            <li><a href="#ondurationchanged">OnDurationChanged</a></li>
            <li><a href="#onstatechanged">OnStateChanged</a></li>
          </ul>
        </details>
      </li>
      <li>
        <details>
          <summary><a href="#-methods">Methods</a></summary>
          <ul>
            <li><a href="#start">Start()</a></li>
            <li><a href="#stop">Stop()</a></li>
            <li><a href="#isstarted">IsStarted()</a></li>
            <li><a href="#pause">Pause()</a></li>
            <li><a href="#resume">Resume()</a></li>
            <li><a href="#ispaused">IsPaused()</a></li>
            <li><a href="#gettime">GetTime()</a></li>
            <li><a href="#settime">SetTime(float)</a></li>
            <li><a href="#resettime">ResetTime()</a></li>
            <li><a href="#getduration">GetDuration()</a></li>
            <li><a href="#setduration">SetDuration(float)</a></li>
            <li><a href="#getprogress">GetProgress()</a></li>
            <li><a href="#setprogress">SetProgress(float)</a></li>
            <li><a href="#getstate">GetState()</a></li>
            <li><a href="#tickfloat">Tick(float)</a></li>
          </ul>
        </details>
      </li>
    </ul>
  </li>
</ul>


---

## 🗂 Example of Usage

```csharp
// Assume we have an IPeriod instance
IPeriod period = ...;

// Subscribe to events
period.OnStarted += () => Console.WriteLine("Period started!");
period.OnTimeChanged += t => Console.WriteLine($"Time: {t:F1}s");
period.OnProgressChanged += p => Console.WriteLine($"Progress: {p:P0}");
period.OnPeriod += () => Console.WriteLine("Cycle completed!");
period.OnPaused += () => Console.WriteLine("Period paused.");
period.OnResumed += () => Console.WriteLine("Period resumed.");
period.OnStopped += () => Console.WriteLine("Period stopped.");

// Start the period
period.Start();

// Simulate ticking 1 second per loop
float deltaTime = 1f;
for (int i = 0; i < 25; i++)
{
    period.Tick(deltaTime);
    System.Threading.Thread.Sleep(1000);
}

// Pause and resume
period.Pause();
period.Resume();

// Stop and reset
period.Stop();
period.ResetTime();
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

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

- **Inheritance:** [IStartSource](IStartSource.md),  [IPauseSource](IPauseSource.md), [IStateSource](IStateSource.md),
  [ITimeSource](ITimeSource.md), [IDurationSource](IDurationSource.md), [IProgressSource](IProgressSource.md),
  [ITickSource](ITickSource.md).
- **Notes:** [PeriodState](PeriodState.md) represents current state of a period

---

### ⚡ Events

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

### 🏹 Methods

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

#### `ResetTime()`

```csharp
public void ResetTime();  
```

- **Description:** Resets the time source to its initial state.
- **Remarks:** After resetting, the current time will be the initial time, and any listeners may be notified via
  `OnTimeChanged`.

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