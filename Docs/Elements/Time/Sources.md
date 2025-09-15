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

<br>

### Events
#### `event Action<float> OnTimeChanged`
```csharp
event Action<float> OnTimeChanged;
```
- **Description:** Raised whenever the current time changes.
- **Parameters:** `float` — the new current time in seconds.

### Methods
#### `float GetTime()`
```csharp
float GetTime();
```
- **Description:** Gets the current time from the source.
- **Returns:** `float` — current time in seconds.

#### `void SetTime(float time)`
```csharp
void SetTime(float time);
```
- **Description:** Sets the current time.
- **Parameters:**
  - `time` — The new time to set, expected to be in the range `0` to the duration of the source.

#### `void ResetTime()`
```csharp
void ResetTime();  
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
<br>

### Events
#### `event Action<float> OnDurationChanged`
```csharp
event Action<float> OnDurationChanged;
```
- **Description:** Invoked when the duration value changes.

### Methods
#### `float GetDuration()`
```csharp
float GetDuration();  
```
- **Description:** Gets the total duration.
- **Returns:** The duration in seconds.

#### `void SetDuration(float duration)`
```csharp
void SetDuration(float duration);  
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

<br>

### Methods
#### `void Tick(float deltaTime)`
```csharp
void Tick(float deltaTime);  
```
- **Description:** Updates the source by a specified time increment.
- **Parameters:**
  - `deltaTime` — The amount of time (in seconds) to advance the source.
- **Remarks:** This method is typically called repeatedly (e.g., once per frame) to progress time-dependent systems.
</details>

---

<details>
  <summary>
    <h2 id="iprogresssource">🧩 IProgressSource</h2>
    <br> Represents a source that <b>tracks progress (0–1) and notifies listeners</b>.
  </summary>

<br>

### Events
### `event Action<float> OnProgressChanged`
```csharp
event Action<float> OnProgressChanged;  
```
- **Description:** Raised when the progress changes.

### Methods
#### `float GetProgress()`
```csharp
float GetProgress();  
```
- **Description:** Gets the current progress.
- **Returns:** Normalized progress (0–1).

#### `void SetProgress(float progress)`
```csharp
void SetProgress(float progress);  
```
- **Description:** Sets the current progress.
- **Parameter:** `progress` — Progress value (0–1).
</details>

---

<details>
  <summary>
    <h2 id="istartsource">🧩 IStartSource</h2>
    <br> Represents a source that <b>can be started, stopped, and notify start/stop events</b>.
  </summary>

<br>

### Events
#### `event Action OnStarted`
```csharp
event Action OnStarted;  
```
- **Description:** Raised when the source starts.

#### `event Action OnStopped`
```csharp
event Action OnStopped;  
```
- **Description:** Raised when the source stops.

### Methods
#### `bool IsIdle()`
```csharp
bool IsIdle();  
```
- **Description:** Returns `true` if the source has not started yet.

#### `bool IsStarted()`
```csharp
bool IsStarted();  
```
- **Description:** Returns `true` if the source is running.

#### `void Start(float time)`
```csharp
void Start(float time);  
```
- **Description:** Starts the source from a specific time.
- **Parameters:**
  - `time` — Time (in seconds) to start from.

#### `void Start()`
```csharp
void Start();  
```
- **Description:** Starts the source from the default start time.

#### `void Stop()`
```csharp
void Stop();  
```
- **Description:** Stops the source and resets its time.
</details>

---

<details>
  <summary>
    <h2 id="icompletesource">🧩 ICompleteSource</h2>
    <br> Represents a source that <b>can complete and notify listeners</b>.
  </summary>

<br>

### Events
#### `event Action OnCompleted`
```csharp
event Action OnCompleted;  
```
- **Description:** Invoked when the source has completed.

### Methods
#### `bool IsCompleted()`
```csharp
bool IsCompleted();  
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

<br>

### Events
#### `event Action OnPaused`
```csharp
event Action OnPaused;  
```
- **Description:** Raised when the source is paused.

#### `event Action OnResumed`
```csharp
event Action OnResumed;  
```
- **Description:** Raised when the source is resumed.

### Methods
#### `bool IsPaused()`
```csharp
bool IsPaused();  
```
- **Description:** Returns true if the source is paused.
- **Returns:** `true` if paused; otherwise `false`.

#### `void Pause()`
```csharp
void Pause();  
```
- **Description:** Pauses the source.

#### `void Resume()`
```csharp
void Resume();  
```
- **Description:** Resumes the source.
</details>

---

<details>
  <summary>
    <h2 id="istatesource">🧩 IStateSource&lt;T&gt;</h2>
    <br> Represents a source that <b>provides state notifications</b>.
  </summary>

<br>

- **Type Parameter:** `T` — Enum type representing the state.

### Events
#### `event Action<T> OnStateChanged`
```csharp
event Action<T> OnStateChanged;  
```
- **Description:** Raised when the state changes.

### Methods
#### `T GetState()`
```csharp
T GetState();  
```
- **Description:** Gets the current internal state.
- **Returns:** The current state of type `T`.
</details>