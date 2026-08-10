# ⚛️ Atomic Source Generators

Roslyn incremental source generators and analyzers for the [Atomic](https://github.com/StarKRE22/Atomic) framework.

They turn declarative `[EntityExtensionsAPI]` and `[EventExtensionsAPI]` classes into strongly-typed extension methods at compile time, eliminating magic strings and manual boilerplate in Unity projects.

---

## 📑 Table of Contents

- [Quick Start](#-quick-start)
- [Requirements](#-requirements)
- [Documentation](#-documentation)
- [What It Generates](#-what-it-generates)
- [Important Notes](#-important-notes)
- [Build](#-build)
- [Related Repositories](#-related-repositories)

---

## 🚀 Quick Start

1. Build the solution:

   ```bash
   dotnet build Atomic.SourceGenerators.sln -c Release
   ```

2. Deploy the four DLLs to your Unity project:

   ```bash
   dotnet build Atomic.SourceGenerators.sln -c Release \
     -p:AtomicDeployToUnity=true \
     -p:AtomicUnityPluginDir="C:\YourProject\Assets\Plugins\Atomic\SourceGenerators"
   ```

3. In Unity, add the **RoslynAnalyzer** asset label to each DLL and uncheck all platforms in the Inspector.

4. Declare an API class and compile:

   ```csharp
   [EntityExtensionsAPI]
   public static partial class PlayerAPI
   {
       public static readonly ValueKey<IEntity, int> Health = new(nameof(Health));
   }
   ```

   ```csharp
   entity.AddHealth(100);
   int health = entity.GetHealth();
   ```

For full details, see [Docs/Setup.md](Docs/Setup.md).

---

## 📝 Requirements

- **Unity 6** (6000.0 LTS or newer) with source-generator support
- **.NET SDK 8+** or **.NET 7+** to build the generators
- A Unity project that references the [Atomic.Entities](https://github.com/StarKRE22/Atomic/blob/main/Assets/Plugins/Atomic/Entities/Scripts/Codegen/EntityExtensionsAPIAttribute.cs) and/or [Atomic.Events](https://github.com/StarKRE22/Atomic/blob/main/Assets/Plugins/Atomic/Events/Scripts/CodeGen/EventExtensionsAPIAttribute.cs) runtime assemblies

---

## 📚 Documentation

| Document | Description |
|---|---|
| [Docs/Setup.md](Docs/Setup.md) | Build, deploy, and Unity import settings for all generators and analyzers |
| [Docs/EntityAPIGenerator.md](Docs/EntityAPIGenerator.md) | `[EntityExtensionsAPI]` source generator: tags, values, unsafe mode, configuration |
| [Docs/EventAPIGenerator.md](Docs/EventAPIGenerator.md) | `[EventExtensionsAPI]` source generator: event-bus extension methods |
| [Docs/EntityAPIAnalyzer.md](Docs/EntityAPIAnalyzer.md) | Analyzer rules and code fixes for `[EntityExtensionsAPI]` declarations |
| [Docs/EventAPIAnalyzer.md](Docs/EventAPIAnalyzer.md) | Analyzer rules and code fixes for `[EventExtensionsAPI]` declarations |
| [Docs/Implementation.md](Docs/Implementation.md) | Solution structure, shared-source model, Roslyn 4.3.0 notes, and how to add a new generator |

---

## 🧩 What It Generates

### Entity API

From a `[EntityExtensionsAPI]` class:

```csharp
[EntityExtensionsAPI]
public static partial class PlayerAPI
{
    public static readonly TagKey<IEntity> Alive = new(nameof(Alive));
    public static readonly ValueKey<IEntity, int> Health = new(nameof(Health));
}
```

The generator emits methods such as:

```csharp
entity.AddHealth(100);
int health = entity.GetHealth();
entity.SetHealth(80);
entity.AddAliveTag();
entity.HasAliveTag();
```

### Event API

From an `[EventExtensionsAPI]` class:

```csharp
[EventExtensionsAPI]
public static partial class GameEventAPI
{
    public static readonly EventKey<IEventBus> GameStarted = new(nameof(GameStarted));
}
```

The generator emits methods such as:

```csharp
bus.SubscribeGameStarted(() => Debug.Log("Started!"));
bus.InvokeGameStarted();
```

---

## ⚠️ Important Notes

- The generator and analyzer DLLs are **compile-time only** and must not be included in player builds.
- In Unity, all DLLs must have the **RoslynAnalyzer** asset label and **all platforms unchecked**.
- Unity 6000 bundles **Roslyn 4.3.0**, so the generators use `SyntaxProvider.CreateSyntaxProvider` instead of newer APIs such as `ForAttributeWithMetadataName`.
- Marker attributes (`[EntityExtensionsAPI]`, `[EventExtensionsAPI]`) live in the runtime assemblies:
  - [EntityExtensionsAPIAttribute.cs](https://github.com/StarKRE22/Atomic/blob/main/Assets/Plugins/Atomic/Entities/Scripts/Codegen/EntityExtensionsAPIAttribute.cs)
  - [EventExtensionsAPIAttribute.cs](https://github.com/StarKRE22/Atomic/blob/main/Assets/Plugins/Atomic/Events/Scripts/CodeGen/EventExtensionsAPIAttribute.cs)

---

## 📦 Build

From the `SourceGenerators` directory:

```bash
dotnet build Atomic.SourceGenerators.sln -c Release
```

To deploy to a Unity project at the same time:

```bash
dotnet build Atomic.SourceGenerators.sln -c Release \
  -p:AtomicDeployToUnity=true \
  -p:AtomicUnityPluginDir="C:\YourProject\Assets\Plugins\Atomic\SourceGenerators"
```

---

## 🔗 Related Repositories

- [Atomic Framework](https://github.com/StarKRE22/Atomic) — entities, events, behaviours, and the marker attributes used by these generators

For contributor details, see [Docs/Implementation.md](Docs/Implementation.md).
