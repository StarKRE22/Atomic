# 🧩 DownTimer

`DownTimer` is a **countdown timer** that tracks duration, remaining time, progress, and state. It implements the [ITimer](ITimer.md) interface and provides full control over **start, pause, resume, stop**, progress updates, and state notifications.

> [!IMPORTANT]  
> Use `DownTimer` when you need a stateful timer that counts down and broadcasts progress and state changes. For simple timers or repeated delays, consider [ICooldown](ICooldown.md).

---

## Constructors

#### `DownTimer()`
```csharp
public DownTimer();
```
- **Description:** Initializes a new instance of the `DownTimer` class with default values.
- **Remarks:** Duration defaults to `0` and state is `IDLE`. The timer must be started with `Start()`.

#### `DownTimer(float duration)`
```csharp
public DownTimer(float duration);
```
- **Description:** Initializes a new instance of the `DownTimer` class with a specified duration.
- **Parameters:** `duration` — total duration of the countdown in seconds.
- **Remarks:** The timer is in `IDLE` state after construction and must be started with `Start()`.

---

## Events

#### `event Action OnStarted`
```csharp
public event Action OnStarted;
```
- **Description:** Invoked when the countdown starts.
- **Remarks:** Triggered whenever `Start()` is called.

#### `event Action OnStopped`
```csharp
public event Action OnStopped;
```
- **Description:** Invoked when the countdown is stopped.
- **Remarks:** Triggered whenever `Stop()` is called. Current time is reset.

#### `event Action OnPaused`
```csharp
public event Action OnPaused;
```
- **Description:** Raised when the countdown is paused.
- **Remarks:** Triggered whenever `Pause()` is called. Countdown stops until `Resume()` is called.

#### `event Action OnResumed`
```csharp
public event Action OnResumed;
```
- **Description:** Raised when the countdown resumes from a paused state.
- **Remarks:** Triggered whenever `Resume()` is called.

#### `event Action OnCompleted`
```csharp
public event Action OnCompleted;
```
- **Description:** Invoked when the countdown reaches zero.
- **Remarks:** Triggered once per completion. Can be used for game logic or notifications.

#### `event Action<float> OnTimeChanged`
```csharp
public event Action<float> OnTimeChanged;
```
- **Description:** Raised when the remaining time changes.
- **Parameter:** `float` — current remaining time in seconds.

#### `event Action<float> OnDurationChanged`
```csharp
public event Action<float> OnDurationChanged;
```
- **Description:** Raised when the total duration changes.
- **Parameter:** `float` — new total duration in seconds.

#### `event Action<float> OnProgressChanged`
```csharp
public event Action<float> OnProgressChanged;
```
- **Description:** Raised when normalized progress changes (0–1).
- **Parameter:** `float` — current progress (0–1).

#### `event Action<TimerState> OnStateChanged`
```csharp
public event Action<TimerState> OnStateChanged;
```
- **Description:** Raised when the timer’s internal state changes.
- **Parameter:** [TimerState](TimerState.md) — new state (`IDLE`, `PLAYING`, `PAUSED`, `COMPLETED`).

---

## Properties

#### `State`
```csharp
public TimerState State { get; }
```
- **Description:** Gets the current state of the countdown.
- **Remarks:** Read-only property reflecting the [timer state](TimerState.md).

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

## Methods

#### `void Start()`
```csharp
public void Start();
```
- **Description:** Starts the countdown from its full duration.
- **Remarks:** Triggers `OnStarted` and sets state to `PLAYING`.

#### `void Start(float time)`
```csharp
public void Start(float time);
```
- **Description:** Starts the countdown from a specific time.
- **Parameter:** `time` — starting time in seconds.
- **Remarks:** Triggers `OnStarted` and sets state to `PLAYING`.

#### `void Stop()`
```csharp
public void Stop();
```
- **Description:** Stops the countdown and resets the current time to zero.
- **Remarks:** Triggers `OnStopped` and sets state to `IDLE`.

#### `void Pause()`
```csharp
public void Pause();
```
- **Description:** Pauses the countdown.
- **Remarks:** Triggers `OnPaused` and sets state to `PAUSED`.

#### `void Resume()`
```csharp
public void Resume();
```
- **Description:** Resumes the countdown from paused state.
- **Remarks:** Triggers `OnResumed` and sets state to `PLAYING`.

#### `bool IsIdle()`
```csharp
public bool IsIdle();
```
- **Description:** Returns whether the countdown has not started.
- **Returns:** `true` if `IDLE`; otherwise `false`.

#### `bool IsStarted()`
```csharp
public bool IsStarted();
```
- **Description:** Returns whether the countdown is running.
- **Returns:** `true` if `PLAYING`; otherwise `false`.

#### `bool IsPaused()`
```csharp
public bool IsPaused();
```
- **Description:** Returns whether the countdown is paused.
- **Returns:** `true` if `PAUSED`; otherwise `false`.

#### `bool IsCompleted()`
```csharp
public bool IsCompleted();
```
- **Description:** Returns whether the countdown has finished.
- **Returns:** `true` if `COMPLETED`; otherwise `false`.

#### `float GetTime()`
```csharp
public float GetTime();
```
- **Description:** Returns the current remaining time in seconds.

#### `void SetTime(float time)`
```csharp
public void SetTime(float time);
```
- **Description:** Sets the current remaining time (clamped to [0, Duration]).
- **Parameter:** `time` — new time in seconds.
- **Remarks:** Triggers `OnTimeChanged` and `OnProgressChanged`.

#### `float GetDuration()`
```csharp
public float GetDuration();
```
- **Description:** Returns the total duration of the countdown.

#### `void SetDuration(float duration)`
```csharp
public void SetDuration(float duration);
```
- **Description:** Sets a new total duration.
- **Parameter:** `duration` — new duration in seconds.
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
- **Remarks:** Triggers `OnTimeChanged` and `OnProgressChanged`.

#### `TimerState GetState()`
```csharp
public TimerState GetState();
```
- **Description:** Returns the current state of the countdown (`IDLE`, `PLAYING`, `PAUSED`, `COMPLETED`).

#### `void Tick(float deltaTime)`
```csharp
public void Tick(float deltaTime);
```
- **Description:** Advances the countdown by a specified time increment.
- **Parameter:** `deltaTime` — time in seconds.
- **Remarks:** Updates current time, progress, and triggers `OnCompleted` if countdown reaches zero.

---

## Operators

#### `implicit operator DownTimer(float duration)`
```csharp
public static implicit operator DownTimer(float duration);
```
- **Description:** Implicitly converts a `float` value to a `DownTimer` instance.
- **Parameters:** `duration` — duration in seconds for the new countdown.
- **Returns:** A new `DownTimer` initialized with the specified duration.
- **Example:**
  
  ```csharp
  DownTimer timer = 5f; // creates a DownTimer with duration = 5 seconds
  ```

#### `implicit operator DownTimer(int duration)`
```csharp
public static implicit operator DownTimer(int duration);
```
- **Description:** Implicitly converts an `int` value to a `DownTimer` instance.
- **Parameters:** `duration` — duration in seconds for the new countdown.
- **Returns:** A new `DownTimer` initialized with the specified duration.
- **Example:**
  
  ```csharp
  DownTimer timer = 3; // creates a DownTimer with duration = 3 seconds
  ```

---

## 🗂 Example of Usage
```csharp
ITimer timer = new DownTimer(30f);

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