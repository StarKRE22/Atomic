# 🧩 Cooldown

`Cooldown` is a concrete implementation of the [ICooldown](ICooldown.md) interface that represents a **cooldown timer**. It tracks the remaining time, provides normalized progress, and raises events when its state changes.

> [!IMPORTANT]  
> Useful for game mechanics such as ability cooldowns, weapon reloads, or timed delays.

The class combines multiple sources internally: [ITimeSource](Sources.md/#itimesource), [IDurationSource](Sources.md/#idurationsource), [ITickSource](Sources.md/#iticksource), [IProgressSource](Sources.md/#iprogresssource), [ICompleteSource](Sources.md/#icompletesource).

---

## Constructors

#### `Cooldown()`
```csharp
public Cooldown();
```
- **Description:** Initializes a new instance of the `Cooldown` class with default values.
- **Remarks:** Duration defaults to `0` and remaining time is `0`. The cooldown must be set or reset before use.

#### `Cooldown(float duration)`
```csharp
public Cooldown(float duration);
```
- **Description:** Initializes a new instance of the `Cooldown` class with a specified duration.
- **Parameter:** `duration` — total duration of the cooldown in seconds.
- **Remarks:** The remaining time is initialized to the full duration.

---

## Events

#### `event Action<float> OnTimeChanged`
```csharp
public event Action<float> OnTimeChanged;
```
- **Description:** Invoked whenever the current remaining time changes.
- **Parameters:** `float` — the new remaining time in seconds.

#### `event Action<float> OnDurationChanged`
```csharp
public event Action<float> OnDurationChanged;
```
- **Description:** Invoked whenever the total duration changes.
- **Parameters:** `float` — the new total duration in seconds.

#### `event Action<float> OnProgressChanged`
```csharp
public event Action<float> OnProgressChanged;
```
- **Description:** Raised when the normalized progress changes.
- **Parameters:** `float` — the current progress (0–1).

#### `event Action OnCompleted`
```csharp
public event Action OnCompleted;
```
- **Description:** Invoked when the cooldown has finished (remaining time reaches zero).

---

## Methods

#### `float GetTime()`
```csharp
public float GetTime();
```
- **Description:** Returns the current remaining time of the cooldown.
- **Returns:** `float` — remaining time in seconds.

#### `void SetTime(float time)`
```csharp
public void SetTime(float time);
```
- **Description:** Sets the current remaining time.
- **Parameters:** `time` — new time to set, clamped between `0` and the total duration.
- **Notes:** Invokes `OnTimeChanged` and `OnProgressChanged` if the value changes.

#### `void ResetTime()`
```csharp
public void ResetTime();
```
- **Description:** Resets the cooldown to its full duration.

#### `float GetDuration()`
```csharp
public float GetDuration();
```
- **Description:** Returns the total duration of the cooldown.
- **Returns:** `float` — total duration in seconds.

#### `void SetDuration(float duration)`
```csharp
public void SetDuration(float duration);
```
- **Description:** Sets a new total duration.
- **Parameters:** `duration` — new duration value.
- **Notes:** Invokes `OnDurationChanged` and `OnProgressChanged` if the value changes.

#### `void Tick(float deltaTime)`
```csharp
public void Tick(float deltaTime);
```
- **Description:** Advances the cooldown by a given time increment, reducing remaining time.
- **Parameters:** `deltaTime` — time to subtract from the current remaining time.
- **Notes:** Invokes `OnTimeChanged`, `OnProgressChanged`, and `OnCompleted` if the cooldown expires.

#### `float GetProgress()`
```csharp
public float GetProgress();
```
- **Description:** Returns the normalized progress of the cooldown.
- **Returns:** `float` — progress between 0 and 1.

#### `void SetProgress(float progress)`
```csharp
public void SetProgress(float progress);
```
- **Description:** Sets the normalized progress (0–1), updating remaining time accordingly.
- **Parameters:** `progress` — new progress value between 0 and 1.
- **Notes:** Invokes `OnTimeChanged` and `OnProgressChanged`.

#### `bool IsCompleted()`
```csharp
public bool IsCompleted();
```
- **Description:** Returns whether the cooldown has finished.
- **Returns:** `true` if remaining time is zero; otherwise `false`.

#### `override string ToString()`
```csharp
public override string ToString();
```
- **Description:** Returns a string representation of the cooldown's state.
- **Returns:** Formatted string showing `duration` and `remaining time`.

---

## Operators

#### `public static implicit operator Cooldown(float)`
```csharp
public static implicit operator Cooldown(float duration) => new(duration);
```
- **Description:** Allows implicit conversion from a `float` to a `Cooldown`.
- **Parameter:** `duration` — The duration value in seconds.
- **Returns:** A new instance of `Cooldown` initialized with the given `duration`.
- **Usage Example:**  

  ```csharp
  Cooldown cooldown = 5f; // creates a Cooldown with duration = 5 seconds
  ```

#### `public static implicit operator Cooldown(int)`
```csharp
public static implicit operator Cooldown(int duration) => new(duration);
```
- **Description:** Allows implicit conversion from a `int` to a `Cooldown`.
- **Parameter:** `duration` — The duration value in seconds.
- **Returns:** A new instance of `Cooldown` initialized with the given `duration`.
- **Usage Example:**

  ```csharp
  Cooldown cooldown = 3; // creates a Cooldown with duration = 3 seconds
  ```
  
---

## 🗂 Example of Usage

```csharp
// Create a cooldown of 5 seconds
ICooldown cooldown = new Cooldown(5f);

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

---

## 📌 Best Practice
Below are real-world examples of using the `Cooldown` class in different gameplay scenarios.

### 🗂 Example #1: Weapon Shooting

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
> For ticking cooldowns it’s convenient to use `WhenTick`/`WhenFixedTick`/`WhenLateTick` extensions for `IEntity` from `Atomic.Entities` to synchronize updates.

---

### 🗂 Example #2: Melee Combat

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

### 🗂 Example #3: Game Timer

Using `Cooldown` as a countdown timer for game sessions.

#### 🔹 Countdown Controller
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

#### 🔹 Countdown Installing
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
#### 🔹 Countdown Visualization
`CountdownPresenter` subscribes to the `OnTimeChanged` event and updates the UI view with the current time in `MM:SS` format.  
Thanks to the [MVP Passive View](https://martinfowler.com/eaaDev/PassiveScreen.html) pattern, the UI stays simple and reactive.

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