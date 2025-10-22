# 📌 Using Cooldown with Entities

The [Cooldowns](../Elements/Time/Cooldowns.md) provide a simple yet powerful way to control **timed actions** —
such as attacks, abilities, or timers — in your entity systems.
Below are several **real-world usage examples** demonstrating how to integrate cooldowns in different gameplay
scenarios.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
  - [Weapon Shooting](#ex1)
  - [Melee Combat](#ex2)
  - [Game Timer](#ex3)
  - [Coin Spawner](#ex4)
- [Conclusion](#-conclusion)

---

## 🗂 Examples of Usage

<div id="ex1"></div>

### 1️⃣ Weapon Shooting

A basic weapon setup that limits firing rate using [Cooldown](../Elements/Time/Cooldown.md).
Implemented with [Atomic.Entities](../Entities/Manual.md) for event-driven logic.

```csharp
public sealed class WeaponInstaller : SceneEntityInstaller<IWeaponEntity>
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Cooldown _cooldown = 0.5f;

    public override void Install(IWeaponEntity weapon)
    {
        weapon.AddFireAction(new InlineAction(() =>
        {
            // Check cooldown before firing
            if (!_cooldown.IsCompleted())
                return;

            // TODO: Weapon shooting logic
            
            // Reset cooldown after shot
            _cooldown.ResetTime();
        }));
        
        // Tick cooldown each FixedUpdate
        weapon.WhenFixedTick(_cooldown.Tick);
    }

}
```

> 💡 **Tip:**  
> For ticking cooldowns, use the `WhenTick`, `WhenFixedTick`, or
`WhenLateTick` [extensions](../Entities/Lifecycle/Extensions.md) from `Atomic.Entities` 
> to keep updates synchronized with your entity lifecycle.

---

<div id="ex2"></div>

### 2️⃣ Melee Combat

A melee combat setup using cooldowns to regulate attack frequency.

```csharp
[Serializable]
public sealed class MeleeCombatEntityInstaller : IEntityInstaller<IGameEntity>
{
    [SerializeField] private float _fireCooldown = 1;
    [SerializeField] private Const<float> _fireDistance = 1;
    [SerializeField] private Const<int> _damage;

    public void Install(IGameEntity entity)
    {
        entity.AddDamage(_damage);
        entity.AddFireCooldown(new Cooldown(_fireCooldown));
        entity.AddFireRequest(new BaseRequest<IGameEntity>());
        entity.WhenFixedTick(_ =>
        {
            if (HealthUseCase.IsAlive(entity) &&
                entity.GetFireCooldown().IsCompleted() &&
                entity.GetFireRequest().Consume(out IGameEntity target))
            {
                CombatUseCase.DealDamage(entity, target);
                entity.GetFireCooldown().ResetTime();
                entity.GetFireEvent().Invoke(target);
            }
        });
        
        entity.WhenFixedTick(entity.GetFireCooldown().Tick);
        entity.AddFireDistance(_fireDistance);
        entity.AddFireEvent(new BaseEvent<IGameEntity>());
    }

}
```

This pattern ensures that **attack logic**, **cooldown timing**, and **event notifications** remain fully decoupled.

---

<div id="ex3"></div>

### 3️⃣ Game Timer

You can also use `Cooldown` as a **countdown timer** for sessions or time-based events.

#### I. GameCountdownController

Ticks down every frame and triggers `GameOver` when time runs out.

```csharp
public sealed class GameCountdownController : IEntityInit<IGameContext>, IEntityFixedTick
{
    private ICooldown _countdown;
    private IEvent _overEvent;

    public void Init(IGameContext context)
    {
        _countdown = context.GetGameCountdown();
        _overEvent = context.GetGameOverEvent();
    }

    public void FixedTick(IEntity entity, float deltaTime)
    {
        if (_countdown.IsCompleted())
            _overEvent.Invoke();
        else
            _countdown.Tick(deltaTime);
    }
}
```

#### II. GameContextInstaller

Registers the countdown and connects it with the controller.

```csharp
public sealed class GameContextInstaller : SceneEntityInstaller<IGameContext>
{
    [SerializeField] 
    private Cooldown _gameCountdown = 60.0f; // 60-second session

    public override void Install(IGameContext context)
    {
        context.AddGameCountdown(_gameCountdown);
        context.AddGameOverEvent(new BaseEvent());
        context.AddBehaviour<GameCountdownController>();
    }
}
```

#### 3. CountdownPresenter (UI Layer)

Updates the UI with the current remaining time (`MM:SS` format). 
Follows the [MVP Passive View](https://martinfowler.com/eaaDev/PassiveScreen.html) pattern for clean separation.

```csharp
public class CountdownPresenter : IEntityInit<IUIContext>, IEntityDispose
{
    private ICooldown _countdown;
    private CountdownView _view;

    public void Init(IUIContext context)
    {
        _countdown = GameContext.Instance.GetGameCountdown();
        _countdown.OnTimeChanged += this.OnTimeChanged;
        _view = context.GetGameCountdownView();
    }

    public void Dispose(IEntity entity)
    {
        _countdown.OnTimeChanged -= this.OnTimeChanged;
    }

    private void OnTimeChanged(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        _view.SetTime($"{minutes:00}:{seconds:00}");
    }

}
```

---

<div id="ex4"></div>

### 4️⃣ Coin Spawner

A simple example showing how to use a cooldown to **spawn coins periodically**.

#### I. CoinSpawnController

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

#### II. GameContextInstaller

Attach the cooldown and register the controller to the game context.

```csharp
public sealed class GameContextInstaller : SceneEntityInstaller<IGameContext>
{
    [SerializeField]
    private Cooldown _coinSpawnCooldown = new Cooldown(2); // spawn every 2 seconds

    public override void Install(IGameContext context)
    {
        context.AddCoinSpawnCooldown(_coinSpawnCooldown);
        context.AddBehaviour<CoinSpawnController>();
    }
}
```

---

## 🏁 Conclusion

- `Cooldown` can represent **delays**, **timers**, or **cooldown logic** for actions.
- It integrates seamlessly with the `Atomic.Entities` lifecycle.
- Works perfectly for:
    - Weapon firing intervals
    - Attack delays
    - Respawn or spawn systems
    - Global or local game timers

> Use `Cooldown` wherever you need **timed, repeatable logic** that remains clean, consistent, and frame-synchronized.










<!--

# 📌 Using Cooldown with Entities

Below are real-world examples of using the `Cooldown` class in different gameplay scenarios.

### Example #1: Weapon Shooting

Cooldown for a weapon that shoots bullets. Implemented with `Atomic.Entities` to handle the firing logic.

```csharp
public sealed class WeaponInstaller : SceneEntityInstaller<IWeaponEntity>
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Cooldown _cooldown = 0.5f;

    public override void Install(IWeaponEntity weapon)
    {
        weapon.AddFireAction(new InlineAction(() =>
        {
            // Check cooldown before firing
            if (!_cooldown.IsCompleted())
                return;

            // TODO: weapon shooting logic
            
            // Reset cooldown after shot
            _cooldown.ResetTime();
        }));
        
        // Tick cooldown each FixedUpdate
        weapon.WhenFixedTick(_cooldown.Tick);
    }
}
```

> [!TIP]  
> For ticking cooldowns it’s convenient to use `WhenTick`/`WhenFixedTick`/`WhenLateTick` extensions for `IEntity` from
`Atomic.Entities` to synchronize updates.

---

### Example #2: Melee Combat

The same approach works for melee attacks.  
Here is a more complete combat setup with cooldown control.

```csharp
[Serializable]
public sealed class MeleeCombatEntityInstaller : IEntityInstaller<IGameEntity>
{
    [SerializeField] private float _fireCooldown = 1;
    [SerializeField] private Const<float> _fireDistance = 1;
    [SerializeField] private Const<int> _damage;
    
    public void Install(IGameEntity entity)
    {
        entity.AddDamage(_damage);
        entity.AddFireCooldown(new Cooldown(_fireCooldown));
        entity.AddFireRequest(new BaseRequest<IGameEntity>());
        entity.WhenFixedTick(_ =>
        {
            if (LifeUseCase.IsAlive(entity) &&
                entity.GetFireCooldown().IsCompleted() &&
                entity.GetFireRequest().Consume(out IGameEntity target))
            {
                DamageUseCase.DealDamage(entity, target);
                entity.GetFireCooldown().ResetTime();
                entity.GetFireEvent().Invoke(target);
            }
        });
        
        entity.WhenFixedTick(entity.GetFireCooldown().Tick);
        entity.AddFireDistance(_fireDistance);
        entity.AddFireEvent(new BaseEvent<IGameEntity>());
    }
}
```

---

### Example #3: Game Timer

Using `Cooldown` as a countdown timer for game sessions.

#### 1. Countdown Controller

Reduces the countdown each frame (`FixedTick`).  
When the timer reaches zero, the `GameOver` event is triggered.

```csharp
public sealed class GameCountdownController : IEntityInit<IGameContext>, IEntityFixedTick
{
    private ICooldown _countdown;
    private IEvent _overEvent;

    public void Init(IGameContext context)
    {
        _countdown = context.GetGameCountdown();
        _overEvent = context.GetGameOverEvent();
    }

    public void FixedTick(IEntity entity, float deltaTime)
    {
        if (_countdown.IsCompleted())
            _overEvent.Invoke();
        else
            _countdown.Tick(deltaTime);
    }
}
```

#### 2. Countdown Installing

The `Cooldown` is initialized with `60` seconds.  
The countdown and `GameOverEvent` are registered in the game context, and the controller is attached.

```csharp
public sealed class GameContextInstaller : SceneEntityInstaller<IGameContext>
{
    [SerializeField] private Cooldown _gameCountdown = 60.0f; // 60 seconds game session

    public override void Install(IGameContext context)
    {
        context.AddGameCountdown(_gameCountdown);
        context.AddGameOverEvent(new BaseEvent());
        context.AddBehaviour<GameCountdownController>();
    }
}
```

#### 3. Countdown Visualization

`CountdownPresenter` subscribes to the `OnTimeChanged` event and updates the UI view with the current time in `MM:SS`
format.  
Thanks to the [MVP Passive View](https://martinfowler.com/eaaDev/PassiveScreen.html) pattern, the UI stays simple and
reactive.

```csharp
public class CountdownPresenter : IEntityInit<IUIContext>, IEntityDispose
{
    private ICooldown _countdown;
    private CountdownView _view;

    public void Init(IUIContext context)
    {
        _countdown = GameContext.Instance.GetGameCountdown();
        _countdown.OnTimeChanged += this.OnTimeChanged;
        _view = context.GetGameCountdownView();
    }

    public void Dispose(IEntity entity)
    {
        _countdown.OnTimeChanged -= this.OnTimeChanged;
    }

    private void OnTimeChanged(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        _view.SetTime($"{minutes:00}:{seconds:00}");
    }
}
```


## 🗂 Example of Usage

The following example demonstrates how to use **cooldown** for spawning coins as game objects in a scene, together with
the `Atomic.Entities` framework.


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

-->