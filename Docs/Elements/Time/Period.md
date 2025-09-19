# 🧩 Period

Represents a **stateful looping cycle timer** that tracks time progression and emits events on completion of each cycle.
It implements the [IPeriod](IPeriod.md) interface and provides full control over **start, pause, resume, stop**,
progress tracking, duration management, and state notifications.

> [!IMPORTANT]  
> Use `Period` when you need **repeating timers or cycle-based events**, such as gameplay loops, animations, or periodic
> system updates. Unlike [ITimer](ITimer.md), `Period` automatically loops on completion.

```csharp
public class Period : IPeriod
```

---

## 🏗️ Constructors

#### `Period()`

```csharp
public Period();
```

- **Description:** Initializes a new instance of `Period` with default duration `0`.
- **Usage:** Creates an empty period to configure later.

#### `Period(float)`

```csharp
public Period(float duration);
```

- **Description:** Initializes a new instance with a specified duration in seconds.
- **Parameter:** `duration` — total duration of the cycle.
- **Usage:** Creates a period ready to start with a given cycle length.

---

## ⚡ Events

#### `OnStarted`

```csharp
public event Action OnStarted;
```

- **Description:** Raised when the period timer starts.
- **Remarks:** Triggered whenever `Start()` is called.

#### `OnStopped`

```csharp
public event Action OnStopped;
```

- **Description:** Raised when the period timer stops.
- **Remarks:** Triggered whenever `Stop()` is called.

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

- **Description:** Raised when the period timer resumes from pause.
- **Remarks:** Triggered whenever `Resume()` is called.

#### `OnPeriod`

```csharp
public event Action OnPeriod;
```

- **Description:** Raised each time a cycle completes.
- **Remarks:** Automatically invoked when `Time` reaches `Duration`.

#### `OnStateChanged`

```csharp
public event Action<PeriodState> OnStateChanged;
```

- **Description:** Raised whenever the period's internal state changes.
- **Parameters:** [PeriodState](PeriodState.md) — current state (`IDLE`, `PLAYING`, `PAUSED`).

#### `OnTimeChanged`

```csharp
public event Action<float> OnTimeChanged;
```

- **Description:** Raised whenever the current cycle time changes.
- **Parameter:** `float` — current time in seconds.

#### `OnProgressChanged`

```csharp
public event Action<float> OnProgressChanged;
```

- **Description:** Raised whenever the progress of the current cycle changes.
- **Parameter:** `float` — normalized progress (0–1).

#### `OnDurationChanged`

```csharp
public event Action<float> OnDurationChanged;
```

- **Description:** Raised whenever the duration of the cycle changes.
- **Parameter:** `float` — new cycle duration in seconds.

---

## 🔑 Properties

#### `State`

```csharp
public PeriodState State { get; }
```

- **Description:** Current state of the period (`IDLE`, `PLAYING`, `PAUSED`).
- **Remarks:** Read-only; state changes are triggered by `Start()`, `Pause()`, `Resume()`, or `Stop()`.

#### `Duration`

```csharp
public float Duration { get; set; }
```

- **Description:** Total duration of one cycle.
- **Remarks:** Setting triggers `OnDurationChanged`.

#### `Time`

```csharp
public float Time { get; set; }
```

- **Description:** Current time within the cycle.
- **Remarks:** Setting triggers `OnTimeChanged` and updates `Progress`.

#### `Progress`

```csharp
public float Progress { get; set; }
```

- **Description:** Normalized progress of the current cycle (0–1).
- **Remarks:** Setting updates `Time` and triggers `OnProgressChanged`.

---

## 🏹 Methods

#### `Start()`

```csharp
public void Start();
```

- **Description:** Starts the period timer from 0 seconds.
- **Remarks:** Sets state to `PLAYING` and triggers `OnStarted`.

#### `Start(float)`

```csharp
public void Start(float time);
```

- **Description:** Starts the timer from a specific time.
- **Parameter:** `time` — starting time in seconds.
- **Remarks:** Sets state to `PLAYING` and triggers `OnStarted`.

#### `Pause()`

```csharp
public void Pause();
```

- **Description:** Pauses the period timer.
- **Remarks:** Sets state to `PAUSED` and triggers `OnPaused`.

#### `Resume()`

```csharp
public void Resume();
```

- **Description:** Resumes the period timer from pause.
- **Remarks:** Sets state to `PLAYING` and triggers `OnResumed`.

#### `Stop()`

```csharp
public void Stop();
```

- **Description:** Stops the period timer and resets time.
- **Remarks:** Sets state to `IDLE` and triggers `OnStopped`.

#### `IsStarted()`

```csharp
public bool IsStarted();
```

- **Description:** Returns whether the period is currently playing.
- **Returns:** `true` if `PLAYING`; otherwise `false`.

#### `IsPaused()`

```csharp
public bool IsPaused();
```

- **Description:** Returns whether the period is paused.
- **Returns:** `true` if `PAUSED`; otherwise `false`.

#### `IsIdle()`

```csharp
public bool IsIdle();
```

- **Description:** Returns whether the period is idle.
- **Returns:** `true` if `IDLE`; otherwise `false`.

#### `GetTime()`

```csharp
public float GetTime();
```

- **Description:** Returns the current cycle time.

#### `SetTime(float)`

```csharp
public void SetTime(float time);
```

- **Description:** Sets the current cycle time.
- **Remarks:** Clamped to `[0, Duration]` and triggers `OnTimeChanged`.

#### `GetDuration()`

```csharp
public float GetDuration();
```

- **Description:** Returns the duration of one cycle.

#### `SetDuration(float)`

```csharp
public void SetDuration(float duration);
```

- **Description:** Sets a new cycle duration.
- **Remarks:** Triggers `OnDurationChanged`.

#### `GetProgress()`

```csharp
public float GetProgress();
```

- **Description:** Returns the current progress (0–1) of the cycle.

#### `SetProgress(float)`

```csharp
public void SetProgress(float progress);
```

- **Description:** Sets the progress (`0–1`) and updates `Time`.
- **Remarks:** Triggers `OnTimeChanged` and `OnProgressChanged`.

#### `GetState()`

```csharp
public PeriodState GetState();
```

- **Description:** Returns the current state of the period.

#### `Tick(float)`

```csharp
public void Tick(float deltaTime);
```

- **Description:** Advances the timer by a time increment.
- **Parameter:** `deltaTime` — time in seconds.
- **Remarks:** Only works in `PLAYING` state; triggers `OnTimeChanged`, `OnProgressChanged`, and `OnPeriod` when a cycle
  completes.

#### `ResetTime()`

```csharp
public void ResetTime();
```

- **Description:** Resets the current cycle time to `0`.

---

## 🪄 Operators

#### `operator Period(float duration)`

```csharp
public static implicit operator Period(float duration);
```

- **Description:** Allows creating a `Period` directly from a float.
- **Usage Example:**

  ```csharp
  Period timer = 5f; // creates a Period with duration = 5 seconds
  ```

#### `operator Period(int duration)`

```csharp
public static implicit operator Period(int duration);
```

- **Description:** Allows creating a `Period` directly from an integer.
- **Usage Example:**

  ```csharp
  Period timer = 3; // creates a Period with duration = 3 seconds
  ```

---

## 🗂 Example of Usage

```csharp
IPeriod period = new Period(10f);

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