# 🧩️️ Stopwatch

`Stopwatch` represents a stopwatch timer that can start, pause, resume, stop,  
and track elapsed time. It is useful for measuring intervals, performance, or gameplay events.

---

## IStopwatch

Defines the **contract** for a stopwatch timer.

> The interface combines multiple sources (`IStartSource`, `IPauseSource`, `ITimeSource`, `IStateSource<StopwatchState>`, `ITickSource`)  
to provide full control over elapsed time and state notifications.

### Events

- `event Action OnStarted` – invoked when the stopwatch starts.
- `event Action OnStopped` – invoked when the stopwatch is stopped.
- `event Action OnPaused` – invoked when the stopwatch is paused.
- `event Action OnResumed` – invoked when the stopwatch resumes.
- `event Action<float> OnTimeChanged` – invoked when the elapsed time changes.
- `event Action<StopwatchState> OnStateChanged` – invoked when the stopwatch state changes.

### Methods

- `StopwatchState GetState()` – returns the current state (`IDLE`, `PLAYING`, `PAUSED`).
- `bool IsIdle()` – returns `true` if the stopwatch hasn’t started yet.
- `bool IsStarted()` – returns `true` if the stopwatch is running.
- `bool IsPaused()` – returns `true` if the stopwatch is paused.

- `float GetTime()` – returns the current elapsed time.
- `void SetTime(float time)` – sets the elapsed time (must be ≥ 0).
- `void ResetTime()` – resets the elapsed time to zero.

- `void Start()` – starts the stopwatch from zero.
- `void Start(float time)` – starts the stopwatch from a specific elapsed time.
- `void Pause()` – pauses the stopwatch if running.
- `void Resume()` – resumes the stopwatch if paused.
- `void Stop()` – stops the stopwatch and resets elapsed time to zero.
- `void Tick(float deltaTime)` – advances the stopwatch by `deltaTime` (only if running).

---

## Stopwatch

The `Stopwatch` class implements `IStopwatch`.  
It tracks elapsed time, supports start, pause, resume, stop, reset, and notifies listeners on state and time changes.

### Constructors

- `Stopwatch()` – creates a stopwatch (time starts at zero).

### Example of Usage

```csharp
var stopwatch = new Stopwatch();

stopwatch.OnStarted += () => Console.WriteLine("Stopwatch started!");
stopwatch.OnTimeChanged += t => Console.WriteLine($"Elapsed: {t}s");
stopwatch.OnStopped += () => Console.WriteLine("Stopwatch stopped!");

// start the stopwatch
stopwatch.Start();

// simulate ticking
for (int i = 0; i < 5; i++)
{
    stopwatch.Tick(1f);
}

// pause and resume
stopwatch.Pause();
stopwatch.Resume();

// stop
stopwatch.Stop();
```

### Behavior

- Supports all methods and events defined by `IStopwatch`.
- Tracks state changes (`IDLE`, `PLAYING`, `PAUSED`).
- `Tick()` increases elapsed time only when running.
- `ResetTime()` restores elapsed time to zero.

---

### StopwatchState

Represents the current state of a `Stopwatch`.

| State     | Description                            |
|-----------|----------------------------------------|
| `IDLE`    | The stopwatch is idle and not running. |
| `PLAYING` | The stopwatch is currently running.    |
| `PAUSED`  | The stopwatch is paused.               |
