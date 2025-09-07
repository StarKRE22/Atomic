#  🧩️ Timestamp

`Timestamp` represents a timestamp that can be tracked over time using ticks.  
It is useful for measuring time intervals, scheduling events, or tracking expiration in a deterministic tick-based system.

---

## ITimestamp

Defines the **contract** for a timestamp.

### Properties

- `int EndTick` – the tick at which the timestamp is considered complete.
- `int RemainingTicks` – number of ticks remaining until expiration.
- `float RemainingTime` – remaining time in seconds until expiration.

### Methods

- `void StartFromSeconds(float seconds)` – starts the timestamp from the current time with a given duration in seconds.
- `void StartFromTicks(int ticks)` – starts the timestamp with a specified number of ticks.
- `void Stop()` – stops and resets the timestamp.
- `float GetProgress(float duration)` – returns progress relative to a duration (0–1).
- `bool IsIdle()` – returns `true` if the timestamp is stopped and has not started.
- `bool IsPlaying()` – returns `true` if the timestamp is active and counting.
- `bool IsExpired()` – returns `true` if the timestamp has expired.

---

## FixedTimestamp

The `FixedTimestamp` class implements `ITimestamp` and uses Unity’s `Time.fixedTime` and `Time.fixedDeltaTime`  
to update the timestamp on fixed updates. It is suitable for deterministic timing in physics updates.

### Constructors

- `FixedTimestamp(int endTick = -1)` – creates a timestamp. Default `endTick` is -1 (inactive).

### Example of Usage

```csharp
var timestamp = new FixedTimestamp();

// start for 3 seconds
timestamp.StartFromSeconds(3f);

while (!timestamp.IsExpired())
{
    Debug.Log($"Remaining time: {timestamp.RemainingTime}s");
}
Debug.Log("Timestamp expired!");
```

### Behavior

- Supports starting from seconds or ticks.
- Tracks remaining ticks and time.
- `GetProgress(duration)` returns normalized progress (0–1).
- `Stop()` resets the timestamp.
- `IsIdle()`, `IsPlaying()`, and `IsExpired()` report current state based on fixed ticks.
