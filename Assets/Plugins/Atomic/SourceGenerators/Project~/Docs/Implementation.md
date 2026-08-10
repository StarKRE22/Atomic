# 🏗️ Implementation Notes

This document is for contributors and maintainers who want to understand, modify, or extend the Atomic source generators. It covers the solution layout, shared-source model, Roslyn compatibility constraints, and how to add a new generator.

---

## 📑 Table of Contents

- [Solution Structure](#-solution-structure)
- [Shared Source Model](#-shared-source-model)
- [Common MSBuild Settings](#-common-msbuild-settings)
- [Generator Pipeline](#-generator-pipeline)
- [Analyzer Pipeline](#-analyzer-pipeline)
- [Roslyn 4.3.0 Compatibility](#-roslyn-430-compatibility)
- [Adding a New Generator](#-adding-a-new-generator)
- [Important Design Notes](#-important-design-notes)

---

## 📁 Solution Structure

```
SourceGenerators/
├── Atomic.SourceGenerators.sln      # Solution containing all generator projects
├── Directory.Build.props            # Common MSBuild settings (Roslyn 4.3.0, netstandard2.0)
├── Atomic.SourceGenerators.Shared/  # Shared source files (no separate DLL)
│   ├── CodeWriter.cs
│   ├── DiagnosticLogger.cs
│   └── SourceOutputHelpers.cs
├── EntityAPIGenerator/              # [EntityExtensionsAPI] generator for tags/values
├── EntityAPIAnalyzer/              # diagnostics + code fixes for [EntityExtensionsAPI]
├── EventAPIGenerator/              # [EventExtensionsAPI] generator for event keys
├── EventAPIAnalyzer/               # diagnostics + code fixes for [EventExtensionsAPI]
└── EntityDomainGenerator/           # [EntityDomainBehaviours] generator for domain behaviour interfaces
```

Each generator/analyzer project compiles into its own DLL. Unity loads these DLLs as Roslyn analyzers.

---

## 🔗 Shared Source Model

The `Atomic.SourceGenerators.Shared` folder contains source files that are **compiled directly into each generator** via:

```xml
<ItemGroup>
  <Compile Include="..\Atomic.SourceGenerators.Shared\*.cs"
           Link="Shared\%(Filename)%(Extension)" />
</ItemGroup>
```

This keeps every generator DLL self-contained and avoids analyzer dependency issues in Unity's Roslyn pipeline.

> Do not reference a separate class-library project from the generator projects — that would create a second DLL that Unity's analyzer loader may not resolve correctly.

---

## ⚙️ Common MSBuild Settings

`Directory.Build.props` centralizes the following for all projects:

| Property | Value | Reason |
|---|---|---|
| `TargetFramework` | `netstandard2.0` | Required by Unity's compiler |
| `LangVersion` | `12` | Modern C# without requiring newer runtime |
| `Nullable` | `enable` | Safer null handling |
| `ImplicitUsings` | `disable` | Explicit control over dependencies |
| `IsRoslynComponent` | `true` | Marks the assembly as a Roslyn component |
| `EnforceExtendedAnalyzerRules` | `true` | Analyzer safety checks |
| `GenerateDocumentationFile` | `false` | Not needed for analyzer DLLs |

Roslyn packages are pinned to Unity 6000 compatibility:

| Package | Version |
|---|---|
| `Microsoft.CodeAnalysis.CSharp` | **4.3.0** |
| `Microsoft.CodeAnalysis.Analyzers` | **3.3.4** |

`RS1035` is suppressed so that generators can write debug output to `Temp/GeneratedCode` when the `ATOMIC_OUTPUT_SOURCEGEN_FILES` symbol is defined.

---

## 🔄 Generator Pipeline

Both generators are incremental (`IIncrementalGenerator`) and follow the same shape:

1. **Find candidates** with `SyntaxProvider.CreateSyntaxProvider`.
   - `EntityAPIGenerator` looks for classes with `[EntityExtensionsAPI]`.
   - `EventAPIGenerator` looks for classes with `[EventExtensionsAPI]`.
   - `EntityDomainGenerator` looks for classes with `[EntityDomainBehaviours(typeof(...))]`.
2. **Transform** each candidate into a semantic model (parser).
3. **Filter** out invalid candidates (e.g., wrong field types, missing attribute).
4. **Combine** the collected definitions with the compilation and parse options.
5. **Emit** source for each definition and add it to the compilation.
6. **Log** diagnostics and optionally write files to `Temp/GeneratedCode/`.

Generators skip IDE analysis and only run during actual builds:

```csharp
internal static readonly bool IsBuildTime = Assembly.GetEntryAssembly() != null;
```

They also skip the assembly that defines the marker attribute itself (`Atomic.Entities` or `Atomic.Events`) because that assembly only contains the attribute declaration.

---

## 🔍 Analyzer Pipeline

Both analyzers are `DiagnosticAnalyzer` subclasses:

1. **Register** a `SyntaxNodeAction` on `FieldDeclarationSyntax`.
2. **Filter** to static fields inside classes marked with the corresponding attribute.
3. **Check** each field's type:
   - `EntityAPIAnalyzer` checks for `ValueKey<>` or `TagKey<>` from `Atomic.Entities`.
   - `EventAPIAnalyzer` checks for `EventKey<>` from `Atomic.Events`.
4. **Report** diagnostics if a field is uninitialized or initialized with `new()` / `default`.
5. **Code fix providers** generate the `= new(nameof(FieldName))` initializer.

---

## ⚠️ Roslyn 4.3.0 Compatibility

Unity 6000.3.x bundles **Roslyn 4.3.0**. Do not use APIs that require a newer version:

- ❌ `ForAttributeWithMetadataName` requires Roslyn ≥ 4.6.0
- ✅ `SyntaxProvider.CreateSyntaxProvider` + manual attribute lookup is the compatible approach

Always verify generator behavior by building inside Unity, not only with the standalone .NET SDK.

---

## 🏗️ Adding a New Generator

1. Create a new folder under `SourceGenerators/`, e.g. `MyGenerator/`.
2. Add `MyGenerator.csproj`:

   ```xml
   <Project Sdk="Microsoft.NET.Sdk">
     <PropertyGroup>
       <AssemblyName>MyGenerator</AssemblyName>
       <RootNamespace>MyGenerator</RootNamespace>
     </PropertyGroup>
     <ItemGroup>
       <Compile Include="..\Atomic.SourceGenerators.Shared\*.cs"
                Link="Shared\%(Filename)%(Extension)" />
     </ItemGroup>
     <Target Name="CopyToUnity" AfterTargets="Build">
       <PropertyGroup>
         <_UnityPluginDir>$([MSBuild]::NormalizeDirectory('$(ProjectDir)..\..\Assets\Plugins\Atomic\SourceGenerators'))</_UnityPluginDir>
       </PropertyGroup>
       <Copy SourceFiles="$(TargetDir)$(TargetName).dll" DestinationFolder="$(_UnityPluginDir)" SkipUnchangedFiles="true" />
       <Copy SourceFiles="$(TargetDir)$(TargetName).pdb" DestinationFolder="$(_UnityPluginDir)" SkipUnchangedFiles="true" Condition="Exists('$(TargetDir)$(TargetName).pdb')" />
       <Message Text="MyGenerator deployed to $(_UnityPluginDir)" Importance="high" />
     </Target>
   </Project>
   ```

3. Add `MyGenerator.cs` with a class implementing `IIncrementalGenerator` and decorated with `[Generator]`.
4. Add a parser and code emitter as needed.
5. Add the project to `Atomic.SourceGenerators.sln`.
6. Build the solution and verify the DLL appears in `Assets/Plugins/Atomic/SourceGenerators/`.

> 💡 **Tip:** Reuse the shared `CodeWriter`, `DiagnosticLogger`, and `SourceOutputHelpers` classes for consistent formatting and diagnostics.

---

## 📝 Important Design Notes

- Keep marker attributes (`[EntityExtensionsAPI]`, `[EventExtensionsAPI]`, `[EntityDomainBehaviours]`) in a **separate Unity assembly definition** (`Atomic.Entities`, `Atomic.Events`) so the generator can discover them during compilation.
- Generator DLLs must be marked with the `RoslynAnalyzer` asset label in Unity and must have **all platforms unchecked**.
- Generators run only at **build time**; they do not add runtime overhead.
- Use `SyntaxProvider.CreateSyntaxProvider` for Roslyn 4.3.0 compatibility.
- If a generator silently fails, check Unity's `Editor.log` for `CS9057` (Roslyn version mismatch) or missing DLLs in `Assets/Plugins/Atomic/SourceGenerators/`.
