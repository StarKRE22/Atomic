# 🧩 Time

Provides a set of tools for managing **timers, cooldowns, countdowns, stopwatches, and time sources**. It allows
developers to track and control time-related events in a consistent and reactive manner, making it useful for gameplay
mechanics, scheduling, and periodic updates.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
    - [Cooldown Usage](#ex1)
    - [Timer Usage](#ex2)
    - [Stopwatch Usage](#ex3)
    - [Period Usage](#ex4)
    - [Timestamp Usage](#ex5)
- [API Reference](#-api-reference)
- [Best Practices](#-best-practices)

---

## 🗂 Examples of Usage

<div id="ex1"></div>

### 1️⃣ Cooldown Usage

```csharp
// Create a cooldown of 5 seconds
Cooldown cooldown = 5f;

// Subscribe to events
cooldown.OnTimeChanged += time => 
    Console.WriteLine($"Time remaining: {time:F2}s");

cooldown.OnProgressChanged += progress => 
    Console.WriteLine($"Progress: {progress:P0}");

cooldown.OnCompleted += () => 
    Console.WriteLine("Cooldown complete!");

// Simulate a game loop updating the cooldown
float deltaTime = 1f; // 1 second per tick
while (!cooldown.IsCompleted())
{
    cooldown.Tick(deltaTime);
    System.Threading.Thread.Sleep(1000); // wait 1 second
}

// Reset the cooldown to full duration
cooldown.ResetTime();
Console.WriteLine($"Cooldown reset. Time remaining: {cooldown.GetTime()}s");

// Set progress to 50%
cooldown.SetProgress(0.5f);
Console.WriteLine($"Cooldown progress set to 50%, time remaining: {cooldown.GetTime()}s");
```

<div id="ex2"></div>

### 2️⃣ Timer Usage

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

<div id="ex3"></div>

### 3️⃣ Stopwatch Usage

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

<div id="ex4"></div>

### 4️⃣ Period Usage

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

<div id="ex5"></div>

### 5️⃣ Timestamp Usage

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


---

## 🔍 API Reference

The module supports flexible time representations, including fixed and
variable intervals, as well as reactive notifications for state changes.

<details>
  <summary><a href="Sources.md">ISources</a></summary>
  <ul>
    <li><a href="ITimeSource.md">ITimeSource</a></li>
    <li><a href="IDurationSource.md">IDurationSource</a></li>
    <li><a href="ITickSource.md">ITickSource</a></li>
    <li><a href="IStartSource.md">IStartSource</a></li>
    <li><a href="IPauseSource.md">IPauseSource</a></li>
    <li><a href="ICompleteSource.md">ICompleteSource</a></li>
    <li><a href="IProgressSource.md">IProgressSource</a></li>
    <li><a href="IStateSource.md">IStateSource&lt;T&gt;</a></li>
  </ul>
</details>

<details>
  <summary><a href="Cooldowns.md">Cooldowns</a></summary>
  <ul>
    <li><a href="ICooldown.md">ICooldown</a></li>
    <li><a href="Cooldown.md">Cooldown</a></li>
    <li><a href="RandomCooldown.md">RandomCooldown</a></li>
  </ul>
</details>

<details>
  <summary><a href="Timers.md">Timers</a></summary>
  <ul>
    <li><a href="ITimer.md">ITimer</a></li>
    <li><a href="UpTimer.md">UpTimer</a></li>
    <li><a href="DownTimer.md">DownTimer</a></li>
    <li><a href="TimerState.md">TimerState</a></li>
  </ul>
</details>

<details>
  <summary><a href="Stopwatches.md">Stopwatches</a></summary>
  <ul>
    <li><a href="IStopwatch.md">IStopwatch</a></li>
    <li><a href="Stopwatch.md">Stopwatch</a></li>
    <li><a href="StopwatchState.md">StopwatchState</a></li>
  </ul>
</details>

<details>
  <summary><a href="Periods.md">Periods</a></summary>
  <ul>
    <li><a href="IPeriod.md">IPeriod</a></li>
    <li><a href="Period.md">Period</a></li>
    <li><a href="PeriodState.md">PeriodState</a></li>
  </ul>
</details>

<details>
  <summary><a href="Timestamps.md">Timestamps</a></summary>
  <ul>
    <li><a href="ITimestamp.md">ITimestamp</a></li>
    <li><a href="FixedTimestamp.md">FixedTimestamp</a></li>
  </ul>
</details>

<details>
  <summary>Extensions</summary>
  <ul>
    <li><a href="ExtensionsIStartSource.md">IStartSource</a></li>
  </ul>
</details>

---

## 📌 Best Practices

- [Using Cooldowns with Entities](../../BestPractices/UsingCooldownInGameMechanics.md)
- [Cooldown vs Timer](../../BestPractices/ChosingBetweenTimerAndCooldown.md)
