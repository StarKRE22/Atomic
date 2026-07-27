# 🧩 MonoActionReference

A family of **serializable reference wrappers** for invoking [MonoAction](MonoAction.md) components from outside their scene context. Each variant matches the arity of the referenced action and implements the corresponding [IAction](IAction.md) interface.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
  - [MonoActionReference](#-monoactionreference)
    - [Type](#type)
    - [Constructors](#constructors)
    - [Fields](#fields)
    - [Methods](#methods)
  - [MonoActionReference&lt;T&gt;](#-monoactionreferencet)
    - [Type](#type-1)
    - [Constructors](#constructors-1)
    - [Fields](#fields-1)
    - [Methods](#methods-1)
  - [MonoActionReference&lt;T1, T2&gt;](#-monoactionreferencet1-t2)
    - [Type](#type-2)
    - [Constructors](#constructors-2)
    - [Fields](#fields-2)
    - [Methods](#methods-2)
  - [MonoActionReference&lt;T1, T2, T3&gt;](#-monoactionreferencet1-t2-t3)
    - [Type](#type-3)
    - [Constructors](#constructors-3)
    - [Fields](#fields-3)
    - [Methods](#methods-3)
  - [MonoActionReference&lt;T1, T2, T3, T4&gt;](#-monoactionreferencet1-t2-t3-t4)
    - [Type](#type-4)
    - [Constructors](#constructors-4)
    - [Fields](#fields-4)
    - [Methods](#methods-4)

---

## 🗂 Example of Usage

Reference a parameterless scene action and invoke it from code:

```csharp
public sealed class HelloWorldMonoAction : MonoAction
{
    public override void Invoke() => Debug.Log("Hello World!");
}

public sealed class GameStartup : MonoBehaviour
{
    [SerializeField]
    private MonoActionReference _reference;

    private void Start()
    {
        _reference.Invoke();
    }
}
```

Reference a single-argument scene action:

```csharp
public sealed class DestroyGameObjectMonoAction : MonoAction<GameObject>
{
    public override void Invoke(GameObject arg) => GameObject.Destroy(arg);
}

public sealed class GameStartup : MonoBehaviour
{
    [SerializeField]
    private MonoActionReference<GameObject> _reference;

    private void Start()
    {
        _reference.Invoke(gameObject);
    }
}
```

---

## 🔍 API Reference

### 🏛️ MonoActionReference

#### Type

```csharp
[Serializable]
public sealed class MonoActionReference : IAction
```

- **Description:** A parameterless reference wrapper for a [MonoAction](MonoAction.md).
- **Inheritance:** [IAction](IAction.md)
- **Type Parameters:** None
- **Notes:** Supports Unity serialization and Odin Inspector.
- **See also:** [MonoAction](MonoAction.md), [MonoActionReference&lt;T&gt;](#-monoactionreferencet)

#### Constructors

##### `MonoActionReference()`

```csharp
public MonoActionReference();
```

- **Description:** Default constructor intended **only for use by the Unity Inspector**.
- **Notes:** Required for Unity serialization.

##### `MonoActionReference(MonoAction)`

```csharp
public MonoActionReference(MonoAction action);
```

- **Description:** Creates a new reference wrapping the specified scene action.
- **Parameter:** `action` — The [MonoAction](MonoAction.md) to invoke.

#### Fields

##### `action`

```csharp
[SerializeField]
#if ODIN_INSPECTOR
[SceneObjectsOnly, Required]
#endif
public MonoAction action;
```

- **Description:** Reference to the scene action to invoke.
- **Access:** Read / Write
- **Notes:** Restricted to scene objects when Odin Inspector is available.

#### Methods

##### `Invoke()`

```csharp
#if ODIN_INSPECTOR
[HideInEditorMode]
[GUIColor(0, 1, 0)]
[Button]
#endif
public void Invoke();
```

- **Description:** Invokes the referenced scene action, if it exists.

---

### 🏛️ MonoActionReference&lt;T&gt;

#### Type

```csharp
[Serializable]
public sealed class MonoActionReference<T> : IAction<T>
```

- **Description:** A reference wrapper for a [MonoAction&lt;T&gt;](MonoAction%601.md) with one parameter.
- **Inheritance:** [IAction&lt;T&gt;](IAction%601.md)
- **Type Parameters:** `T` — The argument type.
- **Notes:** Supports Unity serialization and Odin Inspector.
- **See also:** [MonoAction&lt;T&gt;](MonoAction%601.md), [MonoActionReference&lt;T1, T2&gt;](#-monoactionreferencet1-t2)

#### Constructors

##### `MonoActionReference<T>()`

```csharp
public MonoActionReference();
```

- **Description:** Default constructor intended **only for use by the Unity Inspector**.
- **Notes:** Required for Unity serialization.

##### `MonoActionReference(MonoAction<T>)`

```csharp
public MonoActionReference(MonoAction<T> action);
```

- **Description:** Creates a new reference wrapping the specified scene action.
- **Parameter:** `action` — The [MonoAction&lt;T&gt;](MonoAction%601.md) to invoke.

#### Fields

##### `action`

```csharp
[SerializeField]
#if ODIN_INSPECTOR
[SceneObjectsOnly, Required]
#endif
public MonoAction<T> action;
```

- **Description:** Reference to the scene action to invoke.
- **Access:** Read / Write
- **Notes:** Restricted to scene objects when Odin Inspector is available.

#### Methods

##### `Invoke(T)`

```csharp
#if ODIN_INSPECTOR
[HideInEditorMode]
[GUIColor(0, 1, 0)]
[Button]
#endif
public void Invoke(T arg);
```

- **Description:** Invokes the referenced scene action with the provided argument.
- **Parameter:** `arg` — The argument to pass to the action.

---

### 🏛️ MonoActionReference&lt;T1, T2&gt;

#### Type

```csharp
[Serializable]
public sealed class MonoActionReference<T1, T2> : IAction<T1, T2>
```

- **Description:** A reference wrapper for a [MonoAction&lt;T1, T2&gt;](MonoAction%602.md) with two parameters.
- **Inheritance:** [IAction&lt;T1, T2&gt;](IAction%602.md)
- **Type Parameters:**
  - `T1` — The first argument type.
  - `T2` — The second argument type.
- **Notes:** Supports Unity serialization and Odin Inspector.
- **See also:** [MonoAction&lt;T1, T2&gt;](MonoAction%602.md)

#### Constructors

##### `MonoActionReference<T1, T2>()`

```csharp
public MonoActionReference();
```

- **Description:** Default constructor intended **only for use by the Unity Inspector**.
- **Notes:** Required for Unity serialization.

##### `MonoActionReference(MonoAction<T1, T2>)`

```csharp
public MonoActionReference(MonoAction<T1, T2> action);
```

- **Description:** Creates a new reference wrapping the specified scene action.
- **Parameter:** `action` — The [MonoAction&lt;T1, T2&gt;](MonoAction%602.md) to invoke.

#### Fields

##### `action`

```csharp
[SerializeField]
#if ODIN_INSPECTOR
[SceneObjectsOnly, Required]
#endif
public MonoAction<T1, T2> action;
```

- **Description:** Reference to the scene action to invoke.
- **Access:** Read / Write
- **Notes:** Restricted to scene objects when Odin Inspector is available.

#### Methods

##### `Invoke(T1, T2)`

```csharp
#if ODIN_INSPECTOR
[HideInEditorMode]
[GUIColor(0, 1, 0)]
[Button]
#endif
public void Invoke(T1 arg1, T2 arg2);
```

- **Description:** Invokes the referenced scene action with the provided arguments.
- **Parameters:**
  - `arg1` — The first argument.
  - `arg2` — The second argument.

---

### 🏛️ MonoActionReference&lt;T1, T2, T3&gt;

#### Type

```csharp
[Serializable]
public sealed class MonoActionReference<T1, T2, T3> : IAction<T1, T2, T3>
```

- **Description:** A reference wrapper for a [MonoAction&lt;T1, T2, T3&gt;](MonoAction%603.md) with three parameters.
- **Inheritance:** [IAction&lt;T1, T2, T3&gt;](IAction%603.md)
- **Type Parameters:**
  - `T1` — The first argument type.
  - `T2` — The second argument type.
  - `T3` — The third argument type.
- **Notes:** Supports Unity serialization and Odin Inspector.
- **See also:** [MonoAction&lt;T1, T2, T3&gt;](MonoAction%603.md)

#### Constructors

##### `MonoActionReference<T1, T2, T3>()`

```csharp
public MonoActionReference();
```

- **Description:** Default constructor intended **only for use by the Unity Inspector**.
- **Notes:** Required for Unity serialization.

##### `MonoActionReference(MonoAction<T1, T2, T3>)`

```csharp
public MonoActionReference(MonoAction<T1, T2, T3> action);
```

- **Description:** Creates a new reference wrapping the specified scene action.
- **Parameter:** `action` — The [MonoAction&lt;T1, T2, T3&gt;](MonoAction%603.md) to invoke.

#### Fields

##### `action`

```csharp
[SerializeField]
#if ODIN_INSPECTOR
[SceneObjectsOnly, Required]
#endif
public MonoAction<T1, T2, T3> action;
```

- **Description:** Reference to the scene action to invoke.
- **Access:** Read / Write
- **Notes:** Restricted to scene objects when Odin Inspector is available.

#### Methods

##### `Invoke(T1, T2, T3)`

```csharp
#if ODIN_INSPECTOR
[HideInEditorMode]
[GUIColor(0, 1, 0)]
[Button]
#endif
public void Invoke(T1 arg1, T2 arg2, T3 arg3);
```

- **Description:** Invokes the referenced scene action with the provided arguments.
- **Parameters:**
  - `arg1` — The first argument.
  - `arg2` — The second argument.
  - `arg3` — The third argument.

---

### 🏛️ MonoActionReference&lt;T1, T2, T3, T4&gt;

#### Type

```csharp
[Serializable]
public sealed class MonoActionReference<T1, T2, T3, T4> : IAction<T1, T2, T3, T4>
```

- **Description:** A reference wrapper for a [MonoAction&lt;T1, T2, T3, T4&gt;](MonoAction%604.md) with four parameters.
- **Inheritance:** [IAction&lt;T1, T2, T3, T4&gt;](IAction%604.md)
- **Type Parameters:**
  - `T1` — The first argument type.
  - `T2` — The second argument type.
  - `T3` — The third argument type.
  - `T4` — The fourth argument type.
- **Notes:** Supports Unity serialization and Odin Inspector.
- **See also:** [MonoAction&lt;T1, T2, T3, T4&gt;](MonoAction%604.md)

#### Constructors

##### `MonoActionReference<T1, T2, T3, T4>()`

```csharp
public MonoActionReference();
```

- **Description:** Default constructor intended **only for use by the Unity Inspector**.
- **Notes:** Required for Unity serialization.

##### `MonoActionReference(MonoAction<T1, T2, T3, T4>)`

```csharp
public MonoActionReference(MonoAction<T1, T2, T3, T4> action);
```

- **Description:** Creates a new reference wrapping the specified scene action.
- **Parameter:** `action` — The [MonoAction&lt;T1, T2, T3, T4&gt;](MonoAction%604.md) to invoke.

#### Fields

##### `action`

```csharp
[SerializeField]
#if ODIN_INSPECTOR
[SceneObjectsOnly, Required]
#endif
public MonoAction<T1, T2, T3, T4> action;
```

- **Description:** Reference to the scene action to invoke.
- **Access:** Read / Write
- **Notes:** Restricted to scene objects when Odin Inspector is available.

#### Methods

##### `Invoke(T1, T2, T3, T4)`

```csharp
#if ODIN_INSPECTOR
[HideInEditorMode]
[GUIColor(0, 1, 0)]
[Button]
#endif
public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
```

- **Description:** Invokes the referenced scene action with the provided arguments.
- **Parameters:**
  - `arg1` — The first argument.
  - `arg2` — The second argument.
  - `arg3` — The third argument.
  - `arg4` — The fourth argument.
