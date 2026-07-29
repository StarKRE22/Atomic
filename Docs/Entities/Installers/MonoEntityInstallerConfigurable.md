# 🧩 MonoEntityInstallerConfigurable

A scene-bound installer that delegates installation to a configurable list of nested [IEntityInstaller](IEntityInstaller.md) instances.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
  - [Type](#-type)
  - [Methods](#-methods)
    - [Install(IEntity)](#installientity)

---

## 🗂 Example of Usage

Create small, focused installers and compose them in the Inspector:

```csharp
public sealed class HealthInstaller : IEntityInstaller
{
    [SerializeField]
    private int _health = 100;

    public void Install(IEntity entity) =>
        entity.AddValue("Health", _health);
}

public sealed class TeamInstaller : IEntityInstaller
{
    [SerializeField]
    private int _teamId;

    public void Install(IEntity entity) =>
        entity.AddValue("TeamId", _teamId);
}
```

Attach `MonoEntityInstallerConfigurable` to a GameObject and populate its installers array with `HealthInstaller`, `TeamInstaller`, or any other [IEntityInstaller](IEntityInstaller.md) implementations. When the entity is installed, all nested installers run in order.

---

## 🔍 API Reference

### 🏛️ Type

```csharp
public sealed class MonoEntityInstallerConfigurable : MonoEntityInstaller
```

- **Description:** A scene-bound installer that delegates installation to a configurable list of nested [IEntityInstaller](IEntityInstaller.md) instances.
- **Inheritance:** [MonoEntityInstaller](MonoEntityInstaller.md)
- **Notes:** Stores installers in a `[SerializeReference]` array, allowing mixed implementations in the Inspector.
- **See also:** [MonoEntityInstaller](MonoEntityInstaller.md), [IEntityInstaller](IEntityInstaller.md), [ScriptableEntityInstaller](ScriptableEntityInstaller.md)

---

### 🏹 Methods

#### `Install(IEntity)`

```csharp
public override void Install(IEntity entity)
```

- **Description:** Installs all configured nested installers into the entity.
- **Parameter:** `entity` – The entity being configured.
- **Remarks:** Skips null entries in the installer array.
