# 🧩 PeriodState

`PeriodState` represents the current state of a cycle timer. It is used by `IPeriod` (or similar looping timer implementations) to track the lifecycle of a timer and respond to state changes.

## Enum

### `IDLE`
- **Description:** The period timer is not running and has not been started.
- **Usage:** Initial state before `Start()` is called.

### `PLAYING`
- **Description:** The period timer is currently running and advancing time.
- **Usage:** Indicates the timer is active and `Tick()` is progressing the cycle.

### `PAUSED`
- **Description:** The period timer is paused.
- **Usage:** Timer is temporarily halted by `Pause()` and can continue counting when `Resume()` is called.

---

## 🗂 Example of Usage
The following example demonstrates how `PeriodState` changes during the lifecycle of a period timer and how to respond using `OnStateChanged`.

```csharp
IPeriod period = new Period(10f);

period.OnStateChanged += state =>
    Console.WriteLine($"Period state changed to: {state}");

period.Start(); // Period state changes from IDLE -> PLAYING

period.Pause(); // Period state changes to PAUSED
period.Resume(); // Period state changes back to PLAYING

for (int i = 0; i < 15; i++)
{
    period.Tick(1f); // Advance period by 1 second per tick
}

Console.WriteLine(period.GetState()); // Output: PLAYING (loops automatically)
```