# 📌 Combine EntityWorld + MultiEntityPool + MultiEntityFactory

Combining [MultiEntityFactory](../Entities/Factories/Manual.md), [EntityPool](../Entities/Pooling/Manual.md)
and [EntityWorld](../Entities/Worlds/Manual.md) allows you to **fully automate the lifecycle** of
gameplay objects — from creation to updates and returning them to the pool. This pattern is ideal for **bullets,
units, or temporary effects** that are frequently created and destroyed during gameplay.

---

## 🗂 Example of Usage

Below is an example of creating a unit system with the factory, pool and world  

#### 1. Assume we have a specific type for a unit

```csharp
public class UnitEntity : Entity
{
}
```

#### 2. Create a unit factory

```csharp
public abstract class UnitFactory : ScriptableEntityFactory<IUnitEntity>
{
    public string Name => this.name;

    public sealed override IUnitEntity Create()
    {
        var entity = new UnitEntity(
            this.Name,
            this.initialTagCapacity,
            this.initialValueCapacity,
            this.initialBehaviourCapacity
        );
        this.Install(entity);
        return entity;
    }

    protected abstract void Install(IUnitEntity entity);
}
```


#### 3. Create


#### Create a entity world for 


---

### 2️⃣ Create an EntityWorld and Bullet Pool

!!!
EntityWorld world = new EntityWorld("BulletWorld");

// Subscribe to optional events
world.OnAdded += e => Console.WriteLine($"🔫 Bullet added: {e.Name}");
world.OnRemoved += e => Console.WriteLine($"💨 Bullet removed: {e.Name}");
world.OnTicked += delta => Console.WriteLine($"Tick {delta}");

world.Enable();

// Initialize bullet pool
IEntityFactory<BulletEntity> factory = new BulletFactory();
EntityPool<BulletEntity> pool = new EntityPool<BulletEntity>(factory);

// Pre-create 20 bullets
pool.Init(20);
!!!

**Tip:**  
The pool pre-creates all entities and reuses them, preventing runtime allocations.

---

### 3️⃣ Activate Bullets from the Pool and Add to World

!!!
BulletEntity bullet = pool.Rent();

// Initialize bullet data
bullet.Set("Position", new Vector3(0, 0, 0));
bullet.Set("Direction", new Vector3(0, 0, 1));
bullet.Set("Speed", 10f);
bullet.Name = "Bullet#1";

// Add bullet to world — it will now be auto-ticked
world.Add(bullet);

// Add movement/rotation behaviours
bullet.AddBehaviour<BulletRotationBehaviour>();
bullet.AddBehaviour<BulletMoveBehaviour>();
!!!

---

### 4️⃣ Bullet Behaviours (auto-rotate and move)

!!!
public sealed class BulletRotationBehaviour : IEntityTick
{
private float _rotationSpeed = 180f; // degrees per second

    public void Tick(IEntity entity, float deltaTime)
    {
        float angle = entity.GetOrDefault("Rotation", 0f);
        angle += _rotationSpeed * deltaTime;
        entity.Set("Rotation", angle);

        Console.WriteLine($"🔄 {entity.Name} rotates: {angle:F1}°");
    }

}

public sealed class BulletMoveBehaviour : IEntityTick
{
public void Tick(IEntity entity, float deltaTime)
{
Vector3 pos = entity.GetOrDefault("Position", Vector3.zero);
Vector3 dir = entity.GetOrDefault("Direction", Vector3.forward);
float speed = entity.GetOrDefault("Speed", 0f);

        pos += dir.normalized * speed * deltaTime;
        entity.Set("Position", pos);

        Console.WriteLine($"➡️ {entity.Name} position: {pos}");
        
        // Simple lifetime logic based on distance
        float distance = entity.GetOrDefault("Travelled", 0f);
        distance += speed * deltaTime;
        entity.Set("Travelled", distance);

        float maxDistance = entity.GetOrDefault("MaxDistance", 50f);
        if (distance >= maxDistance)
        {
            entity.Set("ShouldDespawn", true);
        }
    }

}
!!!

**Comment:**  
`EntityWorld` automatically calls `Tick()` for all entities,  
so bullets will move and rotate without manual updates.

---

### 5️⃣ Add Centralized UseCases for Spawn and Despawn

!!!
public static class GameEntitiesUseCase
{
// Spawn a bullet into the world
public static BulletEntity Spawn(EntityWorld world, EntityPool<BulletEntity> pool, string name, Vector3 position,
Vector3 direction, float speed)
{
BulletEntity entity = pool.Rent();
entity.Name = name;

        entity.Set("Position", position);
        entity.Set("Direction", direction.normalized);
        entity.Set("Speed", speed);
        entity.Set("Rotation", 0f);
        entity.Set("Travelled", 0f);
        entity.Set("MaxDistance", 50f);
        entity.Set("ShouldDespawn", false);

        world.Add(entity);
        Console.WriteLine($"🚀 {entity.Name} spawned into world");
        return entity;
    }

    // Despawn (remove from world and return to pool)
    public static bool Despawn(EntityWorld world, EntityPool<BulletEntity> pool, BulletEntity entity)
    {
        if (!world.Remove(entity))
            return false;

        // Clean up values before returning to pool
        entity.Set("Direction", Vector3.zero);
        entity.Set("Speed", 0f);
        entity.Set("Travelled", 0f);
        entity.Set("ShouldDespawn", false);

        pool.Return(entity);
        Console.WriteLine($"💥 {entity.Name} returned to pool");
        return true;
    }

}
!!!

**Comment:**  
These helper methods centralize spawn/despawn logic,  
so you no longer duplicate entity management code across the project.

---

### 6️⃣ Example Usage in Gameplay

!!!
var spawned = GameEntitiesUseCase.Spawn(world, pool, "Bullet#1", new Vector3(0,0,0), new Vector3(0,0,1), 25f);

// Simulate world updates (normally driven by engine)
world.Tick(0.016f);
world.Tick(0.016f);

// Check despawn condition
if (spawned.GetOrDefault("ShouldDespawn", false))
{
GameEntitiesUseCase.Despawn(world, pool, spawned);
}
!!!

---

### 7️⃣ Cleanup

!!!
world.Disable();
pool.Dispose();
world.Dispose();
!!!

---

## ✅ Summary

- **EntityFactory** — centralizes entity creation logic.
- **EntityPool** — reuses entities efficiently, avoiding allocations.
- **EntityWorld** — automatically updates and manages entities.
- **GameEntitiesUseCase** — provides clean, unified spawn/despawn API.

🔹 Bullets (or other gameplay objects) activate, move, and rotate automatically.  
🔹 The world manages all updates and lifecycle.  
🔹 The pool ensures fast reuse with zero GC overhead.

---

Would you like me to extend this example to include **collision-based despawning** or **time-to-live (TTL)** logic?  
That would make it a complete real-world projectile lifecycle demo.
