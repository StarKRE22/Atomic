# 🧩 TimerState

`TimerState` represents the current state of a timer. It is used by `ITimer` (or similar timer implementations) to track the lifecycle of a timer and respond to state changes.

## Enum Values

### `IDLE` = 0
- **Description:** The timer is not running and has not been started.
- **Usage:** Initial state of a timer before `Start()` is called.

### `PLAYING` = 1
- **Description:** The timer is currently counting down or running.
- **Usage:** Indicates that the timer is active and `Tick()` is advancing its time.

### `PAUSED` = 2
- **Description:** The timer is paused and can be resumed.
- **Usage:** Timer is temporarily halted by `Pause()` and can continue counting when `Resume()` is called.

### `COMPLETED` = 3
- **Description:** The timer has finished counting down and expired.
- **Usage:** Indicates that the timer has reached its end, and `OnCompleted` is typically triggered. To restart, the timer should be started again.

---

## 🗂 Example Usage
The following example demonstrates how the `TimerState` changes during the timer lifecycle, and how to respond using `OnStateChanged`.

```csharp
ITimer timer = new Timer(10f);

timer.OnStateChanged += state => 
    Console.WriteLine($"Timer state changed to: {state}");

timer.Start(); // Timer state changes from IDLE -> PLAYING

timer.Pause(); // Timer state changes to PAUSED
timer.Resume(); // Timer state changes back to PLAYING

while (!timer.IsCompleted())
{
    timer.Tick(1f); // Advance timer by 1 second per tick
}

Console.WriteLine(timer.GetState()); // Output: COMPLETED
```