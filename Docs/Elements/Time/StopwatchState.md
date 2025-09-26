# 🧩 StopwatchState

Represents the **current state of a stopwatch**. It is used by [IStopwatch](IStopwatch.md) and [Stopwatch](Stopwatch.md) to indicate whether the stopwatch is idle, running, paused, or stopped.

---

#### `IDLE`
- **Description:** The stopwatch has not been started yet.
- **Usage:** Returned by `IStopwatch.GetState()` before `Start()` is called.

#### `RUNNING`
- **Description:** The stopwatch is actively measuring elapsed time.
- **Usage:** Returned by `IStopwatch.GetState()` while the stopwatch is counting.
- **Events triggered:** `OnTimeChanged` updates as time progresses.

#### `PAUSED`
- **Description:** The stopwatch was running but has been temporarily paused.
- **Usage:** Returned by `IStopwatch.GetState()` after `Pause()` is called and before `Resume()`.
- **Remarks:** Elapsed time does not increase until resumed.

#### `STOPPED`
- **Description:** The stopwatch has been stopped and optionally reset.
- **Usage:** Returned by `IStopwatch.GetState()` after `Stop()` is called.
- **Remarks:** `OnStopped` event is triggered, and elapsed time may reset to zero depending on implementation.

---

## 🗂 Example of Usage
```csharp
IStopwatch stopwatch = new Stopwatch();

// Check initial state
Console.WriteLine(stopwatch.GetState()); // Output: IDLE

// Start the stopwatch
stopwatch.Start();
Console.WriteLine(stopwatch.GetState()); // Output: RUNNING

// Pause the stopwatch
stopwatch.Pause();
Console.WriteLine(stopwatch.GetState()); // Output: PAUSED

// Resume
stopwatch.Resume();
Console.WriteLine(stopwatch.GetState()); // Output: RUNNING

// Stop
stopwatch.Stop();
Console.WriteLine(stopwatch.GetState()); // Output: STOPPED
```