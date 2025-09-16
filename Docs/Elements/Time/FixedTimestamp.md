# 🧩 FixedTimestamp

`FixedTimestamp` is a concrete implementation of [ITimestamp](ITimestamp.md) that is **driven by Unity's `Time.fixedTime`** and updated on `FixedUpdate`. It tracks a timestamp in ticks and seconds, suitable for tick-based game logic and physics updates.

> [!NOTE]  
> Especially useful in **tick-based systems** as it provides consistent timing independent of frame rate.

---

## Constructors

#### `FixedTimestamp(int endTick = -1)`
```csharp
public FixedTimestamp(int endTick = -1);
```
- **Description:** Initializes a new instance of `FixedTimestamp`.
- **Parameter:** `endTick` — optional end tick value. Default is `-1` (inactive).

---

## Properties

#### `EndTick`
```csharp
public int EndTick { get; }
```
- **Description:** Gets the tick at which the timestamp is considered complete.

#### `RemainingTicks`
```csharp
public int RemainingTicks { get; }
```
- **Description:** Gets the number of ticks remaining until expiration.

#### `RemainingTime`
```csharp
public float RemainingTime { get; }
```
- **Description:** Gets the remaining time until expiration in seconds.

---

## Methods

#### `void StartFromSeconds(float seconds)`
```csharp
public void StartFromSeconds(float seconds);
```
- **Description:** Starts the timestamp from the current time with a specified duration in seconds.
- **Parameters:** `seconds` — duration in seconds.
- **Notes:** Throws `ArgumentOutOfRangeException` if `seconds` is negative.

#### `void StartFromTicks(int ticks)`
```csharp
public void StartFromTicks(int ticks);
```
- **Description:** Starts the timestamp using a specified number of ticks.
- **Parameters:** `ticks` — duration in ticks.
- **Notes:** Throws `ArgumentOutOfRangeException` if `ticks` is negative.

#### `void Stop()`
```csharp
public void Stop();
```
- **Description:** Stops and resets the timestamp. After calling this, the timestamp is idle.

#### `float GetProgress(float duration)`
```csharp
public float GetProgress(float duration);
```
- **Description:** Returns the progress of the timestamp relative to a given duration.
- **Parameters:** `duration` — the full duration in seconds.
- **Returns:** progress value between 0 and 1.

#### `bool IsIdle()`
```csharp
public bool IsIdle();
```
- **Description:** Indicates whether the timestamp is stopped and has not started.
- **Returns:** `true` if idle; otherwise, `false`.

#### `bool IsPlaying()`
```csharp
public bool IsPlaying();
```
- **Description:** Indicates whether the timestamp is currently active and counting.
- **Returns:** `true` if playing; otherwise, `false`.

#### `bool IsExpired()`
```csharp
public bool IsExpired();
```
- **Description:** Indicates whether the timestamp has expired.
- **Returns:** `true` if expired; otherwise, `false`.

---

## 🗂 Example of Usage
```csharp
public class Example : MonoBehaviour 
{
    ITimestamp _timestamp = new FixedTimestamp();
    
    private void Awake()
    {
        _timestamp.StartFromSeconds(5f);
        //_timestamp.StartFromTicks(250); (equivalent to 5 seconds)
    }
    
    private void FixedUpdate()
    {
        if (_timestamp.IsExpired())
            Debug.Log("Timestamp expired!");
        else if (_timestamp.IsPlaying())
            Debug.Log("Timestamp is still running.");
        else if (_timestamp.IsIdle())
            Debug.Log("Timestamp is idle.");
    }
}
```
