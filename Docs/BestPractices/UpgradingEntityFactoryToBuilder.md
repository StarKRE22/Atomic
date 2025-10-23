# 📌 Upgrading EntityFactory to the Builder

Entity Factories can also act as **builders**, allowing step-by-step configuration before creation.
This approach is especially useful when entities require **external dependencies** or **custom initialization**.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
  - [Builder Pattern](#ex1)
  - [Builder Pattern for Singleton](#ex2)
- [Summary](#-summary)

---

## 🗂 Examples of Usage

<div id="ex1"></div>

### 1️⃣ Builder Pattern 

A `Builder` version of an entity factory lets you configure parameters using fluent `Set...` methods before calling
`Create()`.

```csharp
public sealed class PlayerContextBuilder : IEntityFactory<PlayerContext>
{
    private GameContext _gameContext;
    private TeamType _teamType;
    private Camera _camera;

    public PlayerContextBuilder SetGameContext(GameContext gameContext)
    {
        _gameContext = gameContext;
        return this;
    }

    public PlayerContextBuilder SetTeamType(TeamType teamType)
    {
        _teamType = teamType;
        return this;
    }
    
    public PlayerContextBuilder SetCamera(Camera camera)
    {
        _camera = camera;
        return this;
    }

    public override PlayerContext Create()
    {
        if (_camera == null)
            throw new InvalidOperationException("Camera must be set before creating PlayerContext.");

        if (_teamType == default)
            throw new InvalidOperationException("TeamType must be set before creating PlayerContext.");

        var playerContext = new PlayerContext();
        playerContext.AddValue("TeamType", _teamType);
        playerContext.AddValue("GameContext", _gameContext);
        playerContext.AddValue("Camera", _camera);
        return playerContext;
    }
}
```

Usage example:

```csharp
var playerContext = new PlayerContextBuilder()
  .SetGameContext(gameContext)
  .SetTeamType(teamType)
  .SetCamera(camera)
  .Create();
```

> 💡 In this pattern, the factory provides `Set...` methods to configure dependencies,  
> and the final `Create()` call produces a **fully initialized** `PlayerContext`.

---

<div id="ex2"></div>

### 2️⃣ Builder Pattern for Singleton

You can also apply the builder pattern to singleton-style factories — useful for centralized systems like `GameContext`.

```csharp
public class GameContextBuilder : IEntityFactory<GameContext>
{
    private string _name = "DefaultGame";
    private int _tagCapacity = 4;
    private int _valueCapacity = 8;
    private int _behaviourCapacity = 4;

    public GameContextBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public GameContextBuilder WithTagCapacity(int capacity)
    {
        _tagCapacity = capacity;
        return this;
    }

    public GameContextBuilder WithValueCapacity(int capacity)
    {
        _valueCapacity = capacity;
        return this;
    }

    public GameContextBuilder WithBehaviourCapacity(int capacity)
    {
        _behaviourCapacity = capacity;
        return this;
    }

    public GameContext Create()
    {
        return new GameContext(_name, _tagCapacity, _valueCapacity, _behaviourCapacity);
    }

}
```

Registering the Builder

```csharp
var builder = new GameContextBuilder()
    .WithName("GameContext")
    .WithTagCapacity(16)
    .WithValueCapacity(32)
    .WithBehaviourCapacity(8);

GameContext.SetFactory(builder);
```

Using the Context

```csharp
GameContext context = GameContext.Instance;

context.AddValue("Score", 42);
context.AddBehaviour<EnemySpawnBehaviour>();
context.Init();
```

---

## ✅ Summary

- **Factories** handle simple, immediate creation.
- **Builders** extend factories with configuration steps for more flexible and testable initialization.
- This pattern helps you:
    - Avoid constructor overload chaos.
    - Support dependency injection.
    - Keep your entity creation process explicit and safe.

<!--




# 📌 Upgrading Entity Factory to Builder

Entity Factories can also act like **builders**, allowing configuration before creation.  
This is useful when the entity requires external dependencies or custom initialization.

```csharp
public sealed class PlayerContextBuilder : IEntityFactory<PlayerContext>
{
    private GameContext _gameContext;
    private TeamType _teamType;
    private Camera _camera;

    public PlayerContextBuilder SetGameContext(GameContext gameContext)
    {
        _gameContext = gameContext; return this;
    }

    public PlayerContextBuilder SetTeamType(TeamType teamType)
    {
        _teamType = teamType; return this;
    }
    
   public PlayerContextBuilder SetCamera(Camera camera)
    {
        _camera = camera; return this;
    }

    public override PlayerContext Create()
    {
        if (_camera == null)  
            throw new InvalidOperationException("Camera must be set before creating PlayerContext.");  

        if (_teamType == default)  
            throw new InvalidOperationException("TeamType must be set before creating PlayerContext.");  

        var playerContext = new PlayerContext();
        playerContext.AddValue<TeamType>("TeamType", _teamType);
        playerContext.AddValue<GameContext>("GameContext", _gameContext);
        playerContext.AddValue<Camera>("Camera", _camera);
        return playerContext;
    }
}
```

```csharp
//You can create a lot of players with different settings
var playerContext = new PlayerContextBuilder()
    .SetGameContext(gameContext)
    .SetTeamType(teamType)
    .SetCamera(camera)
    .Create();
```

> In this pattern, the factory provides `Set...` methods to configure parameters,  
> and the final `Create()` produces a fully prepared `IPlayerContext`.



Also you can adjust it for the singletons


### 3️⃣ Upgrading factory to builder

```csharp
// Builder-based factory implementation
public class GameContextBuilder : IEntityFactory<GameContext>
{
    private string _name = "DefaultGame";
    private int _tagCapacity = 4;
    private int _valueCapacity = 8;
    private int _behaviourCapacity = 4;

    public GameContextBuilder WithName(string name)
    {
        _name = name; return this;
    }

    public GameContextBuilder WithTagCapacity(int capacity)
    {
        _tagCapacity = capacity; return this;
    }

    public GameContextBuilder WithValueCapacity(int capacity)
    {
        _valueCapacity = capacity; return this;
    }

    public GameContextBuilder WithBehaviourCapacity(int capacity)
    {
        _behaviourCapacity = capacity; return this;
    }

    public GameContext Create() 
    {
        return new GameContext(_name, _tagCapacity, _valueCapacity, _behaviourCapacity);
    }
}
```

```csharp
// Register before usage
var builder = new GameContextBuilder()
    .WithName("GameContext")
    .WithTagCapacity(16)
    .WithValueCapacity(32)
    .WithBehaviourCapacity(8);
    
GameContext.SetFactory(builder);
```

```csharp
// Usage
GameContext context = GameContext.Instance;

context.AddValue("Score", 42);
context.AddBehaviour<EnemySpawnBehaviour>();
context.Init();
```
-->