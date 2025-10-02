# 🧩 Cooldowns

Represent a **family of cooldown timers**. It tracks remaining time, provides progress feedback and raises events
when its state changes. It is useful for game mechanics such as ability cooldowns, weapon reloads, and timed delays.

There are an interface and two implementations of the timer.

- [ICooldown](ICooldown.md)
- [Cooldown](Cooldown.md)
- [RandomCooldown](RandomCooldown.md)

---

## 🗂 Example of Usage

The following example demonstrates how to use **cooldown** for spawning coins as game objects in a scene, together with
the `Atomic.Entities` framework.

---

#### 1. Create `CoinSpawnController`

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

#### 2. Attach `Cooldown` to `GameContextInstaller`

Below we bind the `Cooldown` implementation and attach the coin spawn controller.

```csharp
public sealed class GameContextInstaller : SceneEntityInstaller<IGameContext>
{
    // Using Cooldown as implementation
    [SerializeField] 
    private Cooldown _coinSpawnCooldown = new Cooldown(2);

    public override void Install(IGameContext context)
    {
        // Register cooldown
        context.AddCoinSpawnCooldown(_coinSpawnCooldown);
        
        // Register coin spawn logic
        context.AddBehaviour<CoinSpawnController>();
    }
}
```