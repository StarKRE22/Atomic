# 🧩 Time Source Interfaces
Provides a set of flexible interfaces for **time tracking**, **state management**, and **progress monitoring** in reactive systems. These interfaces allow you to create sources that:

- [ITimeSource](#itimesource) — Track **current time**  and notify listeners of changes.
- [IDurationSource](#durationsource) — Handle total duration tracking.
- [ITickSource](#iticksource) — Update incrementally via **ticks** .
- [IStartSource](#istartsource) — Start, stop execution.
- [IPauseSource](#ipausesource) — Pause, or resume execution.
- [ICompleteSource](#icompletesource) — Signal completion.
- [IProgressSource](#iprogresssource) — Progress updates.
- [IStateSource&lt;T&gt;](#istatesource) — Maintain and notify **state changes**.

---

<details>
  <summary>
    <h2 id="itimesource">🧩 ITimeSource</h2>
    <br> Represents a source that tracks the <b>current time</b> and <b>notifies listeners when the time changes</b>.
  </summary>

### Events
#### `event Action<float> OnTimeChanged`
```csharp
public event Action<float> OnTimeChanged;
```
- **Description:** Raised whenever the current time changes.
- **Parameters:** `float` — the new current time in seconds.

### Methods
#### `float GetTime()`
```csharp
public float GetTime();
```
- **Description:** Gets the current time from the source.
- **Returns:** `float` — current time in seconds.

#### `void SetTime(float time)`
```csharp
public void SetTime(float time);
```
- **Description:** Sets the current time.
- **Parameters:**
  - `time` — The new time to set, expected to be in the range `0` to the duration of the source.

#### `void ResetTime()`
```csharp
public void ResetTime();  
```
- **Description:** Resets the time source to its initial state.
- **Remarks:** After resetting, the current time will be the initial time, and any listeners may be notified via `OnTimeChanged`.
</details>

---

<details>
  <summary>
    <h2 id="idurationsource">🧩 IDurationSource</h2>
    <br> Represents a source that <b>has a total duration and can notify changes</b>.
  </summary>

### Events
#### `event Action<float> OnDurationChanged`
```csharp
public event Action<float> OnDurationChanged;
```
- **Description:** Invoked when the duration value changes.

### Methods
#### `float GetDuration()`
```csharp
public float GetDuration();  
```
- **Description:** Gets the total duration.
- **Returns:** The duration in seconds.

#### `void SetDuration(float duration)`
```csharp
public void SetDuration(float duration);  
```
- **Description:** Sets the total duration.
- **Parameter:** `duration` — The new duration value in seconds.
</details>

---

<details>
  <summary>
    <h2 id="iticksource">🧩 ITickSource</h2>
    <br> Represents a source that <b>can be updated over time through the ticks</b>.
  </summary>

### Methods
#### `void Tick(float deltaTime)`
```csharp
public void Tick(float deltaTime);  
```
- **Description:** Updates the source by a specified time increment.
- **Parameters:**
  - `deltaTime` — The amount of time (in seconds) to advance the source.
- **Remarks:** This method is typically called repeatedly (e.g., once per frame) to progress time-dependent systems.
</details>

---

<details>
  <summary>
    <h2 id="istartsource">🧩 IStartSource</h2>
    <br> Represents a source that <b>can be started, stopped, and notify start/stop events</b>.
  </summary>

### Events
#### `event Action OnStarted`
```csharp
public event Action OnStarted;  
```
- **Description:** Raised when the source starts.

#### `event Action OnStopped`
```csharp
public event Action OnStopped;  
```
- **Description:** Raised when the source stops.

### Methods
#### `bool IsIdle()`
```csharp
public bool IsIdle();  
```
- **Description:** Returns `true` if the source has not started yet.

#### `bool IsStarted()`
```csharp
public bool IsStarted();  
```
- **Description:** Returns `true` if the source is running.

#### `void Start(float time)`
```csharp
public void Start(float time);  
```
- **Description:** Starts the source from a specific time.
- **Parameters:**
  - `time` — Time (in seconds) to start from.

#### `void Start()`
```csharp
public void Start();  
```
- **Description:** Starts the source from the default start time.

#### `void Stop()`
```csharp
public void Stop();  
```
- **Description:** Stops the source and resets its time.
</details>

---

<details>
  <summary>
    <h2 id="icompletesource">🧩 ICompleteSource</h2>
    <br> Represents a source that <b>can complete and notify listeners</b>.
  </summary>

### Events
#### `event Action OnCompleted`
```csharp
public event Action OnCompleted;  
```
- **Description:** Invoked when the source has completed.

### Methods
#### `bool IsCompleted()`
```csharp
public bool IsCompleted();  
```
- **Description:** Returns whether the source has completed.
- **Returns:** `true` if completed; otherwise `false`.
</details>

---

<details>
  <summary>
    <h2 id="ipausesource">🧩 IPauseSource</h2>
    <br> Represents a source that <b>can be paused and resumed</b>.
  </summary>

### Events
#### `event Action OnPaused`
```csharp
public event Action OnPaused;  
```
- **Description:** Raised when the source is paused.

#### `event Action OnResumed`
```csharp
public event Action OnResumed;  
```
- **Description:** Raised when the source is resumed.

### Methods
#### `bool IsPaused()`
```csharp
public bool IsPaused();  
```
- **Description:** Returns true if the source is paused.
- **Returns:** `true` if paused; otherwise `false`.

#### `void Pause()`
```csharp
public void Pause();  
```
- **Description:** Pauses the source.

#### `void Resume()`
```csharp
public void Resume();  
```
- **Description:** Resumes the source.
</details>

---

<details>
  <summary>
    <h2 id="iprogresssource">🧩 IProgressSource</h2>
    <br> Represents a source that <b>tracks progress (0–1) and notifies listeners</b>.
  </summary>

### Events
#### `event Action<float> OnProgressChanged`
```csharp
public event Action<float> OnProgressChanged;  
```
- **Description:** Raised when the progress changes.

### Methods
#### `float GetProgress()`
```csharp
public float GetProgress();  
```
- **Description:** Gets the current progress.
- **Returns:** Normalized progress (0–1).

#### `void SetProgress(float progress)`
```csharp
public void SetProgress(float progress);  
```
- **Description:** Sets the current progress.
- **Parameter:** `progress` — Progress value (0–1).
</details>

---

<details>
  <summary>
    <h2 id="istatesource">🧩 IStateSource&lt;T&gt;</h2>
    <br> Represents a source that <b>provides state notifications</b>.
  </summary>

- **Type Parameter:** `T` — Enum type representing the state.

### Events
#### `event Action<T> OnStateChanged`
```csharp
public event Action<T> OnStateChanged;  
```
- **Description:** Raised when the state changes.

### Methods
#### `T GetState()`
```csharp
public T GetState();  
```
- **Description:** Gets the current internal state.
- **Returns:** The current state of type `T`.
</details>