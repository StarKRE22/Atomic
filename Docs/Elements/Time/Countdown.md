# 🧩️ Countdown

`Countdown` represents a countdown timer that supports play, pause, stop, reset,  
while broadcasting progress and state changes. It is useful for timed gameplay mechanics, UI timers, or sequences requiring precise control.


---

## ICountdown

Defines the **contract** for a countdown timer.
> The interface combines multiple sources (`IDurationSource`, `ITimeSource`, `IProgressSource`, `ICompleteSource`, `IStartSource`, `IPauseSource`, `ITickSource`, `IStateSource`)  
to provide full control over timer behavior and state notifications.

### Events

- `event Action OnStarted` – invoked when the countdown begins.
- `event Action OnStopped` – invoked when the countdown is manually stopped.
- `event Action OnPaused` – invoked when the countdown is paused.
- `event Action OnResumed` – invoked when the countdown resumes from pause.
- `event Action OnCompleted` – invoked when the countdown reaches zero.
- `event Action<CountdownState> OnStateChanged` – invoked when the timer state changes.
- `event Action<float> OnTimeChanged` – invoked when the remaining time changes.
- `event Action<float> OnDurationChanged` – invoked when the total duration changes.
- `event Action<float> OnProgressChanged` – invoked when the progress (0–1) changes.

### Methods

- `CountdownState GetState()` – returns the current state (`IDLE`, `PLAYING`, `PAUSED`, `COMPLETED`).
- `bool IsIdle()` – returns `true` if the countdown hasn’t started yet.
- `bool IsStarted()` – returns `true` if the countdown is running.
- `bool IsPaused()` – returns `true` if the countdown is paused.
- `bool IsCompleted()` – returns `true` if the countdown finished.

- `float GetDuration()` – returns the total duration.
- `void SetDuration(float duration)` – sets the total duration.

- `float GetTime()` – returns the current remaining time.
- `void SetTime(float time)` – sets the remaining time (0–duration).
- `void ResetTime()` – resets the remaining time to full duration.

- `float GetProgress()` – returns normalized progress (0–1).
- `void SetProgress(float progress)` – sets progress, updating remaining time accordingly.

- `void Start()` – starts the countdown using the full duration.
- `void Start(float time)` – starts the countdown with a specific remaining time.
- `void Pause()` – pauses the countdown if running.
- `void Resume()` – resumes the countdown if paused.
- `void Stop()` – stops the countdown and resets the remaining time to zero.
- `void Tick(float deltaTime)` – advances the countdown, triggers completion if needed.

---

## Countdown

The `Countdown` class implements `ICountdown`.  
It is a flexible countdown timer that supports play, pause, resume, stop, reset, and notifies on state, time, and progress changes.

### Constructors

- `Countdown()` – creates an empty countdown (duration must be set manually).
- `Countdown(float duration)` – creates a countdown with the given duration.

### Example of Usage

```csharp
var countdown = new Countdown(5f); // 5 seconds

countdown.OnStarted += () => Console.WriteLine("Countdown started!");
countdown.OnCompleted += () => Console.WriteLine("Countdown finished!");
countdown.OnProgressChanged += p => Console.WriteLine($"Progress: {p:P0}");

//start countdown
countdown.Start();

// simulate game loop
while (!countdown.IsCompleted())
{
    countdown.Tick(1f);
    Console.WriteLine($"Time left: {countdown.GetTime()}s");
}
```
### Behavior

- Supports all methods and events defined by `ICountdown`.
- Tracks state changes (`IDLE`, `PLAYING`, `PAUSED`, `COMPLETED`).
- Progress is normalized from 0 (not started) to 1 (completed).
- `ResetTime()` restores the countdown to full duration.

---

### CountdownState

Represents the current state of a `Countdown`.

| State       | Description                            |
|-------------|----------------------------------------|
| `IDLE`      | No activity yet.                       |
| `PLAYING`   | Countdown is currently running.        |
| `PAUSED`    | Countdown is temporarily paused.       |
| `COMPLETED` | Countdown has finished (time expired). |
