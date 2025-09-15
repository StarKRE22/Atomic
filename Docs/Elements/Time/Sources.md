# 🧩 Time Source Interfaces

<details>
  <summary>
    <h2>🧩 ITimeSource</h2>
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

---
</details>