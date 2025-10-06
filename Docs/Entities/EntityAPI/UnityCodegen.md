
### Генерация через Unity Editor

Для генерации через Unity Editor


Примечание, можно делать несколько таких .yaml файлов


#### Пример использования


Sometimes managing tags by raw `int` keys or `string` names can get messy and error-prone, especially in big projects.
To
make this process easier and **type-safe**, the Atomic Framework supports **code generation**. This means you describe
all your tags (and values) once in a small config file, and the framework will automatically generate C# helpers. You
can learn more about this in the Manual under
the [Entity API Generation](../Manual.md/#-generate-entity-api) section.

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
  - Player
  - NPC

values:
```

- `namespace` — the namespace of the generated code
- `tags` — list of tags that will be turned into constants
- `values` — same for values (empty in this example)

---

**Step 2:** Based on this config, the framework creates a **static API class**:

```csharp
/**
 * Code generation. Don't modify! 
 **/

public static class EntityAPI
{

    ///Tags
    public static readonly int Player;
    public static readonly int NPC;

    ///Values

    static GameEntityAPI()
    {
        //Tags
        Player = NameToId(nameof(Player));
        NPC = NameToId(nameof(NPC));

        //Values
    }


    ///Tag Extensions

    #region Player
    public static bool HasPlayerTag(this IGameEntity entity) => entity.HasTag(Player);
    public static bool AddPlayerTag(this IGameEntity entity) => entity.AddTag(Player);
    public static bool DelPlayerTag(this IGameEntity entity) => entity.DelTag(Player);
    #endregion
    
    #region NPC
    public static bool HasNPCTag(this IGameEntity entity) => entity.HasTag(NPC);
    public static bool AddNPCTag(this IGameEntity entity) => entity.AddTag(NPC);
    public static bool DelNPCTag(this IGameEntity entity) => entity.DelTag(NPC);
    #endregion
}
```

**Step 3:** Now you get ready-to-use methods for each tag: `AddPlayerTag()`, `HasPlayerTag()`, `DelPlayerTag()`, etc. No
more “magic
strings” or manual ID lookups.
```csharp
// Create a new entity
IEntity entity = new Entity();

// Add tags by string name
entity.AddPlayerTag();
entity.AddNPCTag(); // Get numeric ID

// Check tags
if (entity.HasPlayerTag())
    Console.WriteLine("Entity is a Player");

// Remove a tag
entity.DelNPCTag();
```