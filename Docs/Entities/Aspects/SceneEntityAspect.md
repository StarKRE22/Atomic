# 🧩 SceneEntityAspect

Represents a non-generic `MonoBehaviour` that applies or discards reusable behavior on
any entity within a Unity scene.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Methods](#-methods)
        - [Apply()](#applyientity)
        - [Discard()](#discardientity)

---

## 🗂 Example of Usage

Below is an example of aspect that temporarily multiplies an entity's speed and restores it when discarded:

#### 1. Create a `SpeedBoost` script deriving from `SceneEntityAspect`

```csharp
public sealed class SpeedBoost : SceneEntityAspect
{
    [SerializeField]
    private float _multiplier = 1.5f;

    public override void Apply(IEntity entity) =>
        entity.GetValue<IVariable<float>>("Speed").Value *= _multiplier;

    public override void Discard(IEntity entity) => 
        entity.GetValue<IVariable<float>>("Speed").Value /= _multiplier;
}
```

#### 2. Attach `SpeedBoost` script to a GameObject

<img width="500" height="" alt="изображение" src="../../Images/SpeedBoost.png" />

#### 3. For example, use `SpeedBoost` when an entity interacts with some physics trigger

```csharp
public class EntityAspectTrigger : MonoBehaviour
{
    [SerializeField]
    private SceneEntityAspect _aspect; //Assign Speed boost in the Unity Inspector
    
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out IEntity entity))
            _aspect.Apply(entity);
    }  
    
    private void OnTriggerExit(Collider collider)
    {
        if (collider.TryGetComponent(out IEntity entity))
           _aspect.Discard(entity);
    }
}
```

---

## 🔍 API Reference

### 🏛️ Type

```csharp
public abstract class SceneEntityAspect : SceneEntityAspect<IEntity>, IEntityAspect
```

- **Description:** Represents a non-generic `MonoBehaviour` that applies or discards reusable behavior on
  any entity within a Unity scene.
- **Inheritance:** [SceneEntityAspect&lt;E&gt;](SceneEntityAspect%601.md)
- **Note:** Ideal for modular behaviors that can be dynamically applied or removed at runtime.

---

### 🏹 Methods

#### `Apply(IEntity)`

```csharp
public abstract void Apply(IEntity entity);
```

- **Description:** Applies the aspect to the specified entity.
- **Parameter:** `entity` – The entity to which the aspect will be applied.

#### `Discard(IEntity)`

```csharp
public abstract void Discard(IEntity entity);
```

- **Description:** Reverses the effects of `Apply` on the specified entity.
- **Parameter:** `entity` – The entity from which the aspect should be removed.