# 📖 Interaction Between Entities

In game development, it’s often necessary to organize interaction between objects and systems.

Let’s imagine we’re developing a multiplayer shooter where we need to implement a scoring mechanic for defeating
enemies. Here’s the situation: a bullet hits an enemy — we need to apply damage and check whether the enemy is dead. If
the character dies, a kill event is sent to the GameContext, which then updates the leaderboard score.

With the atomic approach, interaction between entities can be implemented using **procedural programming**. It’s enough
to
simply create a static method that describes the interaction logic, with all required information passed through its
arguments.

Below is an example of a damage application mechanic implemented through procedural programming:

```csharp
public static class CombatUseCase
{
    public static bool DealDamage(
        IGameEntity instigator,
        IGameEntity victim, 
        IGameContext gameContext,
        int damage 
    )
    {
        IVariable<int> health = victim.GetHealth();
        if (health.Value <= 0)
            return false;
      
        health.Value = Math.Max(0, health.Value - damage);  
        victim.GetTakeDamageEvent().Invoke(instigator, damage);
        
        if (health.Value == 0)
            gameContext.GetKillEvent().Invoke(instigator, victim);
        
        return true;
    }
}
```

In this example, you can see that interaction between entities occurs through a **single algorithm** that connects them.
The
entities are not directly dependent on each other — they are simply passed as arguments to the method.

If you need to redefine the type of the `victim` argument in the **Collider**, procedural programming allows you to use
method overloading, enabling you to reuse existing logic while extending functionality within the project.

```csharp
public static class CombatUseCase 
{
    // Overloaded method
    public static bool DealDamage(
       IGameEntity instigator, 
       Collider victim,
       IGameContext gameContext,
       int damage
    ) 
    {
       return victim.TryGetComponent(out IGameEntity target) &&
             DealDamage(instigator, target, gameContext, damage);
    }

    // Original method, written earlier
    public static bool DealDamage(
        IGameEntity instigator,
        IGameEntity victim, 
        IGameContext gameContext,
        int damage
    ) 
    {
        // ...
    }
}
```

Next, we can use the `CombatUseCase.DealDamage` method inside the bullet behaviour, which handles collisions with other
game objects (line 37):

Since the `CombatUseCase.DealDamage` method triggers a kill event in the `GameContext` (line 18), that event can then be
handled in the `LeaderboardController`:

```csharp
public sealed class LeaderboardController : 
    IEntityInit<IGameContext>, 
    IEntityDispose
{
    private ISignal<KillArgs> _killEvent;
    private IGameContext _gameContext;

    public void Init(IGameContext context)
    {
        _gameContext = context;
        _killEvent = context.GetKillEvent();
        _killEvent.Subscribe(this.OnKill);
    }

    public void Dispose(IEntity entity)
    {
        _killEvent.Unsubscribe(this.OnKill);
    }

    private void OnKill(KillArgs args)
    {
        LeaderboardUseCase.AddScore(_gameContext, args);
    }
}
```

In the controller, we can see that another static method — implemented as a UseCase (line 22) — is used to handle score
updates.

Thus, procedural programming eliminates the need to design complex object architectures. It is easy to test, reusable,
follows the single-responsibility principle, and keeps the code clean and understandable.

> [!TIP]
> To minimize code duplication, each behaviour should delegate all business logic to static methods placed inside
> static classes.

---

<p align="center">
<a href="MinimizingUnity.md">Move Next</a> •
<a href="https://github.com/StarKRE22/Atomic/issues">Report Issue</a> •
<a href="https://github.com/StarKRE22/Atomic/discussions">Join Discussion</a>
</p>