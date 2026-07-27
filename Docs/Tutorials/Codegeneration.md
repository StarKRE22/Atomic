# 📖 Code Generation Walkthrough

This tutorial walks through generating type-safe extension methods for entity tags/values and event-bus keys using the
Atomic Roslyn source generators.

For full reference, see:

- [Code Generation Manual](../CodeGeneration/Manual.md)
- [Code Generation Setup](../CodeGeneration/Setup.md)
- [Entity API Generator](../CodeGeneration/EntityAPI/EntityAPIGenerator.md)
- [Event API Generator](../CodeGeneration/EventAPI/EventAPIGenerator.md)

---

## 📑 Table of Contents

- [Prerequisites](#-prerequisites)
- [Step 1: Set Up the Generators](#-step-1-set-up-the-generators)
- [Step 2: Declare Entity Keys](#-step-2-declare-entity-keys)
- [Step 3: Use the Generated Entity API](#-step-3-use-the-generated-entity-api)
- [Step 4: Declare Event Keys](#-step-4-declare-event-keys)
- [Step 5: Use the Generated Event API](#-step-5-use-the-generated-event-api)
- [Step 6: Fix Missing Initializers](#-step-6-fix-missing-initializers)

---

## 📝 Prerequisites

- Unity 6 (6000.0 LTS or newer)
- Atomic.Entities and Atomic.Events referenced in your project
- The four source-generator/analyzer DLLs added to `Assets/Plugins/Atomic/SourceGenerators/`

See [Code Generation Setup](../CodeGeneration/Setup.md) if you have not configured the DLLs yet.

---

## Step 1: Set Up the Generators

Place the following assemblies in `Assets/Plugins/Atomic/SourceGenerators/`:

```
EntityAPIGenerator.dll
EntityAPIAnalyzer.dll
EventAPIGenerator.dll
EventAPIAnalyzer.dll
```

For each DLL:

1. Select it in the Project window.
2. Add the asset label `RoslynAnalyzer`.
3. Uncheck **Any Platform** and every individual platform in the Inspector.
4. Click **Apply**.

Restart Unity. The generators are now ready.

---

## Step 2: Declare Entity Keys

Create a static partial class for the character API:

```csharp
using Atomic.Entities;
using UnityEngine;

[EntityAPI]
public static partial class CharacterAPI
{
    // Tags
    public static readonly TagKey<IEntity> Player = new(nameof(Player));
    public static readonly TagKey<IEntity> Enemy = new(nameof(Enemy));

    // Values
    public static readonly ValueKey<IEntity, int> Health = new(nameof(Health));
    public static readonly ValueKey<IEntity, float> Speed = new(nameof(Speed));
    public static readonly ValueKey<IEntity, Vector3> Position = new(nameof(Position));
}
```

Save the file. After the next compilation, extension methods for these keys are generated automatically.

---

## Step 3: Use the Generated Entity API

In a behaviour or installer, use the generated methods instead of raw ids:

```csharp
public sealed class CharacterInstaller : IEntityInit
{
    public void Init(IEntity entity)
    {
        entity.AddPlayerTag();
        entity.AddHealth(100);
        entity.AddSpeed(5.0f);
        entity.AddPosition(Vector3.zero);
    }
}
```

```csharp
public sealed class MovementBehaviour : IEntityTick
{
    public void Tick(IEntity entity, float deltaTime)
    {
        Vector3 position = entity.GetPosition();
        float speed = entity.GetSpeed();

        entity.SetPosition(position + Vector3.forward * speed * deltaTime);
    }
}
```

---

## Step 4: Declare Event Keys

Create another static partial class for gameplay events:

```csharp
using Atomic.Events;

[EventAPI]
public static partial class GameEventAPI
{
    public static readonly EventKey<IEventBus> PlayerTurnStarted = new(nameof(PlayerTurnStarted));
    public static readonly EventKey<IEventBus, IEntity> EntityDamaged = new(nameof(EntityDamaged));
}
```

Save the file. Event-bus extension methods are generated on the next compilation.

---

## Step 5: Use the Generated Event API

Subscribe and invoke using the generated methods:

```csharp
public sealed class DamagePresenter
{
    private readonly IEventBus _eventBus;

    public DamagePresenter(IEventBus eventBus)
    {
        _eventBus = eventBus;
        _eventBus.SubscribeEntityDamaged(OnEntityDamaged);
    }

    private void OnEntityDamaged(IEntity entity)
    {
        Debug.Log($"Entity damaged: {entity}");
    }
}
```

```csharp
public sealed class DealDamageUseCase
{
    private readonly IEventBus _eventBus;

    public DealDamageUseCase(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public void Execute(IEntity target, int damage)
    {
        int health = target.GetHealth();
        target.SetHealth(Mathf.Max(0, health - damage));

        _eventBus.InvokeEntityDamaged(target);
    }
}
```

---

## Step 6: Fix Missing Initializers

If you forget to initialize a key field, the analyzer reports a build error:

```csharp
[EntityAPI]
public static partial class CharacterAPI
{
    // EAPI0001: field has no initializer
    public static readonly ValueKey<IEntity, int> Health;
}
```

Use the quick fix (Ctrl+. or Alt+Enter) to insert:

```csharp
public static readonly ValueKey<IEntity, int> Health = new(nameof(Health));
```

The same applies to event keys and parameterless constructors (`EAPI0002`).

---

## ✅ Result

You now have:

- Type-safe entity tag/value extension methods via `[EntityAPI]`
- Type-safe event-bus extension methods via `[EventAPI]`
- Build-time validation of key initializers via analyzers

For more advanced options such as unsafe mode, aggressive inlining, and detailed setup, see the
[Code Generation Manual](../CodeGeneration/Manual.md).
