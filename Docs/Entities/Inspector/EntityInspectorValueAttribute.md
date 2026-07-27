# 🧩 EntityInspectorValueAttribute

Odin Inspector drawer attribute for `string` fields and parameters that renders a popup listing available value names, filtered by value type.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
  - [Type](#-type)
  - [Constructors](#-constructors)
    - [EntityInspectorValueAttribute(Type)](#entityinspectorvalueattributetype)
    - [EntityInspectorValueAttribute(Type, Type)](#entityinspectorvalueattributetype-type)
  - [Properties](#-properties)
    - [entityType](#entitytype)
    - [valueType](#valuetype)

---

## 🗂 Example of Usage

Use on a serialized field to select a value name from a dropdown filtered by value type:

```csharp
public class HealthBonusBehaviour : MonoBehaviour
{
    [EntityInspectorValue(typeof(int))]
    [SerializeField]
    private string healthValueName;

    public void ApplyBonus(IEntity entity)
    {
        int currentHealth = entity.GetValue<int>(healthValueName);
        entity.SetValue(healthValueName, currentHealth + 10);
    }
}
```

Filter by both entity type and value type:

```csharp
public class MoveSpeedBonus : MonoBehaviour
{
    [EntityInspectorValue(typeof(IGameEntity), typeof(float))]
    [SerializeField]
    private string moveSpeedValueName;
}
```

---

## 🔍 API Reference

### 🏛️ Type

```csharp
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter)]
public sealed class EntityInspectorValueAttribute : Attribute
```

- **Description:** Odin Inspector drawer attribute for `string` fields and parameters that renders a popup listing available value names, filtered by value type.
- **Inheritance:** `Attribute`
- **Targets:** Fields, Parameters
- **Notes:** Available only in the Unity Editor and requires Odin Inspector.
- **See also:** [EntityInspectorAPIAttribute](EntityInspectorAPIAttribute.md), [EntityInspectorTagAttribute](EntityInspectorTagAttribute.md)

---

### 🏗️ Constructors

#### `EntityInspectorValueAttribute(Type)`

```csharp
public EntityInspectorValueAttribute(Type valueType)
```

- **Description:** Uses `typeof(IEntity)` as the entity type and the specified value type.
- **Parameter:** `valueType` – The type of the value whose keys should appear in the dropdown.

#### `EntityInspectorValueAttribute(Type, Type)`

```csharp
public EntityInspectorValueAttribute(Type entityType, Type valueType)
```

- **Description:** Specifies both the entity type and the value type.
- **Parameters:**
  - `entityType` – The entity type used to filter values.
  - `valueType` – The type of the value whose keys should appear in the dropdown.

---

### 🔑 Properties

#### `entityType`

```csharp
public readonly Type entityType;
```

- **Description:** The entity type used to filter values. Defaults to `typeof(IEntity)`.
- **Access:** Read-only

#### `valueType`

```csharp
public readonly Type valueType;
```

- **Description:** The value type used to filter keys.
- **Access:** Read-only
