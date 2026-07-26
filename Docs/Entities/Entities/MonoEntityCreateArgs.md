# 🧩 CreateArgs

Defines a set of parameters for creating a dynamic entity.


---

## 📑 Table of Contents

- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Fields](#-fields)
        - [Name](#name)
        - [Tags](#tags)
        - [Values](#values)
        - [Behaviours](#behaviours)
        - [SceneInstallers](#sceneinstallers)
        - [ScriptableInstallers](#scriptableinstallers)
        - [Children](#children)
        - [InitialTagCapacity](#initialtagcapacity)
        - [InitialValueCapacity](#initialvaluecapacity)
        - [InitialBehaviourCapacity](#initialbehaviourcapacity)
        - [InstallOnAwake](#installonawake)
        - [UninstallOnDestroy](#uninstallondestroy)
        - [DisposeValues](#disposevalues)
        - [UseUnityLifecycle](#useunitylifecycle)

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
[Serializable]  
public struct CreateArgs
```

---

### 🧱 Fields

#### `Name`

```csharp
public string name;
```

- **Description:** Name of the entity (Unity object name).

#### `Tags`

```csharp
public IEnumerable<int> tags;
```

- **Description:** Optional tags to assign to the entity.

#### `Values`

```csharp
public IReadOnlyDictionary<int, object> values;
```

- **Description:** Optional key-value pairs assigned to the entity.

#### `Behaviours`

```csharp
public IEnumerable<IEntityBehaviour> behaviours;
```

- **Description:** Optional behaviours attached to the entity.

#### `SceneInstallers`

```csharp
public List<MonoEntityInstaller> sceneInstallers;
```

- **Description:** Optional **MonoBehaviour installers** to run in the scene.

#### `ScriptableInstallers`

```csharp
public List<ScriptableEntityInstaller> scriptableInstallers;
```

- **Description:** Optional **ScriptableObject installers** to run.

#### `Children`

```csharp
public List<MonoEntity> children;
```

- **Description:** Optional child entities attached to this entity.

#### `InitialTagCapacity`

```csharp
public int initialTagCapacity;
```

- **Description:** Initial capacity for tags.

#### `InitialValueCapacity`

```csharp
public int initialValueCapacity;
```

- **Description:** Initial capacity for values.

#### `InitialBehaviourCapacity`

```csharp
public int initialBehaviourCapacity;
```

- **Description:** Initial capacity for behaviours.

#### `InstallOnAwake`

```csharp
public bool installOnAwake;
```

- **Description:** If true, the entity installs automatically on **Awake**.

#### `UninstallOnDestroy`

```csharp
public bool uninstallOnDestroy;
```

- **Description:** If true, the entity uninstalls automatically on **Destroy**.

#### `DisposeValues`

```csharp
public bool disposeValues;
```

- **Description:** If true, values are disposed when the entity is destroyed.

#### `UseUnityLifecycle`

```csharp
public bool useUnityLifecycle;
```

- **Description:** If true, uses Unity lifecycle methods (**Awake**, **OnEnable**, **OnDisable**, **OnDestroy**).
