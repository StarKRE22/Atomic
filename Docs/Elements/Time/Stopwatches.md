# 🧩 Stopwatches

Represents a **stateful stopwatch timer** that tracks elapsed time and supports **start, pause, resume, stop**, time
updates, and state notifications. It provides a simple way to
track elapsed time in gameplay, animations, or any time-dependent system.

> [!TIP]  
> Use `IStopwatch` when you need to **measure elapsed time** (e.g., performance tracking, gameplay session time,
> speedrun timers). Unlike [ITimer](ITimer.md), a stopwatch does not count down toward a duration — it only measures how
> long something has been running.

- [IStopwatch](IStopwatch.md) – Stopwatch interface
- [Stopwatch](Stopwatch.md) — Stopwatch implementation
- [StopwatchState](StopwatchState.md) — Current state of a stopwatch