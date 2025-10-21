
#### 3️⃣ Using Code Generation






**Step 1:** Create a `.yaml` file where you list all your tags and values:

```yaml
header: EntityAPI
entityType: IEntity
aggressiveInlining: true
namespace: PROJECT_NAMESPACE
className: EntityAPI
directory: CODE_GENERATION_PATH

imports:

  - UnityEngine
  - Atomic.Entities
  - Atomic.Elements

tags:

values:

  - Health: int
  - Speed: float
  - Inventory: IInventory
```

---

**Step 2:** Generated code looks like this:

```csharp
/**
 * Code generation. Don't modify! 
 **/
public static class EntityAPI
{
    ///Values
    public static readonly int Health; //int
    public static readonly int Speed; //float
    public static readonly int Inventory; //IInventory

    static EntityAPI()
    {
        // Values
        Health = NameToId(nameof(Health));
        Speed = NameToId(nameof(Speed));
        Inventory = NameToId(nameof(Inventory));
    }

    ///Value Extensions
    
    #region Health
    public static IVariable<int> GetHealth(this IEntity entity) => entity.GetValue<IVariable<int>>(Health);
    public static bool TryGetHealth(this IEntity entity, out IVariable<int> value) => entity.TryGetValue(Health, out value);
    public static void AddHealth(this IEntity entity, IVariable<int> value) => entity.AddValue(Health, value);
    public static bool HasHealth(this IEntity entity) => entity.HasValue(Health);
    public static bool DelHealth(this IEntity entity) => entity.DelValue(Health);
    public static void SetHealth(this IEntity entity, IVariable<int> value) => entity.SetValue(Health, value);
    #endregion
    
    #region Speed
    public static IVariable<float> GetSpeed(this IEntity entity) => entity.GetValue<IVariable<float>>(Speed);
    public static bool TryGetSpeed(this IEntity entity, out IVariable<float> value) => entity.TryGetValue(Speed, out value);
    public static void AddSpeed(this IEntity entity, IVariable<float> value) => entity.AddValue(Speed, value);
    public static bool HasSpeed(this IEntity entity) => entity.HasValue(Speed);
    public static bool DelSpeed(this IEntity entity) => entity.DelValue(Speed);
    public static void SetSpeed(this IEntity entity, IVariable<float> value) => entity.SetValue(Speed, value);
    #endregion
    
    #region Inventory
    public static IVariable<IInventory> GetInventory(this IEntity entity) => entity.GetValue<IVariable<IInventory>>(Inventory);
    public static bool TryGetInventory(this IEntity entity, out IVariable<IInventory> value) => entity.TryGetValue(Inventory, out value);
    public static void AddInventory(this IEntity entity, IVariable<IInventory> value) => entity.AddValue(Inventory, value);
    public static bool HasInventory(this IEntity entity) => entity.HasValue(Inventory);
    public static bool DelInventory(this IEntity entity) => entity.DelValue(Inventory);
    public static void SetInventory(this IEntity entity, IVariable<IInventory> value) => entity.SetValue(Inventory, value);
    #endregion
}
```

---

**Step 3:** Now you get ready-to-use methods for each value:

```csharp
// Create a new entity
IEntity entity = new Entity();

// Add values by string key
entity.AddHealth(100);
entity.AddSpeed(12.5f);
entity.AddInventory(new GridInventory());

// Get a value
int health = entity.GetHealth();
Console.WriteLine($"Health: {health}");

// Update a value
entity.SetHealth(150);

// Remove a value
entity.DelInventory();
```