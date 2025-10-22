# 📌 Combine EntityPool with EntityFactory

Combining an [EntityFactory](../Entities/Factories/Manual.md) with an [EntityPool](../Entities/Pooling/Manual.md) allows
you to **efficiently reuse entities** without constantly allocating new objects. This is especially useful for bullets,
projectiles, enemies, or any frequently spawned gameplay objects.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
    - [Basic Pool Initialization](#ex1)
    - [Renting and Returning Entities](#ex2)
- [Benefits](#-benefits)

---

## 🗂 Examples of Usage

<div id="ex1"></div>

### 1️⃣ Basic Pool Initialization

1. Assume we have a specific type for entity bullets

```csharp
public class BulletEntity : Entity 
{
}
```

2. Create a special factory for the bullet

```csharp
public class BulletFactory : ScriptableEntityFactory<BulletEntity>
{
    public override BulletEntity Create() => new BulletEntity();
}
```

3. Usage in Runtime

```csharp
var factory = new BulletFactory();
var pool = new EntityPool<BulletEntity>(factory);

// Initialize a pool with 20 bullets
pool.Init(20);
```

**Explanation:**

- `BulletEntity` is a simple entity type.
- `BulletFactory` handles creation of new bullets.
- `EntityPool` pre-instantiates 20 bullets to **avoid runtime allocations** during gameplay.

---

<div id="ex2"></div>

### 2️⃣ Renting and Returning Entities

```csharp
// Rent a bullet from the pool
BulletEntity bullet = pool.Rent();

// Use it in gameplay
bullet.Set("Damage", 10);

// Return it when done
pool.Return(bullet);
```

**Explanation:**

- `Rent()` gives you an available entity from the pool.
- You can configure it (e.g., set damage, position, or target).
- `Return()` puts the entity back into the pool for **reuse**, reducing garbage collection and improving performance.

---

## ✅ Benefits

- Reduces **runtime allocations** and garbage collection.
- Works well for **high-frequency objects** like bullets, projectiles, or visual effects.
- Keeps **entity creation logic centralized** via factories while enabling **efficient reuse** via pools.