# 🧩 Timer

`Timer` is a **countdown timer** that tracks duration, current time, progress, and state. It implements the [ITimer](ITimer.md) interface and provides full control over **start, pause, resume, stop**, progress updates, and state notifications.

> [!IMPORTANT]  
> Use `Timer` when you need a stateful timer with events and full control over its lifecycle. For simple countdowns, consider [ICooldown](ICooldown.md)`.

---

<details>
  <summary>
    <h2>🔻 Constructors</h2>
  </summary>

#### `Timer()`
```csharp
public Timer();
```
- **Description:** Initializes a new instance of the `Timer` class with default values.
- **Remarks:** Duration defaults to `0` and state is `IDLE`. The timer must be started with `Start()`.

#### `Timer(float duration)`
```csharp
public Timer(float duration);
```
- **Description:** Initializes a new instance of the `Timer` class with a specified duration.
- **Parameters:** `duration` — total duration of the timer in seconds.
- **Remarks:** The timer is in `IDLE` state after construction and must be started with `Start()`.

</details>

---

<details>
  <summary>
    <h2>🔻 Events</h2>
  </summary>

#### `event Action OnStarted`
```csharp
public event Action OnStarted;
```
- **Description:** Invoked when the timer starts.
- **Remarks:** Triggered whenever `Start()` is called.
- **Parameters:** None.

#### `event Action OnStopped`
```csharp
public event Action OnStopped;
```
- **Description:** Invoked when the timer is stopped.
- **Remarks:** Triggered whenever `Stop()` is called. The current time is reset.
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
- **Remarks:** Triggered whenever `Resume()` is called. The timer continues counting from its paused time.
- **Parameters:** None.

#### `event Action OnCompleted`
```csharp
public event Action OnCompleted;
```
- **Description:** Invoked when the timer reaches its duration.
- **Remarks:** Triggered once per completion. Can be used for game logic or notifications.
- **Parameters:** None.

#### `event Action<float> OnTimeChanged`
```csharp
public event Action<float> OnTimeChanged;
```
- **Description:** Raised when the current time changes.
- **Parameters:**
    - `float` — current time in seconds.

#### `event Action<float> OnDurationChanged`
```csharp
public event Action<float> OnDurationChanged;
```
- **Description:** Raised when the total duration changes.
- **Parameters:**
    - `float` — new total duration in seconds.

#### `event Action<float> OnProgressChanged`
```csharp
public event Action<float> OnProgressChanged;
```
- **Description:** Raised when normalized progress changes (0–1).
- **Parameters:**
    - `float` — current progress (0–1).

#### `event Action<TimerState> OnStateChanged`
```csharp
public event Action<TimerState> OnStateChanged;
```
- **Description:** Raised when the timer’s internal state changes.
- **Parameter:** [TimerState](TimerState.md) — new state (Idle, Playing, Paused, Completed).

</details>

---

<details>
  <summary>
    <h2>🔻 Properties</h2>
  </summary>

#### `TimerState CurrentState`
```csharp
public TimerState CurrentState { get; }
```
- **Description:** Gets the current state of the timer.
- **Remarks:** Read-only property reflecting the [timer state](TimerState.md): `IDLE`, `PLAYING`, `PAUSED`, or `COMPLETED`.

#### `float Duration`
```csharp
public float Duration { get; set; }
```
- **Description:** Gets or sets the total duration of the timer in seconds.
- **Remarks:** Setting this property triggers `OnDurationChanged`.

#### `float Time`
```csharp
public float Time { get; set; }
```
- **Description:** Gets or sets the current time of the timer in seconds.
- **Remarks:** Setting this property triggers `OnTimeChanged` and updates progress via `OnProgressChanged`.

#### `float Progress`
```csharp
public float Progress { get; set; }
```
- **Description:** Gets or sets the normalized progress of the timer (0–1).
- **Remarks:** Setting this property updates the current time and triggers `OnTimeChanged` and `OnProgressChanged`.

</details>

---

<details>
  <summary>
    <h2>🔻 Methods</h2>
  </summary>

#### `void Start()`
```csharp
public void Start();
```
- **Description:** Starts the timer from its default start time.
- **Remarks:** Triggers `OnStarted` and sets state to `PLAYING`.

#### `void Start(float time)`
```csharp
public void Start(float time);
```
- **Description:** Starts the timer from a specific time.
- **Parameter:** `time` — starting time in seconds.
- **Remarks:** Triggers `OnStarted` and sets state to `PLAYING`.

#### `void Stop()`
```csharp
public void Stop();
```
- **Description:** Stops the timer and resets the current time.
- **Remarks:** Triggers `OnStopped` and sets state to `IDLE`.

#### `bool IsStarted()`
```csharp
public bool IsStarted();
```
- **Description:** Returns whether the timer is currently running.
- **Returns:** `true` if the timer is running (`PLAYING`); otherwise `false`.

#### `bool IsIdle()`
```csharp
public bool IsIdle();
```
- **Description:** Returns whether the timer has not started yet.
- **Returns:** `true` if the timer is `IDLE`; otherwise `false`.

#### `void Pause()`
```csharp
public void Pause();
```
- **Description:** Pauses the timer.
- **Remarks:** Triggers `OnPaused` and sets state to `PAUSED`.

#### `void Resume()`
```csharp
public void Resume();
```
- **Description:** Resumes the timer from paused state.
- **Remarks:** Triggers `OnResumed` and sets state to `PLAYING`.

#### `bool IsPaused()`
```csharp
public bool IsPaused();
```
- **Description:** Returns whether the timer is currently paused.
- **Returns:** `true` if the timer is `PAUSED`; otherwise `false`.

#### `bool IsCompleted()`
```csharp
public bool IsCompleted();
```
- **Description:** Returns whether the timer has finished counting down.
- **Returns:** `true` if `COMPLETED`; otherwise `false`.

#### `float GetTime()`
```csharp
public float GetTime();
```
- **Description:** Returns the current timer value in seconds.

#### `void SetTime(float time)`
```csharp
public void SetTime(float time);
```
- **Description:** Sets the current timer value (clamped to [0, Duration]).
- **Parameter:** `time` — new timer value in seconds.
- **Remarks:** Triggers `OnTimeChanged` and `OnProgressChanged`.

#### `float GetDuration()`
```csharp
public float GetDuration();
```
- **Description:** Returns the total duration of the timer.

#### `void SetDuration(float duration)`
```csharp
public void SetDuration(float duration);
```
- **Description:** Sets a new total duration.
- **Parameters:** `duration` — new duration in seconds.
- **Remarks:** Triggers `OnDurationChanged` and `OnProgressChanged`.

#### `float GetProgress()`
```csharp
public float GetProgress();
```
- **Description:** Returns normalized progress (0–1).

#### `void SetProgress(float progress)`
```csharp
public void SetProgress(float progress);
```
- **Description:** Sets the normalized progress and updates current time.
- **Parameter:** `progress` — value between 0 and 1.
- **Remarks:** Triggers `OnTimeChanged` and `OnProgressChanged.

#### `TimerState GetState()`
```csharp
public TimerState GetState();
```
- **Description:** Returns the current state of the timer (`IDLE`, `PLAYING`, `PAUSED`, `COMPLETED`).

#### `void Tick(float deltaTime)`
```csharp
public void Tick(float deltaTime);
```
- **Description:** Advances the timer by a specified time increment.
- **Parameter:** `deltaTime` — time in seconds.
- **Remarks:** Triggers `OnTimeChanged`, `OnProgressChanged`, and `OnCompleted` as appropriate.
</details>

---

<details>
  <summary>
    <h2>🔻 Operators</h2>
  </summary>

#### `implicit operator Timer(float duration)`
```csharp
public static implicit operator Timer(float duration);
```
- **Description:** Implicitly converts a `float` value to a `Timer` instance.
- **Parameters:** `duration` — The duration in seconds for the new `Timer`.
- **Returns:** A new `Timer` initialized with the specified duration.
- **Example:**  
  
  ```csharp
  Timer timer = 5f; // creates a Timer with duration = 5 seconds
  ```

#### `implicit operator Timer(int duration)`
```csharp
public static implicit operator Timer(int duration);
```
- **Description:** Implicitly converts an `int` value to a `Timer` instance.
- **Parameters:** `duration` — The duration in seconds for the new `Timer`.
- **Returns:** A new `Timer` initialized with the specified duration.
- **Example:**  
  
  ```csharp
  Timer timer = 3; // creates a Timer with duration = 3 seconds
  ```

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
- Use `ICooldown` for **simple timers**.
- Use `ITimer` for **complex, interactive timers** that need full state and control.
