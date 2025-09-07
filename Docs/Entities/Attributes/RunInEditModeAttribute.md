# 🧩️ RunInEditModeAttribute

`RunInEditModeAttribute` is a marker attribute for entity behaviour classes that instructs the system to **invoke entity lifecycle callbacks** (`Init`, `Enable`, `Disable`, `Dispose`) even when running in the **Unity Editor mode**.  
It is intended for use only on types implementing `IEntityBehaviour<T>`.

---

## Key Features

- **Editor Simulation** – Allows lifecycle logic to execute in the editor without entering Play Mode.
- **Selective Application** – Only affects classes marked with this attribute.
- **Lifecycle Coverage** – Applies to `Init`, `Enable`, `Disable`, and `Dispose` methods.
- **Non-Intrusive** – Does not alter runtime behavior; only affects editor execution.

---

## Usage
Mark your entity behaviour class with `[RunInEditMode]`:

```csharp
[RunInEditMode]
public class InitColorBehaviour : IEntityInit
{
    public void Init(IEntity entity)
    {
        var renderer = entity.GetValue<Renderer>("Renderer");
        var color = entity.GetValue<Color>("Color");
        renderer.material.color = color;
    }
}
```

> Note: This allows `Init` (and other lifecycle callbacks) to run in the Unity Editor without entering Play Mode.

---

## Remarks

- Intended primarily for editor tooling and debugging workflows.
- Only applies to classes implementing `IEntityBehaviour`.
- Useful for previewing runtime logic, setting up scene simulations, or verifying entity behaviors during development.
- Does not affect runtime execution in builds.
