# 🧩️ Cooldown

`Cooldown` represents a cooldown timer that tracks remaining time,  
provides progress feedback (0–1), and raises events when its state changes.  

> [!NOTE]
> It is useful for game mechanics such as ability cooldowns, weapon reloads, and timed delays.

---

Defines the **contract** for a cooldown timer.

> The interface combines multiple sources (`IDurationSource`, `ITimeSource`, `IProgressSource`, `ICompleteSource`, `ITickSource`)  
to provide flexible access to timer data and notifications.

### Events

- `event Action<float> OnDurationChanged` – invoked when the total duration changes.
- `event Action<float> OnTimeChanged` – invoked when the current remaining time changes.
- `event Action<float> OnProgressChanged` – invoked when the progress (0–1) changes.
- `event Action OnCompleted` – invoked when the cooldown has finished (time reaches zero).

### Methods

- `float GetDuration()` – returns the total duration.
- `void SetDuration(float duration)` – sets the total duration.

- `float GetTime()` – returns the current remaining time.
- `void SetTime(float time)` – sets the current remaining time (0–duration).
- `void ResetTime()` – resets time back to full duration.

- `float GetProgress()` – returns progress from 0 to 1.
- `void SetProgress(float progress)` – sets progress, updating remaining time accordingly.

- `bool IsCompleted()` – returns `true` if time has expired.
- `void Tick(float deltaTime)` – decreases time by `deltaTime`, raises events if necessary.

---

## Cooldown

The `Cooldown` class implements `ICooldown`.  
It is a fixed-duration timer, starting at full or partial time.

### Constructors

- `Cooldown(float duration)` – creates a cooldown with the given duration, starting at full time.
- `Cooldown(float duration, float current)` – creates a cooldown with the given duration and current remaining time.
- `static implicit operator Cooldown(float duration)` – implicit conversion from `float`.

### Example of Usage

```csharp
var cooldown = new Cooldown(5f); // 5 seconds

cooldown.OnCompleted += () => Console.WriteLine("Cooldown finished!");
cooldown.OnProgressChanged += p => Console.WriteLine($"Progress: {p:P0}");

// simulate game loop
for (int i = 0; i < 6; i++)
{
    cooldown.Tick(1f);
    Console.WriteLine($"Time left: {cooldown.GetTime()}s");
}
```
## RandomCooldown
`RandomCooldown` is a variant of `Cooldown` where the duration is chosen randomly
within the range `[minDuration, maxDuration]`.
### Constructor
- `RandomCooldown(float minDuration, float maxDuration)` – creates a cooldown with random duration in the given range.
### Behavior
- On `ResetTime()`, the duration is randomized again within `[minDuration, maxDuration]`.
- Supports all methods and events from `ICooldown`.
### Example of Usage
```csharp
var randomCooldown = new RandomCooldown(3f, 7f);

randomCooldown.OnCompleted += () => Console.WriteLine("Random cooldown expired!");

randomCooldown.ResetTime();
while (!randomCooldown.IsCompleted())
{
    randomCooldown.Tick(1f);
    Console.WriteLine($"Time left: {randomCooldown.GetTime()}s");
}
```