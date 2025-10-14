# 🧩 Cooldowns

Represent a **family of cooldown timers**. It tracks remaining time, provides progress feedback and raises events
when its state changes. It is useful for game mechanics such as ability cooldowns, weapon reloads, and timed delays.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
    - [Cooldown Usage](#ex1)
    - [Random Cooldown Usage](#ex2)
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

### 2️⃣ Random Cooldown Usage

```csharp
// Create a random cooldown between 2 and 5 seconds
ICooldown cooldown = new RandomCooldown(2f, 5f);

cooldown.OnTimeChanged += t => Console.WriteLine($"Time: {t:F2}s");
cooldown.OnProgressChanged += p => Console.WriteLine($"Progress: {p:P0}");
cooldown.OnCompleted += () => Console.WriteLine("Cooldown complete!");

// Simulate ticking
float deltaTime = 1f;
while (!cooldown.IsCompleted())
{
    cooldown.Tick(deltaTime);
    System.Threading.Thread.Sleep(1000);
}

// Reset to new random duration
cooldown.ResetTime();
Console.WriteLine($"Cooldown reset. Duration: {cooldown.GetDuration():F2}s");
```

---

## 🔍 API Reference

There are an interface and two implementations of the timer.

- [ICooldown](ICooldown.md)
- [Cooldown](Cooldown.md)
- [RandomCooldown](RandomCooldown.md)

---

## 📌 Best Practices

- [Using Cooldowns with Entities](../../BestPractices/UsingCooldownInGameMechanics.md)
- [Cooldown vs Timer](../../BestPractices/ChosingBetweenTimerAndCooldown.md)