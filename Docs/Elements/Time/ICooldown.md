# 🧩️ ICooldown

Represents a contract of **cooldown timer** that tracks remaining time, provides progress feedback and raises events
when its state changes. The interface combines multiple sources: [ITimeSource](Sources.md/#itimesource), [IDurationSource](Sources.md/#idurationsource), [ITickSource](Sources.md/#iticksource), [IProgressSource](Sources.md/#iprogresssource), [ICompleteSource](Sources.md/#icompletesource)
to provide flexible access to timer data and notifications. It is useful for game mechanics such as ability cooldowns, weapon reloads, and timed delays.

```csharp
public interface ICooldown : IDurationSource, ITimeSource, IProgressSource, ICompleteSource, ITickSource
{
}
```

---

## ⚡ Events

#### `OnTimeChanged`

```csharp
public event Action<float> OnTimeChanged;
```

- **Description:** Raised whenever the current time changes.
- **Parameters:** `float` — the new current time in seconds.

#### `OnDurationChanged`

```csharp
public event Action<float> OnDurationChanged;
```

- **Description:** Invoked when the duration value changes.

#### `OnProgressChanged`

```csharp
public event Action<float> OnProgressChanged;  
```

- **Description:** Raised when the progress changes.

#### `OnCompleted`

```csharp
public event Action OnCompleted;  
```

- **Description:** Invoked when the source has completed.

---

## 🏹 Methods

#### `GetTime()`

```csharp
public float GetTime();
```

- **Description:** Gets the current time from the source.
- **Returns:** `float` — current time in seconds.

#### `SetTime(float)`

```csharp
public void SetTime(float time);
```

- **Description:** Sets the current time.
- **Parameters:**
    - `time` — The new time to set, expected to be in the range `0` to the duration of the source.

#### `ResetTime()`

```csharp
public void ResetTime();  
```

- **Description:** Resets the time source to its initial state.
- **Remarks:** After resetting, the current time will be the initial time, and any listeners may be notified via
  `OnTimeChanged`.

#### `GetDuration()`

```csharp
public float GetDuration();  
```

- **Description:** Gets the total duration.
- **Returns:** The duration in seconds.

#### `SetDuration(float)`

```csharp
public void SetDuration(float duration);  
```

- **Description:** Sets the total duration.
- **Parameter:** `duration` — The new duration value in seconds.

#### `Tick(float)`

```csharp
public void Tick(float deltaTime);  
```

- **Description:** Updates the source by a specified time increment.
- **Parameter:** `deltaTime` — The amount of time (in seconds) to advance the source.
- **Remarks:** This method is typically called repeatedly (e.g., once per frame) to progress time-dependent systems.

#### `GetProgress()`

```csharp
public float GetProgress();  
```

- **Description:** Gets the current progress.
- **Returns:** Normalized progress (0–1).

#### `SetProgress(float)`

```csharp
public void SetProgress(float progress);  
```

- **Description:** Sets the current progress.
- **Parameter:** `progress` — Progress value (0–1).

#### `IsCompleted()`

```csharp
public bool IsCompleted();  
```

- **Description:** Returns whether the source has completed.
- **Returns:** `true` if completed; otherwise `false`.

---

## 🗂 Example of Usage

The following example demonstrates how to use `ICooldown` for spawning coins as game objects in a scene, together with
the `Atomic.Entities` framework.

### 1. Create `CoinSpawnController`

```csharp
public sealed class CoinSpawnController : IEntityInit<IGameContext>, IEntityFixedTick
{
    private ICooldown _cooldown;

    public void Init(IGameContext context)
    {
        _cooldown = context.GetCoinSpawnCooldown();
    }

    public void FixedTick(IEntity entity, float deltaTime)
    {
        _cooldown.Tick(deltaTime);
        if (_cooldown.IsCompleted())
        {
            this.SpawnCoin();
            _cooldown.ResetTime();
        }
    }
    
    // Logic for spawning a coin
    private void SpawnCoin() { ... }
}
```

### 2. Attach `Cooldown` to `GameContextInstaller`

Below we bind the `Cooldown` implementation and attach the coin spawn controller.

```csharp
public sealed class GameContextInstaller : SceneEntityInstaller<IGameContext>
{
    // Using Cooldown as implementation
    [SerializeField] private Cooldown _coinSpawnCooldown = new Cooldown(2);

    public override void Install(IGameContext context)
    {
        // Register cooldown
        context.AddCoinSpawnCooldown(_coinSpawnCooldown);
        
        // Register coin spawn logic
        context.AddBehaviour<CoinSpawnController>();
    }
}
```

### Explanation

- **Cooldown setup:** A `Cooldown` of 2 seconds is created and registered in the game context.
- **Controller logic:** Each `FixedTick`, the cooldown is updated via `Tick(deltaTime)`.
- **Coin spawning:** When the cooldown completes, a new coin is spawned, and the cooldown is reset.