# 🧩 Entity Installers

**Entity Installer** is a component that installs tags, values, and behaviors into an entity instance. It provides a
**declarative mechanism** for configuring entities during initialization or runtime.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
- [Notes](#-notes)
- [Best Practices](#-best-practices)

---

## 🗂 Example of Usage

Below is an example how to install [MonoEntity](../Entities/MonoEntity.md) and populate it wit **tags** and 
**values**:

#### 1. Create `CharacterInstaller` script

 ```csharp
//Populates entity with tags, values and behaviours
public sealed class CharacterInstaller : MonoEntityInstaller
{
    [SerializeField] private Transform _transform;
    [SerializeField] private Const<float> _moveSpeed = 5.0f; //Immutable variable
    [SerializeField] private ReactiveVariable<Vector3> _moveDirection; //Mutable variable with subscription

    public override void Install(IEntity entity)
    {
        //Add tags to a character
        entity.AddTag("Character");
        entity.AddTag("Moveable");

        //Add properties to a character
        entity.AddValue("Transform", _transform);
        entity.AddValue("MoveSpeed", _moveSpeed);
        entity.AddValue("MoveDirection", _moveDirection);
    }
}
```

#### 2. Attach `CharacterInstaller` script to the GameObject

<img width="464" height="153" alt="изображение" src="https://github.com/user-attachments/assets/1967b1d8-b6b7-41c7-85db-5d6935f6443e" />

#### 3. Drag & drop `CharacterInstaller` into `installers` field of the entity

<img width="464" height="" alt="изображение" src="../../Images/MonoEntity%20Attach%20Installer.png" />

#### 4. Now your `MonoEntity` has tags and properties.

---

## 🔍 API Reference

Below are the different types of installers depending on the usage scenario:

- **Interfaces**
    - [IEntityInstaller](IEntityInstaller.md) <!-- + -->
    - [IEntityInstaller&lt;E&gt;](IEntityInstaller%601.md) <!-- + -->
- **MonoBehaviours**
    - [MonoEntityInstaller](MonoEntityInstaller.md) <!-- + -->
    - [MonoEntityInstaller&lt;E&gt;](MonoEntityInstaller%601.md) <!-- + -->
- **ScriptableObjects**
    - [ScriptableEntityInstaller](ScriptableEntityInstaller.md) <!-- + -->
    - [ScriptableEntityInstaller&lt;E&gt;](ScriptableEntityInstaller%601.md) <!-- + -->

---

## 📝 Notes

- **Installer** — declarative way of configuring entities.
- **MonoEntityInstaller** — configuration via `MonoBehaviour`, bound to the scene.
- **ScriptableEntityInstaller** — configuration via `ScriptableObject`, reusable logic.
- **Generic Installers** — strongly typed variant for improved safety and readability.
- Keep installers focused on **entity configuration only**; avoid embedding business logic.
- Always override `Uninstall` when working with subscriptions or `IDisposable` objects to ensure proper cleanup.  

---

## 📌 Best Practices

- [Modular EntityInstallers](../../BestPractices/ModularEntityInstallers.md)  <!-- + -->
- [Uninstall Method for EntityInstallers](../../BestPractices/UninstallEntityInstaller.md)
- [Optional with EntityInstallers](../../BestPractices/UsingOptionalWithInstallers.md)
- [DisposeComposite in EntityInstallers](../../BestPractices/UsingSubscriptionsWithDisposeComposite.md)
- [PlayMode & EditMode for EntityInstallers](../../BestPractices/UsingUtilsForEntityInstallers.md) <!-- + -->
