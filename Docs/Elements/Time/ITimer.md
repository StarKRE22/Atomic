# 🧩 ITimer

`ITimer` represents a general-purpose **timer interface** that supports starting, pausing, resuming, stopping, progress tracking, and state change notifications.

The interface combines multiple sources internally:

- [IStartSource](Sources.md/#istartsource) — start / stop control
- [IPauseSource](Sources.md/#ipausesource) — pause / resume control
- [ICompleteSource](Sources.md/#icompletesource) — completion notification
- [IStateSource<TimerState>](Sources.md/#istatesource) — state tracking
- [ITimeSource](Sources.md/#itimesource) — current time tracking
- [IDurationSource](Sources.md/#idurationsource) — total duration
- [IProgressSource](Sources.md/#iprogresssource) — normalized progress
- [ITickSource](Sources.md/#iticksource) — incremental updates

> [!IMPORTANT]  
> Useful for gameplay timers, ability cooldowns, animation timers, and any system requiring precise time management.

---

<details>
  <summary>
    <h2>Events</h2>
  </summary>

<br>

#### `event Action OnStarted`
```csharp
public event Action OnStarted;
```
- **Description:** Invoked when the timer starts running.
- **Remarks:** Triggered whenever `Start()` is called and the timer begins counting.
- **Parameters:** None.

#### `event Action OnStopped`
```csharp
public event Action OnStopped;
```
- **Description:** Invoked when the timer is stopped.
- **Remarks:** Triggered whenever `Stop()` is called. The current time may be reset depending on implementation.
- **Parameters:** None.

#### `event Action OnPaused`
```csharp
public event Action OnPaused;
```
- **Description:** Raised when the timer is paused.
- **Remarks:** Triggered whenever `Pause()` is called. The timer stops progressing until `Resume()` is invoked.
- **Parameters:** None.

#### `event Action OnResumed`
```csharp
public event Action OnResumed;
```
- **Description:** Raised when the timer resumes from a paused state.
- **Remarks:** Triggered whenever `Resume()` is called. The timer continues counting from the paused time.
- **Parameters:** None.

#### `event Action OnCompleted`
```csharp
public event Action OnCompleted;
```
- **Description:** Invoked when the timer finishes (remaining time reaches zero or completes its duration).
- **Remarks:** Triggered once per completion. Can be used to notify gameplay logic that the timer ended.
- **Parameters:** None.

#### `event Action<float> OnTimeChanged`
```csharp
public event Action<float> OnTimeChanged;
```
- **Description:** Invoked whenever the current time changes.
- **Remarks:** Useful to track the countdown or elapsed time.
- **Parameters:**
    - `float` — the current time of the timer in seconds.

#### `event Action<float> OnDurationChanged`
```csharp
public event Action<float> OnDurationChanged;
```
- **Description:** Invoked whenever the total duration changes.
- **Parameters:**
    - `float` — the new total duration in seconds.

#### `event Action<float> OnProgressChanged`
```csharp
public event Action<float> OnProgressChanged;
```
- **Description:** Raised when the normalized progress changes (0–1).
- **Remarks:** Can be used to update UI or trigger game logic based on progress.
- **Parameters:**
    - `float` — the current progress, normalized between 0 and 1.

#### `event Action<TimerState> OnStateChanged`
```csharp
public event Action<TimerState> OnStateChanged;
```
- **Description:** Invoked whenever the timer’s internal state changes.
- **Remarks:** States may include Idle, Running, Paused, Completed depending on `TimerState` enum.
- **Parameters:**
    - `TimerState` — the new state of the timer.

</details>

---

<details>
  <summary>
    <h2>Methods</h2>
  </summary>

<br>

#### `void Start()`
```csharp
public void Start();
```
- **Description:** Starts the timer from its default start time.
- **Remarks:** Triggers `OnStarted` event.

#### `void Start(float time)`
```csharp
public void Start(float time);
```
- **Description:** Starts the timer from a specific time.
- **Parameters:**
    - `time` — starting time in seconds.
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

#### `void SetTime(float time)`
```csharp
public void SetTime(float time);
```
- **Description:** Sets the current timer value.
- **Parameters:**
    - `time` — the new time in seconds.
- **Remarks:** Triggers `OnTimeChanged` and `OnProgressChanged` if value changes.

#### `float GetDuration()`
```csharp
public float GetDuration();
```
- **Description:** Returns the total duration of the timer.
- **Returns:** Duration in seconds.

#### `void SetDuration(float duration)`
```csharp
public void SetDuration(float duration);
```
- **Description:** Sets a new total duration.
- **Parameters:**
    - `duration` — total duration in seconds.
- **Remarks:** Triggers `OnDurationChanged` and `OnProgressChanged`.

#### `float GetProgress()`
```csharp
public float GetProgress();
```
- **Description:** Returns the normalized progress of the timer.
- **Returns:** Value between `0` and `1`.

#### `void SetProgress(float progress)`
```csharp
public void SetProgress(float progress);
```
- **Description:** Sets the normalized progress and updates the current time accordingly.
- **Parameters:**
    - `progress` — normalized value between `0` and `1`.
- **Remarks:** Triggers `OnTimeChanged` and `OnProgressChanged`.

#### `TimerState GetState()`
```csharp
public TimerState GetState();
```
- **Description:** Returns the current internal state of the timer.
- **Returns:** `TimerState` — e.g., Idle, Running, Paused, Completed.
- **Remarks:** Can be used to track state transitions along with `OnStateChanged`.

#### `void Tick(float deltaTime)`
```csharp
public void Tick(float deltaTime);
```
- **Description:** Updates the timer by a specified time increment.
- **Parameters:**
    - `deltaTime` — time in seconds to advance the timer.
- **Remarks:** Triggers `OnTimeChanged`, `OnProgressChanged`, and `OnCompleted` as appropriate.
</details>

---

## 🗂 Example Usage
```csharp
// Create a timer of 30 seconds
ITimer timer = new Timer(30f);

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

## 📌 Best Practice
Choosing Between `ITimer` and `ICooldown`

- `ITimer` is a **more advanced, stateful timer**:
  - Supports **Start, Pause, Resume, Stop**.
  - Exposes **all events**: OnStarted, OnPaused, OnResumed, OnStopped, OnCompleted, OnTimeChanged, OnProgressChanged, OnStateChanged.
  - Suitable for scenarios where the **timer itself is part of game logic**, e.g., timed buffs, game rounds, or special abilities.

- [ICooldown](ICooldown.md) is a **lightweight countdown**:
  - Only tracks remaining time and normalized progress.
  - Best for **simple countdowns**, repeated delays, or ability cooldowns where pausing or manual state control isn’t needed.

**Rule of thumb:**
- Use `Cooldown` for **simple timers**.
- Use `ITimer` for **complex, interactive timers** that need full state and control.
