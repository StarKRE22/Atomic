
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