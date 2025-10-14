# 🧩 Stopwatch

Represents a **stateful stopwatch timer** that tracks elapsed time and supports **start, pause,
resume, stop**, time updates, and state notifications. It provides a simple way to
track elapsed time in gameplay, animations, or any time-dependent system.

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
          <summary><a href="#-properties">Properties</a></summary>
          <ul>
            <li><a href="#state">State</a></li>
            <li><a href="#time">Time</a></li>
          </ul>
        </details>
      </li>
      <li>
        <details>
          <summary><a href="#-methods">Methods</a></summary>
          <ul>
            <li><a href="#start">Start()</a></li>
            <li><a href="#startfloat">Start(float)</a></li>
            <li><a href="#pause">Pause()</a></li>
            <li><a href="#resume">Resume()</a></li>
            <li><a href="#stop">Stop()</a></li>
            <li><a href="#isstarted">IsStarted()</a></li>
            <li><a href="#ispaused">IsPaused()</a></li>
            <li><a href="#isidle">IsIdle()</a></li>
            <li><a href="#gettime">GetTime()</a></li>
            <li><a href="#settimefloat">SetTime(float)</a></li>
            <li><a href="#getstate">GetState()</a></li>
            <li><a href="#tickfloat">Tick(float)</a></li>
            <li><a href="#resettime">ResetTime()</a></li>
          </ul>
        </details>
      </li>
    </ul>
  </li>
</ul>


---

## 🗂 Example of Usage

```csharp  
// Create a stopwatch
var stopwatch = new Stopwatch();

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
[Serializable]
public class Stopwatch : IStopwatch
```

- **Inheritance:** [IStopwatch](IStopwatch.md)
- **Notes:**
    - [StopwatchState](StopwatchState.md) represents current state of a stopwatch
    - Supports Unity serialization and Odin Inspector

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

- **Description:** Invoked when the stopwatch stops.
- **Remarks:** Triggered whenever `Stop()` is called.

#### `OnPaused`

```csharp
public event Action OnPaused;  
```

- **Description:** Invoked when the stopwatch is paused.
- **Remarks:** Triggered whenever `Pause()` is called.

#### `OnResumed`

```csharp
public event Action OnResumed;  
```

- **Description:** Invoked when the stopwatch resumes from paused state.
- **Remarks:** Triggered whenever `Resume()` is called.

#### `OnTimeChanged`

```csharp  
public event Action<float> OnTimeChanged;  
```

- **Description:** Raised when the current elapsed time changes.
- **Parameter:** `float` — elapsed time in seconds.

#### `OnStateChanged`

```csharp  
public event Action<StopwatchState> OnStateChanged;  
```

- **Description:** Raised whenever the stopwatch state changes.
- **Parameters:** [StopwatchState](StopwatchState.md) — current state (`IDLE`, `PLAYING`, `PAUSED`).

---

### 🔑 Properties

#### `State`

```csharp  
public StopwatchState State { get; }  
```

- **Description:** Gets the current state of the stopwatch (`IDLE`, `PLAYING`, `PAUSED`).
- **Remarks:** Read-only; use `GetState()` to query state programmatically.

#### `Time`

```csharp 
public float Time { get; set; }  
```

- **Description:** Gets or sets the current elapsed time.
- **Remarks:** Setting triggers `OnTimeChanged`.

---

### 🏹 Methods

#### `Start()`

```csharp  
public void Start();  
```

- **Description:** Starts the stopwatch from `0` seconds.
- **Remarks:** Triggers `OnStarted` and sets state to `PLAYING`.

#### `Start(float)`

```csharp  
public void Start(float time);  
```

- **Description:** Starts the stopwatch from a specified elapsed time.
- **Parameter:** `time` — starting elapsed time in seconds.
- **Remarks:** Triggers `OnStarted` and sets state to `PLAYING`.

#### `Pause()`

```csharp  
public void Pause();  
```

- **Description:** Pauses the stopwatch.
- **Remarks:** Triggers `OnPaused` and sets state to `PAUSED`.

#### `Resume()`

```csharp  
public void Resume();  
```

- **Description:** Resumes the stopwatch from paused state.
- **Remarks:** Triggers `OnResumed` and sets state to `PLAYING`.

#### `Stop()`

```csharp  
public void Stop();  
```

- **Description:** Stops the stopwatch and resets elapsed time to `0`.
- **Remarks:** Triggers `OnStopped` and sets state to `IDLE`.

#### `IsStarted()`

```csharp  
public bool IsStarted();  
```

- **Description:** Returns whether the stopwatch is currently running.
- **Returns:** `true` if `PLAYING`; otherwise `false`.

#### `IsPaused()`

```csharp  
public bool IsPaused();  
```

- **Description:** Returns whether the stopwatch is currently paused.
- **Returns:** `true` if `PAUSED`; otherwise `false`.

#### `IsIdle()`

```csharp  
public bool IsIdle();  
```

- **Description:** Returns whether the stopwatch is idle.
- **Returns:** `true` if `IDLE`; otherwise `false`.

#### `GetTime()`

```csharp  
public float GetTime();  
```

- **Description:** Returns the current elapsed time.
- **Returns:** Time in seconds.

#### `SetTime(float)`

```csharp  
public void SetTime(float time);  
```

- **Description:** Sets the current elapsed time.
- **Parameter:** `time` — new elapsed time in seconds (>=0).
- **Remarks:** Triggers `OnTimeChanged` if value changes.

#### `GetState()`

```csharp  
public StopwatchState GetState();  
```

- **Description:** Returns the current state of the stopwatch.

#### `Tick(float)`

```csharp  
public void Tick(float deltaTime);  
```

- **Description:** Advances the stopwatch by a time increment.
- **Parameter:** `deltaTime` — seconds to add to elapsed time.
- **Remarks:** Only works in `PLAYING` state; triggers `OnTimeChanged`.

#### `ResetTime()`

```csharp  
public void ResetTime();  
```

- **Description:** Resets the elapsed time to `0`.
- **Remarks:** Equivalent to `SetTime(0)`.