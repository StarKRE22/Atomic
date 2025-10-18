# 🧩 DownTimer

Represents a **countdown timer** that tracks duration, remaining time, progress, and state.
It provides full control over **start, pause, resume, stop**, progress updates, and state notifications. Use `DownTimer`
when you need a stateful timer that counts down and broadcasts progress and state
changes. For simple timers or repeated delays, consider [ICooldown](ICooldown.md).


---

## 📑 Table of Contents

<ul>
  <li><a href="#-example-of-usage">Example of Usage</a></li>
  <li><a href="#-inspector-settings">Inspector Settings</a></li>
  <li>
    <a href="#-api-reference">API Reference</a>
    <ul>
      <li><a href="#-type">Type</a></li>
      <li>
        <details>
          <summary><a href="#-constructors">Constructors</a></summary>
          <ul>
            <li><a href="#downtimer">DownTimer()</a></li>
            <li><a href="#downtimerfloat">DownTimer(float)</a></li>
          </ul>
        </details>
      </li>
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
          <summary><a href="#-properties">Properties</a></summary>
          <ul>
            <li><a href="#currentstate">CurrentState</a></li>
            <li><a href="#duration">Duration</a></li>
            <li><a href="#time">Time</a></li>
            <li><a href="#progress">Progress</a></li>
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
            <li><a href="#isidle">IsIdle()</a></li>
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
      <li>
        <details>
          <summary><a href="#-operators">Operators</a></summary>
          <ul>
            <li><a href="#operator-downtimerfloat">DownTimer(float)</a></li>
            <li><a href="#operator-downtimerint">DownTimer(int)</a></li>
          </ul>
        </details>
      </li>
    </ul>
  </li>
</ul>


---

## 🗂 Example of Usage

```csharp
DownTimer timer = 30;

// Subscribe to events
timer.OnStarted += () => Console.WriteLine("Countdown started!");
timer.OnTimeChanged += t => Console.WriteLine($"Time remaining: {t:F1}s");
timer.OnProgressChanged += p => Console.WriteLine($"Progress: {p:P0}");
timer.OnCompleted += () => Console.WriteLine("Countdown completed!");

// 1. Start the countdown
timer.Start(); // must call Start before ticking

// 2. Tick the countdown (simulate time passing)
float deltaTime = 1f;
while (!timer.IsCompleted())
{
    timer.Tick(deltaTime);
    System.Threading.Thread.Sleep(1000);
}

// 3. Pause and resume
timer.Pause();
Console.WriteLine("Countdown paused...");
timer.Resume();

// 4. Stop the countdown
timer.Stop();
Console.WriteLine("Countdown stopped!");

// 5. Reset or manually set time/progress
timer.SetTime(15f);        // set remaining time to 15 seconds
timer.SetProgress(0.5f);   // set progress to 50%
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
[Serializable]
public class DownTimer : ITimer
```

- **Inheritance:** [ITimer](ITimer.md)
- **Notes:**
    - [TimerState](TimerState.md) represents current state of the timer
    - Supports Unity serialization and Odin Inspector

---

## 🛠 Inspector Settings

| Parameter  | Description                      |
|------------|----------------------------------|
| `duration` | The total duration of the timer. |

---

<div id="-constructors"></div>

### 🏗️ Constructors

#### `DownTimer()`

```csharp
public DownTimer();
```

- **Description:** Initializes a new instance of the `DownTimer` class with default values.
- **Remarks:** Duration defaults to `0` and state is `IDLE`. The timer must be started with `Start()`.

#### `DownTimer(float)`

```csharp
public DownTimer(float duration);
```

- **Description:** Initializes a new instance of the `DownTimer` class with a specified duration.
- **Parameters:** `duration` — total duration of the countdown in seconds.
- **Remarks:** The timer is in `IDLE` state after construction and must be started with `Start()`.

---

### ⚡ Events

#### `OnStarted`

```csharp
public event Action OnStarted;
```

- **Description:** Invoked when the countdown starts.
- **Remarks:** Triggered whenever `Start()` is called.

#### `OnStopped`

```csharp
public event Action OnStopped;
```

- **Description:** Invoked when the countdown is stopped.
- **Remarks:** Triggered whenever `Stop()` is called. Current time is reset.

#### `OnPaused`

```csharp
public event Action OnPaused;
```

- **Description:** Raised when the countdown is paused.
- **Remarks:** Triggered whenever `Pause()` is called. Countdown stops until `Resume()` is called.

#### `OnResumed`

```csharp
public event Action OnResumed;
```

- **Description:** Raised when the countdown resumes from a paused state.
- **Remarks:** Triggered whenever `Resume()` is called.

#### `OnCompleted`

```csharp
public event Action OnCompleted;
```

- **Description:** Invoked when the countdown reaches zero.
- **Remarks:** Triggered once per completion. Can be used for game logic or notifications.

#### `OnTimeChanged`

```csharp
public event Action<float> OnTimeChanged;
```

- **Description:** Raised when the remaining time changes.
- **Parameter:** `float` — current remaining time in seconds.

#### `OnDurationChanged`

```csharp
public event Action<float> OnDurationChanged;
```

- **Description:** Raised when the total duration changes.
- **Parameter:** `float` — new total duration in seconds.

#### `OnProgressChanged`

```csharp
public event Action<float> OnProgressChanged;
```

- **Description:** Raised when normalized progress changes (0–1).
- **Parameter:** `float` — current progress (0–1).

#### `OnStateChanged`

```csharp
public event Action<TimerState> OnStateChanged;
```

- **Description:** Raised when the timer’s internal state changes.
- **Parameter:** [TimerState](TimerState.md) — new state (`IDLE`, `PLAYING`, `PAUSED`, `COMPLETED`).

---

### 🔑 Properties

#### `State`

```csharp
public TimerState State { get; }
```

- **Description:** Gets the current state of the countdown.
- **Remarks:** Read-only property reflecting the [TimerState](TimerState.md).

#### `Duration`

```csharp
public float Duration { get; set; }
```

- **Description:** Gets or sets the total duration of the countdown in seconds.
- **Remarks:** Setting this property triggers `OnDurationChanged`.

#### `CurrentTime`

```csharp
public float CurrentTime { get; set; }
```

- **Description:** Gets or sets the current remaining time.
- **Remarks:** Setting this property triggers `OnTimeChanged` and updates progress via `OnProgressChanged`.

#### `Progress`

```csharp
public float Progress { get; set; }
```

- **Description:** Gets or sets the normalized progress of the countdown (0–1).
- **Remarks:** Setting this property updates the current time and triggers `OnTimeChanged` and `OnProgressChanged`.

---

### 🏹 Methods

#### `Start()`

```csharp
public void Start();
```

- **Description:** Starts the countdown from its full duration.
- **Remarks:** Triggers `OnStarted` and sets state to `PLAYING`.

#### `Start(float)`

```csharp
public void Start(float time);
```

- **Description:** Starts the countdown from a specific time.
- **Parameter:** `time` — starting time in seconds.
- **Remarks:** Triggers `OnStarted` and sets state to `PLAYING`.

#### `Stop()`

```csharp
public void Stop();
```

- **Description:** Stops the countdown and resets the current time to zero.
- **Remarks:** Triggers `OnStopped` and sets state to `IDLE`.

#### `Pause()`

```csharp
public void Pause();
```

- **Description:** Pauses the countdown.
- **Remarks:** Triggers `OnPaused` and sets state to `PAUSED`.

#### `Resume()`

```csharp
public void Resume();
```

- **Description:** Resumes the countdown from paused state.
- **Remarks:** Triggers `OnResumed` and sets state to `PLAYING`.

#### `IsIdle()`

```csharp
public bool IsIdle();
```

- **Description:** Returns whether the countdown has not started.
- **Returns:** `true` if `IDLE`; otherwise `false`.

#### `IsStarted()`

```csharp
public bool IsStarted();
```

- **Description:** Returns whether the countdown is running.
- **Returns:** `true` if `PLAYING`; otherwise `false`.

#### `IsPaused()`

```csharp
public bool IsPaused();
```

- **Description:** Returns whether the countdown is paused.
- **Returns:** `true` if `PAUSED`; otherwise `false`.

#### `IsCompleted()`

```csharp
public bool IsCompleted();
```

- **Description:** Returns whether the countdown has finished.
- **Returns:** `true` if `COMPLETED`; otherwise `false`.

#### `GetTime()`

```csharp
public float GetTime();
```

- **Description:** Returns the current remaining time in seconds.

#### `SetTime(float)`

```csharp
public void SetTime(float time);
```

- **Description:** Sets the current remaining time (clamped to [0, Duration]).
- **Parameter:** `time` — new time in seconds.
- **Remarks:** Triggers `OnTimeChanged` and `OnProgressChanged`.

#### `ResetTime()`

```csharp
public void ResetTime();  
```

- **Description:** Resets the current time to the full duration.
- **Remarks:** Triggers `OnTimeChanged` and `OnProgressChanged`.

#### `GetDuration()`

```csharp
public float GetDuration();
```

- **Description:** Returns the total duration of the countdown.

#### `SetDuration(float)`

```csharp
public void SetDuration(float duration);
```

- **Description:** Sets a new total duration.
- **Parameter:** `duration` — new duration in seconds.
- **Remarks:** Triggers `OnDurationChanged` and `OnProgressChanged`.

#### `GetProgress()`

```csharp
public float GetProgress();
```

- **Description:** Returns normalized progress (0–1).

#### `SetProgress(float)`

```csharp
public void SetProgress(float progress);
```

- **Description:** Sets the normalized progress and updates current time.
- **Parameter:** `progress` — value between 0 and 1.
- **Remarks:** Triggers `OnTimeChanged` and `OnProgressChanged`.

#### `GetState()`

```csharp
public TimerState GetState();
```

- **Description:** Returns the current state of the countdown (`IDLE`, `PLAYING`, `PAUSED`, `COMPLETED`).

#### `Tick(float)`

```csharp
public void Tick(float deltaTime);
```

- **Description:** Advances the countdown by a specified time increment.
- **Parameter:** `deltaTime` — time in seconds.
- **Remarks:** Updates current time, progress, and triggers `OnCompleted` if countdown reaches zero.

---

### 🪄 Operators

#### `operator DownTimer(float)`

```csharp
public static implicit operator DownTimer(float duration);
```

- **Description:** Implicitly converts a `float` value to a `DownTimer` instance.
- **Parameters:** `duration` — duration in seconds for the new countdown.
- **Returns:** A new `DownTimer` initialized with the specified duration.

#### `operator DownTimer(int)`

```csharp
public static implicit operator DownTimer(int duration);
```

- **Description:** Implicitly converts an `int` value to a `DownTimer` instance.
- **Parameters:** `duration` — duration in seconds for the new countdown.
- **Returns:** A new `DownTimer` initialized with the specified duration.