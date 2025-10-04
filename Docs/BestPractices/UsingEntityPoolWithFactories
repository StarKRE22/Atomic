
## Example Usage

### 1. Basic Pool Initialization
```csharp
// Define a simple entity
public class BulletEntity : Entity { }

// Create a factory for the entity
public class BulletFactory : ScriptableEntityFactory<BulletEntity>
{
    public override BulletEntity Create() => new BulletEntity();
}

// Initialize a pool with 20 bullets
var factory = new BulletFactory();
var pool = new EntityPool<BulletEntity>(factory);
pool.Init(20);
```
---

### 2. Renting and Returning Entities

```csharp
// Rent a bullet from the pool
BulletEntity bullet = pool.Rent();

// Use it in gameplay
bullet.Set("Damage", 10);

// Return it when done
pool.Return(bullet);
```
