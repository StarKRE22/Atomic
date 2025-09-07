# 🧩️ IPeriod

`Period` represents a looping cycle timer that tracks time progression and emits events  
on completion of each cycle. It is useful for repeating game mechanics, looping animations, or periodic updates.

---

## IPeriod

Defines the **contract** for a looping timer.

> The interface combines multiple sources (`IStartSource`, `IPauseSource`, `IStateSource<PeriodState>`, `ITimeSource`, `IProgressSource`, `IDurationSource`, `ITickSource`)  
to provide full control over cycle timing and notifications.

### Events

- `event Action OnStarted` – invoked when the timer starts.
- `event Action OnStopped` – invoked when the timer stops.
- `event Action OnPaused` – invoked when the timer pauses.
- `event Action OnResumed` – invoked when the timer resumes from pause.
- `event Action OnPeriod` – invoked each time the cycle completes.
- `event Action<PeriodState> OnStateChanged` – invoked when the timer state changes.
- `event Action<float> OnTimeChanged` – invoked when the current time changes.
- `event Action<float> OnDurationChanged` – invoked when the cycle duration changes.
- `event Action<float> OnProgressChanged` – invoked when progress changes (0–1).

### Methods

- `PeriodState GetState()` – returns the current state (`IDLE`, `PLAYING`, `PAUSED`).
- `bool IsIdle()` – returns `true` if the timer is idle.
- `bool IsStarted()` – returns `true` if the timer is running.
- `bool IsPaused()` – returns `true` if the timer is paused.

- `float GetDuration()` – returns the duration of one cycle.
- `void SetDuration(float duration)` – sets the duration of the cycle.

- `float GetTime()` – returns the current time in the cycle.
- `void SetTime(float time)` – sets the current time (clamped to [0, duration]).
- `void ResetTime()` – resets the current time to zero.

- `float GetProgress()` – returns normalized progress of the current cycle (0–1).
- `void SetProgress(float progress)` – sets progress, updating the current time accordingly.

- `void Start()` – starts the timer from zero.
- `void Start(float time)` – starts the timer from a specific time.
- `void Pause()` – pauses the timer if running.
- `void Resume()` – resumes the timer if paused.
- `void Stop()` – stops the timer and resets time.
- `void Tick(float deltaTime)` – advances the timer and triggers `OnPeriod` if the cycle completes.

---

## Period

The `Period` class implements `IPeriod`.  
It is a looping cycle timer that tracks duration, current time, progress, and state,  
emitting events for each completed cycle while supporting start, pause, resume, stop, reset, and notifications.

### Constructors

- `Period()` – creates an empty period timer (duration must be set manually).
- `Period(float duration)` – creates a period timer with the given cycle duration.

### Example of Usage

```csharp
var period = new Period(3f); // 3-second cycles

period.OnStarted += () => Console.WriteLine("Period started!");
period.OnPeriod += () => Console.WriteLine("Cycle completed!");
period.OnProgressChanged += p => Console.WriteLine($"Progress: {p:P0}");

// start the timer
period.Start();

// simulate game loop
for (int i = 0; i < 10; i++)
{
    period.Tick(1f);
    Console.WriteLine($"Time in cycle: {period.GetTime()}s");
}
```
### Behavior

- Supports all methods and events defined by `IPeriod`.
- Tracks state changes (`IDLE`, `PLAYING`, `PAUSED`).
- Progress is normalized from 0 (start of cycle) to 1 (end of cycle).
- `ResetTime()` restores the cycle’s current time to zero.
- Automatically emits `OnPeriod` and restarts the cycle when the duration is reached.

---

### PeriodState

Represents the current state of the `Period` timer.

| State     | Description                          |
|-----------|--------------------------------------|
| `IDLE`    | The timer is idle and not running.   |
| `PLAYING` | The timer is currently running.      |
| `PAUSED`  | The timer is paused.                 |