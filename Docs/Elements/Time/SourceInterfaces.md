# 🧩️ Source Interfaces

These interfaces define **building blocks** for timer-like systems in Atomic.Elements.  
They provide event-driven notifications, progress tracking, state management, and time updates,  
allowing creation of flexible timers, cooldowns, stopwatches, and period cycles.

---

## ICompleteSource

Defines a source that can complete and notify listeners.

### Events

- `event Action OnCompleted` – invoked when the source finishes.

### Methods

- `bool IsCompleted()` – returns `true` if the source has finished.

---

## IDurationSource

Defines a source with a total duration that can notify on changes.

### Events

- `event Action<float> OnDurationChanged` – invoked when the duration changes.

### Methods

- `float GetDuration()` – returns the total duration.
- `void SetDuration(float duration)` – sets the total duration, triggers event.

---

## IPauseSource

Defines a source that can be paused and resumed.

### Events

- `event Action OnPaused` – invoked when the source is paused.
- `event Action OnResumed` – invoked when the source resumes.

### Methods

- `bool IsPaused()` – returns `true` if currently paused.
- `void Pause()` – pauses the source.
- `void Resume()` – resumes the source.

---

## IProgressSource

Defines a source that tracks progress (normalized 0–1).

### Events

- `event Action<float> OnProgressChanged` – invoked when progress changes.

### Methods

- `float GetProgress()` – returns current progress (0–1).
- `void SetProgress(float progress)` – sets progress, updating underlying time if applicable.

---

## IStartSource

Defines a source that can start, stop, and report state.

### Events

- `event Action OnStarted` – invoked when the source starts.
- `event Action OnStopped` – invoked when the source stops.

### Methods

- `bool IsIdle()` – returns `true` if the source hasn’t started.
- `bool IsStarted()` – returns `true` if the source is running.
- `void Start()` – starts the source from default start time.
- `void Start(float time)` – starts the source from a specific time.
- `void Stop()` – stops the source and resets current time.

---

## IStateSource<T>

Defines a source that exposes internal state of type `T` (enum).

### Events

- `event Action<T> OnStateChanged` – invoked when the state changes.

### Methods

- `T GetState()` – returns the current internal state.

---

## ITickSource

Defines a source that updates over time.

### Methods

- `void Tick(float deltaTime)` – advances the source by `deltaTime`.

---

## ITimeSource

Defines a source that tracks current time and notifies changes.

### Events

- `event Action<float> OnTimeChanged` – invoked when the current time changes.

### Methods

- `float GetTime()` – returns the current time.
- `void SetTime(float time)` – sets the current time.
- `void ResetTime()` – resets to initial time.

---

## Behavior Summary

- Sources are modular and can be combined to implement cooldowns, stopwatches, timers, and cycles.
- Most sources provide **event-driven notifications** (`OnStarted`, `OnCompleted`, `OnPaused`, `OnTimeChanged`, `OnProgressChanged`, etc.).
- `ITickSource` is used to **advance time** in update loops.
- `IProgressSource` normalizes values to **0–1** for progress tracking.
- `IStartSource` and `IPauseSource` allow **playback control**.
- `IStateSource<T>` enables querying and subscribing to **state changes**.
