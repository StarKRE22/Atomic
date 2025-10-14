# 🧩 IStopwatch

Represents a **stopwatch interface** that supports starting, pausing, resuming, stopping, and
tracking **elapsed time**.
It also provides **state change notifications** and incremental time updates.

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
            <li><a href="#onstarted">OnStarted</a></li>
            <li><a href="#onstopped">OnStopped</a></li>
            <li><a href="#onpaused">OnPaused</a></li>
            <li><a href="#onresumed">OnResumed</a></li>
            <li><a href="#ontimechanged">OnTimeChanged</a></li>
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
            <li><a href="#pause">Pause()</a></li>
            <li><a href="#resume">Resume()</a></li>
            <li><a href="#isstarted">IsStarted()</a></li>
            <li><a href="#ispaused">IsPaused()</a></li>
            <li><a href="#getstate">GetState()</a></li>
            <li><a href="#gettime">GetTime()</a></li>
            <li><a href="#settimefloat">SetTime(float)</a></li>
            <li><a href="#resettime">ResetTime(float)</a></li>
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
// Assume we have an instance of stopwatch
IStopwatch stopwatch = ...;

// Subscribe to events
stopwatch.OnStarted += () => Console.WriteLine("Stopwatch started!");
stopwatch.OnTimeChanged += t => Console.WriteLine($"Elapsed: {t:F1}s");
stopwatch.OnPaused += () => Console.WriteLine("Stopwatch paused.");
stopwatch.OnResumed += () => Console.WriteLine("Stopwatch resumed.");
stopwatch.OnStopped += () => Console.WriteLine("Stopwatch stopped.");

// Start the stopwatch
stopwatch.Start();

// Simulate ticking
float deltaTime = 1f;
for (int i = 0; i < 5; i++)
{
    stopwatch.Tick(deltaTime);
    System.Threading.Thread.Sleep(1000);
}

// Pause and resume
stopwatch.Pause();
stopwatch.Resume();

// Stop and reset
stopwatch.Stop();
stopwatch.ResetTime();
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface IStopwatch :
    IStartSource,
    IPauseSource,
    ITimeSource,
    IStateSource<StopwatchState>,
    ITickSource
```

- **Inheritance:** [IStartSource](IStartSource.md), [IPauseSource](IPauseSource.md), [ITimeSource](ITimeSource.md),
- [IStateSource](IStateSource.md), [ITickSource](ITickSource.md).
- **Notes:** [StopwatchState](StopwatchState.md) represents current state of a stopwatch

---

### ⚡ Events

#### `OnStarted`

```csharp
public event Action OnStarted;
```

- **Description:** Invoked when the stopwatch starts.
- **Remarks:** Triggered whenever `Start()` is called.

#### `OnStopped`

```csharp
public event Action OnStopped;
```

- **Description:** Invoked when the stopwatch is stopped.
- **Remarks:** Triggered whenever `Stop()` is called. Elapsed time resets depending on implementation.

#### `OnPaused`

```csharp
public event Action OnPaused;
```

- **Description:** Raised when the stopwatch is paused.
- **Remarks:** Triggered whenever `Pause()` is called. Elapsed time stops updating until resumed.

#### `OnResumed`

```csharp
public event Action OnResumed;
```

- **Description:** Raised when the stopwatch resumes from a paused state.
- **Remarks:** Triggered whenever `Resume()` is called. Stopwatch continues counting from its paused time.

#### `OnTimeChanged`

```csharp
public event Action<float> OnTimeChanged;
```

- **Description:** Raised whenever elapsed time changes.
- **Parameter:** `float` — elapsed time in seconds.

#### `OnStateChanged`

```csharp
public event Action<StopwatchState> OnStateChanged;
```

- **Description:** Raised when the stopwatch state changes.
- **Parameter:** [StopwatchState](StopwatchState.md) — new state (`IDLE`, `RUNNING`, `PAUSED`).

---

### 🏹 Methods

#### `Start()`

```csharp
public void Start();
```

- **Description:** Starts measuring elapsed time.
- **Remarks:** Triggers `OnStarted`.

#### `Stop()`

```csharp
public void Stop();
```

- **Description:** Stops measuring and resets elapsed time.
- **Remarks:** Triggers `OnStopped`.

#### `Pause()`

```csharp
public void Pause();
```

- **Description:** Pauses the stopwatch.
- **Remarks:** Triggers `OnPaused`.

#### `Resume()`

```csharp
public void Resume();
```

- **Description:** Resumes from paused state.
- **Remarks:** Triggers `OnResumed`.

#### `IsStarted()`

```csharp
public bool IsStarted();
```

- **Description:** Returns whether the stopwatch is currently running.
- **Returns:** `true` if running; otherwise `false`.

#### `IsPaused()`

```csharp
public bool IsPaused();
```

- **Description:** Returns whether the stopwatch is currently paused.
- **Returns:** `true` if paused; otherwise `false`.

#### `GetState()`

```csharp
public StopwatchState GetState();
```

- **Description:** Returns the current state of the stopwatch.
- **Returns:** [StopwatchState](StopwatchState.md) value.

#### `GetTime()`

```csharp
public float GetTime();
```

- **Description:** Returns the elapsed time in seconds.
- **Returns:** Elapsed time.

#### `SetTime(float)`

```csharp
public void SetTime(float time);
```

- **Description:** Sets the elapsed time manually.
- **Parameter:** `time` — new elapsed time in seconds.
- **Remarks:** Triggers `OnTimeChanged`.

#### `ResetTime()`

```csharp
public void ResetTime();  
```

- **Description:** Resets the stopwatch to its initial state.
- **Remarks:** After resetting, the current time will be the initial time, and any listeners may be notified via
  `OnTimeChanged`.

#### `Tick(float)`

```csharp
public void Tick(float deltaTime);
```

- **Description:** Advances the stopwatch by a given time increment.
- **Parameter:** `deltaTime` — time in seconds.
- **Remarks:** Triggers `OnTimeChanged` as time progresses.