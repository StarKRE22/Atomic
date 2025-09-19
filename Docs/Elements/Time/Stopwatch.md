# 🧩 Stopwatch

`Stopwatch` is a **stateful stopwatch timer** that tracks elapsed time and supports **start, pause, resume, stop**, time updates, and state notifications.  

It implements the [IStopwatch](IStopwatch.md) interface and provides a simple way to track elapsed time in gameplay, animations, or any time-dependent system.

> [!IMPORTANT]  
> Use `IStopwatch` when you need to **measure elapsed time** (e.g., performance tracking, gameplay session time, speedrun timers). Unlike [ITimer](ITimer.md), a stopwatch does not count down toward a duration — it only measures how long something has been running.

---

## Events

#### `event Action OnStarted`
```csharp
public event Action OnStarted;  
```
- **Description:** Invoked when the stopwatch starts.
- **Remarks:** Triggered whenever `Start()` is called.

#### `event Action OnStopped`
```csharp
public event Action OnStopped;  
```
- **Description:** Invoked when the stopwatch stops.
- **Remarks:** Triggered whenever `Stop()` is called.

#### `event Action OnPaused`
```csharp
public event Action OnPaused;  
```
- **Description:** Invoked when the stopwatch is paused.
- **Remarks:** Triggered whenever `Pause()` is called.

#### `event Action OnResumed`
```csharp
public event Action OnResumed;  
```
- **Description:** Invoked when the stopwatch resumes from paused state.
- **Remarks:** Triggered whenever `Resume()` is called.

#### `event Action<float> OnTimeChanged`
```csharp  
public event Action<float> OnTimeChanged;  
```
- **Description:** Raised when the current elapsed time changes.
- **Parameter:** `float` — elapsed time in seconds.

#### `event Action<StopwatchState> OnStateChanged`
```csharp  
public event Action<StopwatchState> OnStateChanged;  
```
- **Description:** Raised whenever the stopwatch state changes.
- **Parameters:** [StopwatchState](StopwatchState.md) — current state (`IDLE`, `PLAYING`, `PAUSED`).

---

## Properties

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

## Methods

#### `void Start()`
```csharp  
public void Start();  
```
- **Description:** Starts the stopwatch from `0` seconds.
- **Remarks:** Triggers `OnStarted` and sets state to `PLAYING`.

#### `void Start(float time)`
```csharp  
public void Start(float time);  
```
- **Description:** Starts the stopwatch from a specified elapsed time.
- **Parameter:** `time` — starting elapsed time in seconds.
- **Remarks:** Triggers `OnStarted` and sets state to `PLAYING`.

#### `void Pause()`
```csharp  
public void Pause();  
```
- **Description:** Pauses the stopwatch.
- **Remarks:** Triggers `OnPaused` and sets state to `PAUSED`.

#### `void Resume()`
```csharp  
public void Resume();  
```
- **Description:** Resumes the stopwatch from paused state.
- **Remarks:** Triggers `OnResumed` and sets state to `PLAYING`.

#### `void Stop()`
```csharp  
public void Stop();  
```
- **Description:** Stops the stopwatch and resets elapsed time to `0`.
- **Remarks:** Triggers `OnStopped` and sets state to `IDLE`.

#### `bool IsStarted()`
```csharp  
public bool IsStarted();  
```
- **Description:** Returns whether the stopwatch is currently running.
- **Returns:** `true` if `PLAYING`; otherwise `false`.

#### `bool IsPaused()`
```csharp  
public bool IsPaused();  
```
- **Description:** Returns whether the stopwatch is currently paused.
- **Returns:** `true` if `PAUSED`; otherwise `false`.

#### `bool IsIdle()`
```csharp  
public bool IsIdle();  
```
- **Description:** Returns whether the stopwatch is idle.
- **Returns:** `true` if `IDLE`; otherwise `false`.

#### `float GetTime()`
```csharp  
public float GetTime();  
```
- **Description:** Returns the current elapsed time.
- **Returns:** Time in seconds.

#### `void SetTime(float time)`
```csharp  
public void SetTime(float time);  
```
- **Description:** Sets the current elapsed time.
- **Parameter:** `time` — new elapsed time in seconds (>=0).
- **Remarks:** Triggers `OnTimeChanged` if value changes.

#### `StopwatchState GetState()`
```csharp  
public StopwatchState GetState();  
```
- **Description:** Returns the current state of the stopwatch.

#### `void Tick(float deltaTime)`
```csharp  
public void Tick(float deltaTime);  
```
- **Description:** Advances the stopwatch by a time increment.
- **Parameter:** `deltaTime` — seconds to add to elapsed time.
- **Remarks:** Only works in `PLAYING` state; triggers `OnTimeChanged`.

#### `void ResetTime()`
```csharp  
public void ResetTime();  
```
- **Description:** Resets the elapsed time to `0`.
- **Remarks:** Equivalent to `SetTime(0)`.

---

## 🗂 Example of Usage
```csharp  
// Create a stopwatch
IStopwatch stopwatch = new Stopwatch();

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