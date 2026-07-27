# 🧩 MonoEntityInstallerConfigurable

**MonoEntityInstallerConfigurable** is a scene-bound installer that delegates installation to a configurable list of
nested [IEntityInstaller](IEntityInstaller.md) instances. It is useful when you want to compose entity setup from
multiple reusable installer assets or behaviours in the Unity Inspector.

---

## 📑 Table of Contents

- [Overview](#-overview)
- [Examples of Usage](#-examples-of-usage)
- [API Reference](#-api-reference)
- [Best Practices](#-best-practices)

---

## 🧩 Overview

Unlike a single-purpose [MonoEntityInstaller](MonoEntityInstaller.md), `MonoEntityInstallerConfigurable` stores an array
of `IEntityInstaller` references via `[SerializeReference]`. During installation, it iterates over the array and calls
`Install` on each one.

This makes it easy to build modular entity configurations directly in the Inspector without writing custom installer
classes for every entity variant.

---

## 🗂 Examples of Usage

### Compose Multiple Installers

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

### Configure in Inspector

Attach `MonoEntityInstallerConfigurable` to a GameObject and populate its `_installers` array with `HealthInstaller`,
`TeamInstaller`, or any other `IEntityInstaller` implementations.

```csharp
// The component is assigned to a MonoEntity's installers list.
// When the entity is installed, all nested installers run in order.
```

---

## 🔍 API Reference

### 🏛️ Type

```csharp
public sealed class MonoEntityInstallerConfigurable : MonoEntityInstaller
```

- **Inheritance:** [MonoEntityInstaller](MonoEntityInstaller.md)

### 🏹 Methods

#### `Install(IEntity)`

```csharp
public override void Install(IEntity entity)
```

- **Description:** Installs all configured nested installers into the entity.
- **Parameter:** `entity` — The entity being configured.
- **Note:** Skips null entries in the installer array.

---

## 📌 Best Practices

- Use `MonoEntityInstallerConfigurable` when an entity's setup is composed of multiple independent concerns.
- Keep individual installers focused on a single responsibility.
- Order installers carefully if later installers depend on values added by earlier ones.
- Avoid circular dependencies between installers.
- Consider using [ScriptableEntityInstaller](ScriptableEntityInstaller.md) for reusable cross-scene installers.
