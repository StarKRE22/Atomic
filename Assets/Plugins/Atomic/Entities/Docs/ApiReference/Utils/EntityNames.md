# 🧩 EntityNames

Provides a **bidirectional mapping** between string entity names and unique integer identifiers.  
Useful for efficiently identifying entities at runtime with compact integer IDs while allowing reverse lookup for debugging or editor visualization.

> [!NOTE]  
> The IDs are generated at **runtime** and may vary between different runs for the same string.  
> Therefore, these IDs **must not be serialized or saved**, as they are not stable across sessions.

---

## EntityNames
```csharp
public static class EntityNames
```

---

## Methods

```csharp
public static int NameToId(string name)
```
- Converts a string entity name to a unique integer ID.
- If the name is already registered, the existing ID is returned.
- Otherwise, a new ID is assigned and stored.
- **Parameters:**
    - `name` — The string name to convert to an ID.
- **Returns:**
    - A unique integer identifier corresponding to the provided name.

---

```csharp
public static string IdToName(int id)
```
- Retrieves the original entity name from a given integer ID.
- **Parameters:**
    - `id` — The ID to convert back to a string name.
- **Returns:**
    - The original string name associated with the given ID,
    - or a fallback string in the format `<c>#Unknown:{id}</c>` if the ID was not registered.

---

```csharp
public static void Clear()
```
- Clears all name-to-ID mappings and resets the ID counter.
- Automatically called when entering play mode in the Unity Editor.
- **Note:** Only relevant in editor context; in builds, this method can be called manually if needed.

---

## Example Usage
```csharp
int playerId = EntityNames.NameToId("Player");
int enemyId = EntityNames.NameToId("Enemy");
```

```csharp
string name = EntityNames.IdToName(playerId); // "Player"
string unknown = EntityNames.IdToName(999);   // "#Unknown:999"
```

```csharp
// Clear all mappings (editor-only auto-call on play mode)
EntityNames.Clear();
```
