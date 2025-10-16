# 🧩 EntityNames

A **static utility class** providing bidirectional mapping between string-based entity names and unique integer
identifiers, with internal caching for fast reverse lookups. This class allows you to switch the underlying ID
generation algorithm while maintaining a central cache of names and IDs.

---

## 📑 Table of Contents

<ul>
  <li><a href="#-example-of-usage">Example of Usage</a></li>
  <li>
    <a href="#-api-reference">API Reference</a>
    <ul>
      <li><a href="#-type">Type</a></li>
      <li><a href="#-methods">Methods</a>
          <ul>
            <li><a href="#setalgorithm">SetAlgorithm(IEntityNameAlgorithm)</a></li>
            <li><a href="#nametoid">NameToId(string)</a></li>
            <li><a href="#idtoname">IdToName(int)</a></li>
            <li><a href="#reset">Reset()</a></li>
          </ul>
      </li>
    </ul>
  </li>
</ul>

---

## 🗂 Example of Usage

```csharp
// Set a custom algorithm
EntityNames.SetAlgorithm(new HashEntityNameAlgorithm());

// Convert names to IDs
int playerId = EntityNames.NameToId("Player");
int enemyId = EntityNames.NameToId("Enemy");

// Retrieve names from IDs
string name1 = EntityNames.IdToName(playerId); // "Player"
string name2 = EntityNames.IdToName(enemyId);  // "Enemy"
string unknown = EntityNames.IdToName(999);   // "#Unknown:999"

// Reset cache and algorithm state
EntityNames.Reset();
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public static class EntityNames
````

- **Inheritance:** `System.Object`
- **Thread-safety:** Static dictionaries are **not thread-safe**; synchronize externally if used from multiple threads.

---

### 🏹 Methods

<div id="setalgorithm"></div>

#### `SetAlgorithm(IEntityNameAlgorithm)`

```csharp
public static void SetAlgorithm(IEntityNameAlgorithm algorithm)
````

- **Description:** Sets the algorithm used for generating IDs from entity names and **clears the current cache**.
- **Parameter:** `algorithm` — The new strategy to use. Must not be `null`.
- **Exceptions:** Throws `ArgumentNullException` if `algorithm` is `null`.
- **Remarks:** Changing the algorithm resets all existing mappings.

<div id="nametoid"></div>

#### `NameToId(string)`

```csharp
public static int NameToId(string name)
```

- **Description:** Converts a string entity name into a unique integer ID.
- **Parameter:** `name` — The entity name. Must not be `null`.
- **Returns:** A unique integer ID corresponding to the name.
- **Exceptions:** Throws `ArgumentNullException` if `name` is `null`.
- **Behavior:**
    - Returns a cached ID if the name was already converted.
    - Otherwise, computes the ID using the current algorithm, caches it, and returns it.

<div id="idtoname"></div>

#### `IdToName(int)`

```csharp
public static string IdToName(int id)
````

- **Description:** Retrieves the original entity name associated with a given ID.
- **Parameter:** `id` — The integer ID to look up.
- **Returns:**
    - The original name if registered.
    - Otherwise, a fallback string in the format `#Unknown:{id}`.

#### `Reset()`

```csharp
public static void Reset()
````

- **Description:** Clears all cached mappings and resets the current algorithm.
- **Remarks:** Automatically called in the Unity Editor when entering Play Mode if annotated with
  `[InitializeOnEnterPlayMode]`.

<!--

# 🧩 EntityNames

```csharp
public static class EntityNames
```

- **Description:** Provides a **bidirectional mapping** between string entity names and unique integer identifiers.  
  Useful for efficiently identifying entities at runtime with compact integer IDs while allowing reverse lookup for
  debugging or editor visualization.

> [!NOTE]  
> The IDs are generated at **runtime** and may vary between different runs for the same string.  
> Therefore, these IDs **must not be serialized or saved**, as they are not stable across sessions.

---

## 🏹 Static Methods

```csharp
public static int NameToId(string name)
```

- **Description:** Converts a string entity name to a unique integer ID. If the name is already registered, the existing
  ID is returned. Otherwise, a new ID is assigned and stored.
- **Parameter:** `name` — The string name to convert to an ID.
- **Returns:** A unique integer identifier corresponding to the provided name.

```csharp
public static string IdToName(int id)
```

- **Description:** Retrieves the original entity name from a given integer ID.
- **Parameter:** `id` — The ID to convert back to a string name.
- **Returns:** The original string name associated with the given ID, or a fallback string in the format
  `#Unknown:{id}` if the ID was not registered.

```csharp
public static void Clear()
```

- **Description:** Clears all name-to-ID mappings and resets the ID counter. Automatically called when entering play
  mode in the Unity Editor.
- **Note:** Only relevant in editor context; in builds, this method can be called manually if needed.

---

## 🗂 Examples of Usage

```csharp
//Get int key by string key
int playerId = EntityNames.NameToId("Player");
int enemyId = EntityNames.NameToId("Enemy");
```

```csharp
//Get string key by int key 
string name = EntityNames.IdToName(playerId); // "Player"
string unknown = EntityNames.IdToName(999);   // "#Unknown:999"
```

```csharp
// Clear all mappings (editor-only auto-call on play mode)
EntityNames.Clear();
```

-->