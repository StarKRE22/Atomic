

## 🗂 Example of Usage

### Entity State

An entity may have health, damage, and speed values as part of its **State**:

```csharp
Entity entity = new Entity();

entity.AddValue("Health", 10);
entity.AddValue("Damage", 1);
entity.AddValue("Speed", 5.0f);
```

You can also define coordinates and direction — also part of the **State**:

```csharp
entity.AddValue("Position", new Vector3(10, 0, 10));
entity.AddValue("MoveDirection", Vector3.forward);
```

---

### Entity Tags

Entities can have **tags** that define which categories they belong to:

```csharp
entity.AddTag("Moveable");
entity.AddTag("Damageable");
entity.AddTag("NPC");
```

---

### Entity Behaviour

In addition to *State*, we can add **Behaviour**, such as movement logic, which remains strictly separated from the
entity’s data:

```csharp
public class MoveBehaviour : IEntityFixedTick
{
    public void FixedTick(IEntity entity, float deltaTime)
    {
        Vector3 position = entity.GetValue<Vector3>("Position");
        Vector3 direction = entity.GetValue<Vector3>("MoveDirection");
        float speed = entity.GetValue<float>("Speed");
        
        Vector3 newPosition = position + speed * deltaTime * direction;
        entity.SetValue("Position", newPosition);
    }
}
```

This behaviour can then be dynamically attached to the entity:

```csharp
entity.AddBehaviour<MoveBehaviour>();
```

---

### Entity Lifecycle

**Activate the entity** so that its `FixedTick` logic starts working:

```csharp
entity.Enable();
```

**Update the entity** while the game is running:

```csharp
while(GameIsRunning)
    entity.FixedTick();
```

**Dispose of the entity** when the game is finished:

```csharp
entity.Dispose();
```

---



## 🔥 Performance

The performance comparison below was measured on a **MacBook with Apple M1** for collections containing **1000 elements
of type `object`**.
