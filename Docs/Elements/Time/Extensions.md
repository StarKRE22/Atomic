# 🧩️ IStartSource Extensions

These extension methods provide convenient ways to **restart** timers, countdowns, or other objects implementing `IStartSource`.

---

## Methods

### `Restart(this IStartSource source, float time)`
Stops the source and restarts it from the specified time.
```csharp
timer.Restart(5f); // stops and starts from 5 seconds
```

### `Restart(this IStartSource source)`
Stops the source and restarts it from its default start time.
```csharp
timer.Restart(); // stops and starts from default
```