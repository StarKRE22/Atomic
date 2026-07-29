# ⚙️ Source Generators Setup

This guide explains how to add the **Atomic source generators** and **analyzers** to a Unity project.
The same steps apply to all four assemblies:

- `EntityAPIGenerator.dll`
- `EntityAPIAnalyzer.dll`
- `EventAPIGenerator.dll`
- `EventAPIAnalyzer.dll`

For usage examples, see the [Code Generation Walkthrough](../Tutorials/2.%20Entity%20API%20Generation.md).  
For generator options and advanced configuration, see the [Code Generation Manual](Manual.md).

---

## 📑 Table of Contents

- [Prerequisites](#-prerequisites)
- [Getting the DLLs](#-getting-the-dlls)
  - [Option A: Build from source](#option-a-build-from-source)
  - [Option B: Copy prebuilt DLLs](#option-b-copy-prebuilt-dlls)
- [Unity Import Settings](#-unity-import-settings)
  - [1. Add the RoslynAnalyzer label](#1-add-the-roslynanalyzer-label)
  - [2. Disable runtime platforms](#2-disable-runtime-platforms)
- [Verification](#-verification)
- [Troubleshooting](#-troubleshooting)
  - [Generated methods do not appear in IntelliSense](#generated-methods-do-not-appear-in-intellisense)
  - [Build errors after adding the DLLs](#build-errors-after-adding-the-dlls)
  - [Inspect generated source](#inspect-generated-source)
- [Source Repository](#-source-repository)

---

## 📝 Prerequisites

- **Unity 6** (6000.0 LTS or newer) with bundled Roslyn 4.3.0
- A project that references the **Atomic.Entities** and/or **Atomic.Events** runtime assemblies

---

## 📦 Getting the DLLs

### Option A: Build from source

The generator source code is at https://github.com/dre0dru/Atomic.SourceGenerators.

From the repository root:

```bash
dotnet build Atomic.SourceGenerators.sln -c Release
```

To copy the output to your Unity project automatically:

```bash
dotnet build Atomic.SourceGenerators.sln -c Release \
  -p:AtomicDeployToUnity=true \
  -p:AtomicUnityPluginDir="C:\YourProject\Assets\Plugins\Atomic\SourceGenerators"
```

> 💡 **Tip:** Only the `.dll` files are needed; PDBs can be left behind.

### Option B: Copy prebuilt DLLs

Copy the required assemblies into:

```
Assets/Plugins/Atomic/SourceGenerators/
```

Typical contents:

```
Assets/Plugins/Atomic/SourceGenerators/
├── EntityAPIGenerator.dll
├── EntityAPIAnalyzer.dll
├── EventAPIGenerator.dll
└── EventAPIAnalyzer.dll
```

---

## 🔧 Unity Import Settings

### 1. Add the RoslynAnalyzer label

Select each DLL in the Unity Project window and add the **Asset Label**:

```
RoslynAnalyzer
```

This label tells Unity's Roslyn compiler to load the assembly as a source generator or analyzer.

### 2. Disable runtime platforms

Generators and analyzers are **compile-time only**. They must not be included in the final player build.

In the DLL Inspector:

- **Auto Reference**: ✅ checked
- **Validate References**: ✅ checked
- **Select platforms for plugin**
  - **Any Platform**: ⬜ unchecked
  - **Editor**: ⬜ unchecked
  - **Standalone**: ⬜ unchecked
  - All other platforms: ⬜ unchecked

> ⚠️ **Important:** Leaving all platforms unchecked is correct. The assemblies are analyzers, not runtime plugins.

After changing the settings, click **Apply** and rebuild the project (`Assets → Reimport All` or restart the editor).

---

## ✅ Verification

Create a test API class and confirm the generated methods appear in IntelliSense after compilation.

For entities:

```csharp
using Atomic.Entities;

[GenerateEntityExtensionsAPI]
public static partial class VerifyEntityAPI
{
    public static readonly ValueKey<IEntity, int> Health = new(nameof(Health));
}
```

You should now see `entity.AddHealth(...)`, `entity.GetHealth()`, and `entity.SetHealth(...)` in IntelliSense.

For events:

```csharp
using Atomic.Events;

[GenerateEventExtensionsAPI]
public static partial class VerifyEventAPI
{
    public static readonly EventKey<IEventBus> PlayerScored = new(nameof(PlayerScored));
}
```

You should now see `_eventBus.SubscribePlayerScored(...)` and `_eventBus.InvokePlayerScored()`.

If the methods do not appear, run `Assets → Reimport All` or restart the Unity Editor.

---

## 🔧 Troubleshooting

### Generated methods do not appear in IntelliSense

1. Confirm the DLLs are in `Assets/Plugins/Atomic/SourceGenerators/`.
2. Confirm each DLL has the **RoslynAnalyzer** asset label.
3. Confirm **Any Platform** is unchecked and all platforms are unchecked.
4. Restart Unity or run `Assets → Reimport All`.

### Build errors after adding the DLLs

- Make sure the DLLs are **not** included in any runtime platform.
- Make sure every `[GenerateEntityExtensionsAPI]` / `[GenerateEventExtensionsAPI]` field is initialized with a non-default constructor, e.g. `new(nameof(FieldName))`. The analyzers report missing or invalid initializers:

```csharp
[GenerateEntityExtensionsAPI]
public static partial class CharacterAPI
{
    // EAPI0001: field has no initializer
    public static readonly ValueKey<IEntity, int> Health;
}
```

Fix it by adding the initializer:

```csharp
public static readonly ValueKey<IEntity, int> Health = new(nameof(Health));
```

### Inspect generated source

The generators produce code **in-memory**. To write the generated files to disk, define the symbol:

```
ATOMIC_OUTPUT_SOURCEGEN_FILES
```

in `Edit → Project Settings → Player → Scripting Define Symbols`. Generated files are then written to:

```
Temp/GeneratedCode/
```

---

## 📦 Source Repository

**https://github.com/dre0dru/Atomic.SourceGenerators**
