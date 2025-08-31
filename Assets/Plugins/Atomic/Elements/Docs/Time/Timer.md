# 🧩️ Timer

`ITimer` defines a general-purpose timer that supports start, pause, resume, stop,  
progress tracking, and state change notifications. It is useful for gameplay timers, UI animations, or any timed sequence.

---

## ITimer

Defines the **contract** for a timer.
> The interface combines multiple sources (`IStartSource`, `IPauseSource`, `ICompleteSource`, `IStateSource<TimerState>`, `ITimeSource`, `IDurationSource`, `IProgressSource`, `ITickSource`)  
to provide full control over timer behavior and state monitoring.

### Events

- `event Action OnStarted` – invoked when the timer begins.
- `event Action OnStopped` – invoked when the timer is manually stopped.
- `event Action OnPaused` – invoked when the timer is paused.
- `event Action OnResumed` – invoked when the timer resumes from pause.
- `event Action OnCompleted` – invoked when the timer reaches its duration.
- `event Action<TimerState> OnStateChanged` – invoked when the timer state changes.
- `event Action<float> OnTimeChanged` – invoked when the current time changes.
- `event Action<float> OnDurationChanged` – invoked when the total duration changes.
- `event Action<float> OnProgressChanged` – invoked when the progress (0–1) changes.

### Methods

- `TimerState GetState()` – returns the current state (`IDLE`, `PLAYING`, `PAUSED`, `COMPLETED`).
- `bool IsIdle()` – returns `true` if the timer hasn’t started yet.
- `bool IsStarted()` – returns `true` if the timer is running.
- `bool IsPaused()` – returns `true` if the timer is paused.
- `bool IsCompleted()` – returns `true` if the timer has reached its duration.

- `float GetDuration()` – returns the total duration.
- `void SetDuration(float duration)` – sets the total duration.

- `float GetTime()` – returns the current elapsed time.
- `void SetTime(float time)` – sets the current elapsed time (0–duration).
- `void ResetTime()` – resets the elapsed time to zero.

- `float GetProgress()` – returns normalized progress (0–1).
- `void SetProgress(float progress)` – sets progress, updating current time accordingly.

- `void Start()` – starts the timer from zero.
- `void Start(float time)` – starts the timer from a specific time.
- `void Pause()` – pauses the timer if running.
- `void Resume()` – resumes the timer if paused.
- `void Stop()` – stops the timer and resets elapsed time to zero.
- `void Tick(float deltaTime)` – advances the timer, triggers completion if needed.

---

## Timer

The `Timer` class implements `ITimer`.  
It is a flexible timer that tracks duration, elapsed time, progress, and state, supporting start, pause, resume, stop, reset, and notifications.

### Constructors

- `Timer()` – creates an empty timer (duration must be set manually).
- `Timer(float duration)` – creates a timer with the given duration.

### Example of Usage

```csharp
var timer = new Timer(5f); // 5 seconds

timer.OnStarted += () => Console.WriteLine("Timer started!");
timer.OnCompleted += () => Console.WriteLine("Timer finished!");
timer.OnProgressChanged += p => Console.WriteLine($"Progress: {p:P0}");

// start timer
timer.Start();

// simulate game loop
while (!timer.IsCompleted())
{
    timer.Tick(1f);
    Console.WriteLine($"Elapsed: {timer.GetTime()}s");
}
```
### Behavior

- Supports all methods and events defined by `ITimer`.
- Tracks state changes (`IDLE`, `PLAYING`, `PAUSED`, `COMPLETED`).
- Progress is normalized from 0 (not started) to 1 (completed).
- `ResetTime()` restores the timer’s elapsed time to zero.

---

### TimerState

Represents the current state of a `Timer`.

| State       | Description                        |
|-------------|------------------------------------|
| `IDLE`      | No activity yet.                   |
| `PLAYING`   | Timer is currently running.        |
| `PAUSED`    | Timer is temporarily paused.       |
| `COMPLETED` | Timer has finished (time expired). |
