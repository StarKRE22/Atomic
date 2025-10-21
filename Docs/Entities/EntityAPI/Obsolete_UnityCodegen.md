# Generating API through Unity Editor

To generate the Entity API through the **Unity Editor**, you first need to create a `.yaml` configuration file using the
menu: `Assets/Create/Atomic/Entities/EntityAPI`

Example config:

```yaml
directory: Assets/Scripts/
className: EntityAPI
namespace: SampleGame
entityType: IEntity
aggressiveInlining: true
unsafe: false

imports:

- Atomic.Entities
- Atomic.Elements
- SampleGame
- UnityEngine

tags:

- Player
- NPC

values:

- Health: int
- Speed: float
```

---

### 📑 Configuration options

| Option                 | Description                                                                                   | Default   |
|------------------------|-----------------------------------------------------------------------------------------------|-----------|
| **directory**          | Output path for the generated file                                                            | –         |
| **className**          | Name of the generated class and file                                                          | –         |
| **namespace**          | Namespace of the generated class                                                              | –         |
| **entityType**         | Entity type (can be `IEntity` or a custom type inheriting from `IEntity`)                     | `IEntity` |
| **aggressiveInlining** | Adds `[MethodImpl(MethodImplOptions.AggressiveInlining)]` to extension methods (true/false)   | `false`   |
| **unsafe**             | Optimization flag. Uses `GetValueUnsafe` instead of `GetValue` (faster but can cause crashes) | `false`   |
| **imports**            | List of namespaces (`using`) required for code generation                                     | –         |
| **tags**               | List of tags to generate (names only)                                                         | –         |
| **values**             | List of values to generate, in the format `Name: Type`                                        | –         |

> [!NOTE]  
> You can create multiple `.yaml` files if your project is large, and you want to split tags and values by feature.

---

After creating the config file, run: `Tools/Atomic/Entities/Compile Entity API`. This will generate an extension class based on your `.yaml` configuration.

```csharp
/**
* Code generation. Don't modify!
**/
public static class EntityAPI
{
    /// Tags
    public static readonly int Player;
    public static readonly int NPC;
  
    /// Values
    public static readonly int Health; // int
    public static readonly int Speed; // float
  
    static EntityAPI()
    {
        // Values
        Health = NameToId(nameof(Health));
        Speed = NameToId(nameof(Speed));
    }
  
    /// Tag Extensions
  
    #region Player
  
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool HasPlayerTag(this IEntity entity) => entity.HasTag(Player);
  
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool AddPlayerTag(this IEntity entity) => entity.AddTag(Player);
  
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool DelPlayerTag(this IEntity entity) => entity.DelTag(Player);
  
    #endregion
  
    #region NPC
  
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool HasNPCTag(this IEntity entity) => entity.HasTag(NPC);
  
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool AddNPCTag(this IEntity entity) => entity.AddTag(NPC);
  
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool DelNPCTag(this IEntity entity) => entity.DelTag(NPC);
  
    #endregion
  
    /// Value Extensions
  
    #region Health
  
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IVariable<int> GetHealth(this IEntity entity) => entity.GetValue<IVariable<int>>(Health);
  
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryGetHealth(this IEntity entity, out IVariable<int> value) => entity.TryGetValue(Health, out
    value);
  
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddHealth(this IEntity entity, IVariable<int> value) => entity.AddValue(Health, value);
  
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool HasHealth(this IEntity entity) => entity.HasValue(Health);
  
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool DelHealth(this IEntity entity) => entity.DelValue(Health);
  
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetHealth(this IEntity entity, IVariable<int> value) => entity.SetValue(Health, value);
  
    #endregion
  
    #region Speed
  
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IVariable<float> GetSpeed(this IEntity entity) => entity.GetValue<IVariable<float>>(Speed);
  
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryGetSpeed(this IEntity entity, out IVariable<float> value) => entity.TryGetValue(Speed, out
    value);
  
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddSpeed(this IEntity entity, IVariable<float> value) => entity.AddValue(Speed, value);
  
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool HasSpeed(this IEntity entity) => entity.HasValue(Speed);
  
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool DelSpeed(this IEntity entity) => entity.DelValue(Speed);
  
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetSpeed(this IEntity entity, IVariable<float> value) => entity.SetValue(Speed, value);
  
    #endregion
}
```

> [!NOTE]  
> Do not modify this class manually, as it will be overwritten automatically during regeneration.

---

If you change your `.yaml` configuration file, you can refresh the API by selecting:  
`Tools/Atomic/Entities/Refresh Entity API`

You can also configure code generation through:  
`Tools/Atomic/Entities/Select EntityAPI Settings`

From there, you may disable automatic updates via `AutoRefresh` or adjust the `AutoRefreshPeriod` interval.