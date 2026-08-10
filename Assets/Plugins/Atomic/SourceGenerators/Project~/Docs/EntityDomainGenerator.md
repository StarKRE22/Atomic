# 🧩 Entity Domain Source Generator

The **Entity Domain Source Generator** is a Roslyn incremental source generator that reads `[EntityDomainBehaviours(typeof(TEntity))]` on any class and emits strongly-typed domain behaviour interfaces for the target entity type.

It replaces the boilerplate of manually creating `I*Behaviour`, `I*Tick`, `I*Init`, and other lifecycle interfaces for every domain entity in your project.

---

## 📑 Table of Contents

- [Requirements](#-requirements)
- [Setup](#-setup)
- [Basic Usage](#-basic-usage)
  - [Declaring a Domain](#declaring-a-domain)
  - [Implementing Generated Behaviours](#implementing-generated-behaviours)
- [Generated Code](#-generated-code)
- [Configuration](#-configuration)
- [Troubleshooting](#-troubleshooting)
- [Implementation Notes](#-implementation-notes)

---

## 📝 Requirements

- **Unity 6** (6000.0 LTS or newer) with source-generator support
- The **EntityDomainGenerator.dll** analyzer added to your Unity project (see [Setup.md](Setup.md))
- The `[EntityDomainBehaviours]` attribute from [Atomic.Entities](https://github.com/StarKRE22/Atomic/blob/main/Assets/Plugins/Atomic/Entities/Scripts/Codegen/EntityDomainBehavioursAttribute.cs)

---

## 🔧 Setup

For build, deploy, and Unity import instructions, see the shared [Setup.md](Setup.md).

---

## 🧩 Basic Usage

### Declaring a Domain

Create any `public` class, mark it with `[EntityDomainBehaviours(typeof(IYourEntity))]`, and the generator emits lifecycle interfaces in the same namespace:

```csharp
using Atomic.Entities;

namespace Game.Domain
{
    public interface IPlayer : IEntity
    {
    }

    [EntityDomainBehaviours(typeof(IPlayer))]
    public static class PlayerDomain
    {
    }
}
```

The class can be `static` or instance; its only purpose is to carry the attribute and provide the namespace for generated interfaces.

### Implementing Generated Behaviours

After compilation, implement the generated interfaces like any other behaviour:

```csharp
namespace Game.Domain
{
    public sealed class PlayerMoveBehaviour : IPlayerTick
    {
        public void Tick(IPlayer player, float deltaTime)
        {
            // update logic
        }
    }

    public sealed class PlayerInitBehaviour : IPlayerInit
    {
        public void Init(IPlayer player)
        {
            // setup logic
        }
    }
}
```

Because `IPlayerTick` inherits from `IEntityTick<IPlayer>` and `IPlayerBehaviour`, the behaviour can be added to any entity of type `IPlayer` and will be invoked by the entity's lifecycle systems.

---

## 🔍 Generated Code

For `[EntityDomainBehaviours(typeof(IPlayer))]` the generator emits one C# file containing:

```csharp
public interface IPlayerBehaviour : IEntityBehaviour
{
}

public interface IPlayerTick : IEntityTick<IPlayer>, IPlayerBehaviour
{
}

public interface IPlayerFixedTick : IEntityFixedTick<IPlayer>, IPlayerBehaviour
{
}

public interface IPlayerLateTick : IEntityLateTick<IPlayer>, IPlayerBehaviour
{
}

public interface IPlayerInit : IEntityInit<IPlayer>, IPlayerBehaviour
{
}

public interface IPlayerEnable : IEntityEnable<IPlayer>, IPlayerBehaviour
{
}

public interface IPlayerDisable : IEntityDisable<IPlayer>, IPlayerBehaviour
{
}

public interface IPlayerDispose : IEntityDispose<IPlayer>, IPlayerBehaviour
{
}

public interface IPlayerGizmos : IEntityGizmos<IPlayer>, IPlayerBehaviour
{
}
```

### Generated interfaces

| Interface | Inherits from | Lifecycle method |
|---|---|---|
| `I{Entity}Behaviour` | `IEntityBehaviour` | marker interface |
| `I{Entity}Tick` | `IEntityTick<{Entity}>` | `Tick({Entity}, float)` |
| `I{Entity}FixedTick` | `IEntityFixedTick<{Entity}>` | `FixedTick({Entity}, float)` |
| `I{Entity}LateTick` | `IEntityLateTick<{Entity}>` | `LateTick({Entity}, float)` |
| `I{Entity}Init` | `IEntityInit<{Entity}>` | `Init({Entity})` |
| `I{Entity}Enable` | `IEntityEnable<{Entity}>` | `Enable({Entity})` |
| `I{Entity}Disable` | `IEntityDisable<{Entity}>` | `Disable({Entity})` |
| `I{Entity}Dispose` | `IEntityDispose<{Entity}>` | `Dispose({Entity})` |
| `I{Entity}Gizmos` | `IEntityGizmos<{Entity}>` | `DrawGizmos({Entity})` |

---

## ⚙️ Configuration

### `[EntityDomainBehaviours]`

| Property | Type | Description |
|---|---|---|
| `EntityType` | `Type` | The entity type (interface or concrete class) for which behaviour interfaces are generated. Must implement `IEntity`. |

The attribute has a single required constructor argument:

```csharp
[EntityDomainBehaviours(typeof(IPlayer))]
public static class PlayerDomain { }
```

---

## 🔧 Troubleshooting

### Generated interfaces are not showing in IntelliSense

1. Make sure `EntityDomainGenerator.dll` is in `Assets/Plugins/Atomic/SourceGenerators/`.
2. Check that the **Asset Label** is `RoslynAnalyzer`.
3. Verify platform settings: **Any Platform** must be **unchecked**, and all individual platforms must be **unchecked**.
4. Rebuild the Unity project (`Assets → Reimport All` or restart the editor).

### Build errors after adding the DLL

- Ensure the DLL is **not** included in any runtime platform.
- Ensure the type passed to `[EntityDomainBehaviours(typeof(T))]` implements `IEntity`.

### Generated file is not written to disk

The generator produces source **in-memory**. To write generated files to disk, define `ATOMIC_OUTPUT_SOURCEGEN_FILES` in `Edit → Project Settings → Player → Scripting Define Symbols`. Files are written to `Temp/GeneratedCode/`.

---

## 🏗️ Implementation Notes

- Targets `netstandard2.0` and `Microsoft.CodeAnalysis.CSharp` **4.3.0** for Unity 6000 compatibility.
- Uses `SyntaxProvider.CreateSyntaxProvider` instead of `ForAttributeWithMetadataName` because Unity 6000 ships Roslyn 4.3.0.
- Reads the `[EntityDomainBehaviours]` attribute from the `Atomic.Entities` assembly.
- Validates that the constructor argument is a type implementing `IEntity`.
- Skips IDE analysis and runs only during actual builds.
- For more details, see [Implementation.md](Implementation.md).
