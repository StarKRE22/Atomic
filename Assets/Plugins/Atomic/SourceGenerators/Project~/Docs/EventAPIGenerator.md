# 🧩 Event API Source Generator

The **Event API Source Generator** is a Roslyn incremental source generator that reads `[EventExtensionsAPI]`-marked static classes and emits strongly-typed extension methods for [Atomic.Events](https://github.com/StarKRE22/Atomic) event buses.

It turns `EventKey<>` declarations into `Subscribe`, `Unsubscribe`, `Invoke`, `IsSubscribed`, and `Dispose` methods that are bound to the bus type declared in each key.

---

## 📑 Table of Contents

- [Requirements](#-requirements)
- [Setup](#-setup)
- [Basic Usage](#-basic-usage)
- [Supported Key Shapes](#-supported-key-shapes)
- [Generated Code](#-generated-code)
- [Configuration](#-configuration)
- [Analyzer](#-analyzer)
- [Troubleshooting](#-troubleshooting)
- [Implementation Notes](#-implementation-notes)

---

## 📝 Requirements

- **Unity 6** (6000.0 LTS or newer) with source-generator support
- The **EventAPIGenerator.dll** analyzer added to your Unity project (see [Setup.md](Setup.md))
- The `[EventExtensionsAPI]` attribute from [Atomic.Events](https://github.com/StarKRE22/Atomic/blob/main/Assets/Plugins/Atomic/Events/Scripts/CodeGen/EventExtensionsAPIAttribute.cs)

---

## 🔧 Setup

For build, deploy, and Unity import instructions, see the shared [Setup.md](Setup.md).

---

## 🧩 Basic Usage

Declare a static `partial` class, mark it with `[EventExtensionsAPI]`, and add static readonly fields of type `EventKey<TBus>` or `EventKey<TBus, T...>`:

```csharp
using Atomic.Events;

namespace Game.Gameplay
{
    [EventExtensionsAPI]
    public static partial class GameEventAPI
    {
        public static readonly EventKey<IEventBus> PlayerTurnStarted = new(nameof(PlayerTurnStarted));
        public static readonly EventKey<IEventBus, IGameEntity> EntityDespawned = new(nameof(EntityDespawned));
    }
}
```

After the first build, use the generated extension methods on any matching bus:

```csharp
IEventBus bus = new EventBus();

// Parameterless event
bus.SubscribePlayerTurnStarted(() => Debug.Log("Player turn started"));
bus.InvokePlayerTurnStarted();

// Parameterized event
bus.SubscribeEntityDespawned(entity => Debug.Log($"Despawned: {entity}"));
bus.InvokeEntityDespawned(enemyEntity);
```

The bus type is read from the **first generic argument** of each field, so one API class can target different bus types if needed.

---

## 🔑 Supported Key Shapes

| Key type | Generated methods |
|---|---|
| `EventKey<TBus>` | `Subscribe`, `Unsubscribe`, `Invoke`, `IsSubscribed`, `Dispose` |
| `EventKey<TBus, T>` | `Subscribe`, `Unsubscribe`, `Invoke(T)`, `IsSubscribed`, `Dispose` |
| `EventKey<TBus, T1, T2>` | `Subscribe`, `Unsubscribe`, `Invoke(T1, T2)`, `IsSubscribed`, `Dispose` |
| `EventKey<TBus, T1, T2, T3>` | `Subscribe`, `Unsubscribe`, `Invoke(T1, T2, T3)`, `IsSubscribed`, `Dispose` |

---

## 🔍 Generated Code

For the API class above, the generator emits a matching `partial` class with extension methods such as:

```csharp
public static partial class GameEventAPI
{
    public static Subscription SubscribePlayerTurnStarted(this IEventBus bus, Action action) =>
        bus.Subscribe(GameEventAPI.PlayerTurnStarted.Id, action);

    public static void UnsubscribePlayerTurnStarted(this IEventBus bus, Action action) =>
        bus.Unsubscribe(GameEventAPI.PlayerTurnStarted.Id, action);

    public static void InvokePlayerTurnStarted(this IEventBus bus) =>
        bus.Invoke(GameEventAPI.PlayerTurnStarted.Id);

    public static bool IsSubscribedPlayerTurnStarted(this IEventBus bus) =>
        bus.IsSubscribed(GameEventAPI.PlayerTurnStarted.Id);

    public static bool DisposePlayerTurnStarted(this IEventBus bus) =>
        bus.Dispose(GameEventAPI.PlayerTurnStarted.Id);

    public static Subscription<IGameEntity> SubscribeEntityDespawned(this IEventBus bus, Action<IGameEntity> action) =>
        bus.Subscribe<IGameEntity>(GameEventAPI.EntityDespawned.Id, action);

    public static void UnsubscribeEntityDespawned(this IEventBus bus, Action<IGameEntity> action) =>
        bus.Unsubscribe<IGameEntity>(GameEventAPI.EntityDespawned.Id, action);

    public static void InvokeEntityDespawned(this IEventBus bus, IGameEntity arg) =>
        bus.Invoke<IGameEntity>(GameEventAPI.EntityDespawned.Id, arg);

    public static bool IsSubscribedEntityDespawned(this IEventBus bus) =>
        bus.IsSubscribed(GameEventAPI.EntityDespawned.Id);

    public static bool DisposeEntityDespawned(this IEventBus bus) =>
        bus.Dispose(GameEventAPI.EntityDespawned.Id);
}
```

---

## ⚙️ Configuration

### `[EventExtensionsAPI]`

The `[EventExtensionsAPI]` attribute is parameterless. A class must be:

- `static`
- declared `partial`
- decorated with `[EventExtensionsAPI]`

Each field must be a static `EventKey<>` initialized with a non-default constructor:

```csharp
public static readonly EventKey<IEventBus> GameStarted = new(nameof(GameStarted));
```

---

## 🔬 Analyzer

Deploy the [Event API Analyzer](EventAPIAnalyzer.md) alongside the generator. It reports errors when `EventKey<>` fields inside `[EventExtensionsAPI]` classes are missing an initializer or are initialized with `new()` / `default`.

---

## 🔧 Troubleshooting

### Generated methods are not showing

1. Make sure `EventAPIGenerator.dll` is in `Assets/Plugins/Atomic/SourceGenerators/`.
2. Check that the **Asset Label** is `RoslynAnalyzer`.
3. Verify platform settings: **Any Platform** must be **unchecked**, and all individual platforms must be **unchecked**.
4. Rebuild the Unity project (`Assets → Reimport All` or restart the editor).

### Build errors after adding the DLL

- Ensure the DLL is **not** included in any runtime platform.
- Ensure all `[EventExtensionsAPI]` fields are `EventKey<>` and are initialized (e.g. `new(nameof(Field))`).

### Generated file is not written to disk

The generator produces source **in-memory**. To write generated files to disk, define `ATOMIC_OUTPUT_SOURCEGEN_FILES` in `Edit → Project Settings → Player → Scripting Define Symbols`. Files are written to `Temp/GeneratedCode/`.

---

## 🏗️ Implementation Notes

- Targets `netstandard2.0` and `Microsoft.CodeAnalysis.CSharp` **4.3.0** for Unity 6000 compatibility.
- Uses `SyntaxProvider.CreateSyntaxProvider` instead of `ForAttributeWithMetadataName` because Unity 6000 ships Roslyn 4.3.0.
- Reads the `[EventExtensionsAPI]` attribute from the `Atomic.Events` assembly.
- Skips IDE analysis and runs only during actual builds.
- For more details, see [Implementation.md](Implementation.md).
