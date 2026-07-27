# 🧩 ExpandMode

An enum that determines how an entity pool behaves when it runs out of pre-instantiated entities and a new entity is requested via `Rent()`.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
  - [Type](#-type)
  - [Fields](#-fields)
    - [ExpandByOne](#expandbyone)
    - [ExpandByDoubling](#expandbydoubling)
    - [NoExpand](#noexpand)

---

## 🗂 Example of Usage

Create a pool that doubles when empty:

```csharp
IEntityFactory<GameEntity> factory = ...;

var pool = new EntityPool<GameEntity>(factory, new Args(), ExpandMode.ExpandByDoubling);
pool.Init(10);

var e1 = pool.Rent(); // pool has 9 left
// ... rent all 10 ...
var e11 = pool.Rent(); // pool is empty; creates 10 new entities
```

Enforce a fixed-size pool:

```csharp
var pool = new EntityPool<GameEntity>(factory, new Args(), ExpandMode.NoExpand);
pool.Init(5);

var e1 = pool.Rent();
var e2 = pool.Rent();
var e3 = pool.Rent();
var e4 = pool.Rent();
var e5 = pool.Rent();

var e6 = pool.Rent(); // throws InvalidOperationException
```

---

## 🔍 API Reference

### 🏛️ Type

```csharp
public enum ExpandMode
```

- **Description:** Determines how a pool behaves when it runs out of pre-instantiated entities and a new entity is requested via `Rent()`.
- **Inheritance:** `Enum`
- **Notes:** Used by all pool implementations.
- **See also:** [EntityPool&lt;E&gt;](EntityPool%601.md), [MonoEntityPool&lt;E&gt;](MonoEntityPool%601.md), [PrefabEntityPool&lt;E&gt;](PrefabEntityPool%601.md), [MultiEntityPool&lt;K, E&gt;](MultiEntityPool%601.md)

---

### 🗂️ Fields

#### `ExpandByOne`

```csharp
ExpandByOne = 0
```

- **Description:** Creates one new entity each time the pool is empty. This is the default behavior.
- **Growth:** Linear (+1 per expansion).

#### `ExpandByDoubling`

```csharp
ExpandByDoubling = 1
```

- **Description:** When the pool is empty, creates new entities equal to the current pooled count, effectively doubling the inventory. If the pool has never been populated, creates one entity as a seed.
- **Growth:** Exponential, scaling with demand.
- **Notes:** Useful for reducing frequent small allocations.

#### `NoExpand`

```csharp
NoExpand = 2
```

- **Description:** Throws an `InvalidOperationException` when the pool is empty and a new entity is requested.
- **Growth:** None (fixed size).
- **Notes:** Use this to enforce a fixed pool size.
