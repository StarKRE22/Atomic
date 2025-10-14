# 🧩 ITimer

Represents a general-purpose **timer interface** that supports starting, pausing, resuming, stopping,
progress tracking, and state change notifications. Useful for gameplay timers, ability cooldowns, animation timers, and
any system requiring precise time management.

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
            <li><a href="#oncompleted">OnCompleted</a></li>
            <li><a href="#ontimechanged">OnTimeChanged</a></li>
            <li><a href="#ondurationchanged">OnDurationChanged</a></li>
            <li><a href="#onprogresschanged">OnProgressChanged</a></li>
            <li><a href="#onstatechanged">OnStateChanged</a></li>
          </ul>
        </details>
      </li>
      <li>
        <details>
          <summary><a href="#-methods">Methods</a></summary>
          <ul>
            <li><a href="#start">Start()</a></li>
            <li><a href="#startfloat">Start(float)</a></li>
            <li><a href="#stop">Stop()</a></li>
            <li><a href="#isstarted">IsStarted()</a></li>
            <li><a href="#pause">Pause()</a></li>
            <li><a href="#resume">Resume()</a></li>
            <li><a href="#ispaused">IsPaused()</a></li>
            <li><a href="#iscompleted">IsCompleted()</a></li>
            <li><a href="#gettime">GetTime()</a></li>
            <li><a href="#settimefloat">SetTime(float)</a></li>
            <li><a href="#resettime">ResetTime()</a></li>
            <li><a href="#getduration">GetDuration()</a></li>
            <li><a href="#setdurationfloat">SetDuration(float)</a></li>
            <li><a href="#getprogress">GetProgress()</a></li>
            <li><a href="#setprogressfloat">SetProgress(float)</a></li>
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
//Assume we have an ITimer instance
ITimer timer = ...;

// Subscribe to events
timer.OnStarted += () => Console.WriteLine("Timer started!");
timer.OnTimeChanged += t => Console.WriteLine($"Time remaining: {t:F1}s");
timer.OnProgressChanged += p => Console.WriteLine($"Progress: {p:P0}");
timer.OnCompleted += () => Console.WriteLine("Timer completed!");

// 1. Start the timer
timer.Start(); // must call Start before ticking

// 2. Tick the timer (simulate time passing, e.g., 1 second per tick)
float deltaTime = 1f;
while (!timer.IsCompleted())
{
    timer.Tick(deltaTime);
    System.Threading.Thread.Sleep(1000); // wait 1 second (simulation)
}

// 3. After completion, you can restart the timer
if (timer.IsCompleted())
{
    Console.WriteLine("Restarting timer...");
    timer.Start();
}

// 4. Pause and resume (optional)
timer.Pause();
Console.WriteLine("Timer paused...");
timer.Resume();

// 5. Stop the timer (optional)
timer.Stop();
Console.WriteLine("Timer stopped!");

// 6. Reset or manually set time/progress (optional)
timer.SetTime(15f);        // set remaining time to 15 seconds
timer.SetProgress(0.5f);   // set progress to 50%
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

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

- **Inheritance:** [IStartSource](IStartSource.md), [IPauseSource](IPauseSource.md),
  [ICompleteSource](ICompleteSource.md), [IStateSource](IStateSource.md), [ITimeSource](ITimeSource.md),
  [IDurationSource](IDurationSource.md), [IProgressSource](IProgressSource.md), [ITickSource](ITickSource.md).

- **Note:** [TimerState](TimerState.md) represents current state of the timer.

---

### ⚡ Events

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

### 🏹 Methods

#### `Start()`

```csharp
public void Start();
```

- **Description:** Starts the timer from its default start time.
- **Remarks:** Triggers `OnStarted` event.

#### `Start(float)`

```csharp
public void Start(float time);
```

- **Description:** Starts the timer from a specific time.
- **Parameter:** `time` — starting time in seconds.
- **Remarks:** Triggers `OnStarted` event.

#### `Stop()`

```csharp
public void Stop();
```

- **Description:** Stops the timer and resets the current time.
- **Remarks:** Triggers `OnStopped` event.

#### `IsStarted()`

```csharp
public bool IsStarted();
```

- **Description:** Returns whether the timer is currently running.
- **Returns:** `true` if the timer is running; otherwise `false`.

#### `Pause()`

```csharp
public void Pause();
```

- **Description:** Pauses the timer.
- **Remarks:** Triggers `OnPaused` event.

#### `Resume()`

```csharp
public void Resume();
```

- **Description:** Resumes the timer from paused state.
- **Remarks:** Triggers `OnResumed` event.

#### `IsPaused()`

```csharp
public bool IsPaused();
```

- **Description:** Returns whether the timer is currently paused.
- **Returns:** `true` if paused; otherwise `false`.

#### `IsCompleted()`

```csharp
public bool IsCompleted();
```

- **Description:** Returns whether the timer has finished.
- **Returns:** `true` if completed; otherwise `false`.
- **Remarks:** Completion triggers `OnCompleted` event.

#### `GetTime()`

```csharp
public float GetTime();
```

- **Description:** Returns the current timer value.
- **Returns:** Current time in seconds.

#### `SetTime(float)`

```csharp
public void SetTime(float time);
```

- **Description:** Sets the current timer value.
- **Parameter:** `time` — the new time in seconds.
- **Remarks:** Triggers `OnTimeChanged` and `OnProgressChanged` if value changes.

#### `ResetTime()`

```csharp
public void ResetTime();  
```

- **Description:** Resets the timer to its initial state.
- **Remarks:** After resetting, the current time will be the initial time, and any listeners may be notified via
  `OnTimeChanged`.

#### `GetDuration()`

```csharp
public float GetDuration();
```

- **Description:** Returns the total duration of the timer.
- **Returns:** Duration in seconds.

#### `SetDuration(float)`

```csharp
public void SetDuration(float duration);
```

- **Description:** Sets a new total duration.
- **Parameter:** `duration` — total duration in seconds.
- **Remarks:** Triggers `OnDurationChanged` and `OnProgressChanged`.

#### `GetProgress()`

```csharp
public float GetProgress();
```

- **Description:** Returns the normalized progress of the timer.
- **Returns:** Value between `0` and `1`.

#### `SetProgress(float)`

```csharp
public void SetProgress(float progress);
```

- **Description:** Sets the normalized progress and updates the current time accordingly.
- **Parameter:** `progress` — normalized value between `0` and `1`.
- **Remarks:** Triggers `OnTimeChanged` and `OnProgressChanged`.

#### `GetState()`

```csharp
public TimerState GetState();
```

- **Description:** Returns the current internal state of the timer.
- **Returns:** [TimerState](TimerState.md) — e.g., Idle, Playing, Paused, Completed.
- **Remarks:** Can be used to track state transitions along with `OnStateChanged`.

#### `Tick(float)`

```csharp
public void Tick(float deltaTime);
```

- **Description:** Updates the timer by a specified time increment.
- **Parameter:** `deltaTime` — time in seconds to advance the timer.
- **Remarks:** Triggers `OnTimeChanged`, `OnProgressChanged`, and `OnCompleted` as appropriate.