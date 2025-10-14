# 🧩 Stopwatches

Represents a **stateful stopwatch timer** that tracks elapsed time and supports **start, pause, resume, stop**, time
updates, and state notifications. It provides a simple way to
track elapsed time in gameplay, animations, or any time-dependent system. Use `IStopwatch` when you need to **measure
elapsed time** (e.g., performance tracking, gameplay session time, speedrun timers). Unlike [ITimer](ITimer.md), a
stopwatch does not count down toward a duration — it only measures how
long something has been running.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)

---

## 🗂 Example of Usage

```csharp  
// Create a stopwatch
IStopwatch stopwatch = new Stopwatch();

// Subscribe to events
stopwatch.OnStarted += () => Console.WriteLine("Stopwatch started!");
stopwatch.OnTimeChanged += t => Console.WriteLine($"Elapsed: {t:F1}s");
stopwatch.OnPaused += () => Console.WriteLine("Stopwatch paused.");
stopwatch.OnResumed += () => Console.WriteLine("Stopwatch resumed.");
stopwatch.OnStopped += () => Console.WriteLine("Stopwatch stopped.");

// Start the stopwatch
stopwatch.Start();

// Simulate ticking
float deltaTime = 1f;
for (int i = 0; i < 5; i++)
{
    stopwatch.Tick(deltaTime);
    System.Threading.Thread.Sleep(1000);
}

// Pause and resume
stopwatch.Pause();
stopwatch.Resume();

// Stop and reset
stopwatch.Stop();
stopwatch.ResetTime();
```

---

## 🔍 API Reference

- [IStopwatch](IStopwatch.md) – Stopwatch interface
- [Stopwatch](Stopwatch.md) — Stopwatch implementation
- [StopwatchState](StopwatchState.md) — Current state of a stopwatch