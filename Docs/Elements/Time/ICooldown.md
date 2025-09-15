# 🧩️ ICooldown

`ICooldown` represents a contract of **cooldown timer** that tracks remaining time,  
provides progress feedback and raises events when its state changes. 

The interface combines multiple sources: [ITimeSource](Sources.md/#itimesource), [IDurationSource](Sources.md/#idurationsource), [ITickSource](Sources.md/#iticksource), [IProgressSource](Sources.md/#iprogresssource), [ICompleteSource](Sources.md/#icompletesource) to provide flexible access to timer data and notifications.

> [!IMPORTANT]
> It is useful for game mechanics such as ability cooldowns, weapon reloads, and timed delays.

---

## Events

#### `event Action<float> OnTimeChanged`
```csharp
event Action<float> OnTimeChanged;
```
- **Description:** Raised whenever the current time changes.
- **Parameters:** `float` — the new current time in seconds.

#### `event Action<float> OnDurationChanged`
```csharp
event Action<float> OnDurationChanged;
```
- **Description:** Invoked when the duration value changes.

#### `event Action<float> OnProgressChanged`
```csharp
event Action<float> OnProgressChanged;  
```
- **Description:** Raised when the progress changes.

#### `event Action OnCompleted`
```csharp
event Action OnCompleted;  
```
- **Description:** Invoked when the source has completed.

---

## Methods

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

#### `void Tick(float deltaTime)`
```csharp
void Tick(float deltaTime);  
```
- **Description:** Updates the source by a specified time increment.
- **Parameters:**
  - `deltaTime` — The amount of time (in seconds) to advance the source.
- **Remarks:** This method is typically called repeatedly (e.g., once per frame) to progress time-dependent systems.

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

#### `bool IsCompleted()`
```csharp
bool IsCompleted();  
```
- **Description:** Returns whether the source has completed.
- **Returns:** `true` if completed; otherwise `false`.