# 🧩 Timers

Represent a **timer family** that supports starting, pausing, resuming, stopping, progress tracking,
and state change notifications. Useful for gameplay timers, ability cooldowns, animation timers, and any system
requiring precise time management.

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
- [Best Practices](#-best-practices)

---

## 🗂 Example of Usage

```csharp
//Create a timer with 30 sec
ITimer timer = new DownTimer(30) // or UpTimer(30)

// Subscribe to events
timer.OnStarted += () => Console.WriteLine("Timer started!");
timer.OnTimeChanged += t => Console.WriteLine($"Time remaining: {t:F1}s");
timer.OnProgressChanged += p => Console.WriteLine($"Progress: {p:P0}");
timer.OnCompleted += () => Console.WriteLine("Timer completed!");

// 1. Start the timer
timer.Start(); // must call Start before ticking

// 2. Tick the timer (simulate time passing, e.g., 1 second per tick)
float deltaTime = 1f;
while (!timer.IsCompleted())
{
    timer.Tick(deltaTime);
    System.Threading.Thread.Sleep(1000); // wait 1 second (simulation)
}

// 3. After completion, you can restart the timer
if (timer.IsCompleted())
{
    Console.WriteLine("Restarting timer...");
    timer.Start();
}

// 4. Pause and resume (optional)
timer.Pause();
Console.WriteLine("Timer paused...");
timer.Resume();

// 5. Stop the timer (optional)
timer.Stop();
Console.WriteLine("Timer stopped!");

// 6. Reset or manually set time/progress (optional)
timer.SetTime(15f);        // set remaining time to 15 seconds
timer.SetProgress(0.5f);   // set progress to 50%
```

---

## 🔍 API Reference

- [ITimer](ITimer.md) — Timer interface
- [UpTimer](UpTimer.md) — Timer that counts **upwards** 
- [DownTimer](DownTimer.md) — Timer that counts **down**
- [TimerState](TimerState.md) — Current state of a timer.

---


## 📌 Best Practices

- [Cooldown vs Timer](../../BestPractices/ChosingBetweenTimerAndCooldown.md)