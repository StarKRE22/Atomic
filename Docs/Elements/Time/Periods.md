# 🧩 Periods

Represents a **stateful looping cycle timer** that tracks time progression and emits events on completion of each cycle.
It provides full control over **start, pause, resume, stop**, progress tracking, duration management, and state
notifications. Use `Period` when you need **repeating timers or cycle-based events**, such as gameplay loops,
animations, or periodic system updates. Unlike [ITimer](ITimer.md),
`Period` automatically loops on completion.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)

---

## 🗂 Example of Usage

```csharp
//Create a new period with 10 sec
IPeriod period = new Period(10f);

// Subscribe to events
period.OnStarted += () => Console.WriteLine("Period started!");
period.OnTimeChanged += t => Console.WriteLine($"Time: {t:F1}s");
period.OnProgressChanged += p => Console.WriteLine($"Progress: {p:P0}");
period.OnPeriod += () => Console.WriteLine("Cycle completed!");
period.OnPaused += () => Console.WriteLine("Period paused.");
period.OnResumed += () => Console.WriteLine("Period resumed.");
period.OnStopped += () => Console.WriteLine("Period stopped.");

// Start the period
period.Start();

// Simulate ticking 1 second per loop
float deltaTime = 1f;
for (int i = 0; i < 25; i++)
{
    period.Tick(deltaTime);
    System.Threading.Thread.Sleep(1000);
}

// Pause and resume
period.Pause();
period.Resume();

// Stop and reset
period.Stop();
period.ResetTime();
```

---

## 🔍 API Reference

- [IPeriod](IPeriod.md) — Represents interface of the period
- [Period](Period.md) — Represents implementation of the period
- [PeriodState](PeriodState.md) — Represents current state of the period
