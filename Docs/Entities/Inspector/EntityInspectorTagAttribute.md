# 🧩 EntityInspectorTagAttribute

Odin Inspector drawer attribute for `string` fields and parameters that renders a popup listing available tag names.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
  - [Type](#-type)
  - [Constructors](#-constructors)
    - [EntityInspectorTagAttribute()](#entityinspectortagattribute)
    - [EntityInspectorTagAttribute(Type)](#entityinspectortagattributetype)
  - [Properties](#-properties)
    - [entityType](#entitytype)

---

## 🗂 Example of Usage

Use on a serialized field to select a tag from a dropdown:

```csharp
public class DamageOnTagBehaviour : MonoBehaviour
{
    [EntityInspectorTag]
    [SerializeField]
    private string targetTag;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetEntity(out IEntity entity) && entity.HasTag(targetTag))
            entity.TakeDamage(10);
    }
}
```

Filter the dropdown to a specific entity type:

```csharp
public class GameEntityFilter : MonoBehaviour
{
    [EntityInspectorTag(typeof(IGameEntity))]
    [SerializeField]
    private string selectableTag;
}
```

---

## 🔍 API Reference

### 🏛️ Type

```csharp
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter)]
public sealed class EntityInspectorTagAttribute : Attribute
```

- **Description:** Odin Inspector drawer attribute for `string` fields and parameters that renders a popup listing available tag names.
- **Inheritance:** `Attribute`
- **Targets:** Fields, Parameters
- **Notes:** Available only in the Unity Editor and requires Odin Inspector.
- **See also:** [EntityInspectorAPIAttribute](EntityInspectorAPIAttribute.md), [EntityInspectorValueAttribute](EntityInspectorValueAttribute.md)

---

### 🏗️ Constructors

#### `EntityInspectorTagAttribute()`

```csharp
public EntityInspectorTagAttribute()
```

- **Description:** Uses `typeof(IEntity)` as the entity type.

#### `EntityInspectorTagAttribute(Type)`

```csharp
public EntityInspectorTagAttribute(Type entityType)
```

- **Description:** Specifies the entity type used to filter available tags.
- **Parameter:** `entityType` – The entity type whose registered tags should appear in the dropdown.

---

### 🔑 Properties

#### `entityType`

```csharp
public readonly Type entityType;
```

- **Description:** The entity type used to filter tags. Defaults to `typeof(IEntity)`.
- **Access:** Read-only
