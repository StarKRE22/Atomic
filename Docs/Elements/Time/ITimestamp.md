# 🧩 ITimestamp

Represents a **timestamp that can be tracked over time using ticks**. It provides properties and methods to start, stop,
and query the state of a timestamp, including remaining time, progress, and expiration status. `ITimestamp` is
especially useful in **tick-based systems**, where the game logic updates in discrete ticks. It is ideal for multiplayer
scenarios with **client-side prediction**, as it allows precise tracking of time and progress independently of frame
rate.

---

## 🔑 Properties

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

## 🏹 Methods

#### `StartFromSeconds(float)`

```csharp
void StartFromSeconds(float seconds);
```

- **Description:** Starts the timestamp from the current time with a specified duration.
- **Parameters:** `seconds` — duration in seconds.

#### `StartFromTicks(int)`

```csharp
void StartFromTicks(int ticks);
```

- **Description:** Starts the timestamp using a specified number of ticks.
- **Parameters:** `ticks` — duration in ticks.

#### `Stop()`

```csharp
void Stop();
```

- **Description:** Stops and resets the timestamp. After calling this, the timestamp is idle.

#### `GetProgress(float)`

```csharp
float GetProgress(float duration);
```

- **Description:** Returns the progress of the timestamp relative to a given duration.
- **Parameters:** `float duration` — the full duration in seconds.
- **Returns:** `float` — progress value between `0` and `1`.

#### `IsIdle()`

```csharp
bool IsIdle();
```

- **Description:** Indicates whether the timestamp is stopped and has not started.
- **Returns:** `true` if idle; otherwise, `false`.

#### `IsPlaying()`

```csharp
bool IsPlaying();
```

- **Description:** Indicates whether the timestamp is currently active and counting.
- **Returns:** `true` if playing; otherwise, `false`.

#### `IsExpired()`

```csharp
bool IsExpired();
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