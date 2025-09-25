# 🧩 InvokeRange Extensions 

Provide utility methods for invoking arrays or collections of [IAction](IActions.md). These methods automatically skip
`null` actions and execute them sequentially.

---

## 🏹 Methods

#### `InvokeRange(IEnumerable<IAction>)`

```csharp
public static void InvokeRange(this IEnumerable<IAction> actions)
```

- **Description:** Invokes all actions in the enumerable sequence. Null actions are safely skipped.
- **Parameter:** `actions` – A sequence of actions to invoke.

#### `InvokeRange<T>(IEnumerable<IAction<T>>, T)`

```csharp
public static void InvokeRange<T>(this IEnumerable<IAction<T>> actions, T arg)
```

- **Description:** Invokes all generic actions with a single argument. Null actions are skipped.
- **Type Parameter:** `T` – Type of the argument.
- **Parameters:**
  - `actions` – A sequence of actions to invoke.
  - `arg` – The argument to pass to each action.

#### `InvokeRange<T1, T2>(IEnumerable<IAction<T1, T2>>, T1, T2)`

```csharp
public static void InvokeRange<T1, T2>(this IEnumerable<IAction<T1, T2>> actions, T1 arg1, T2 arg2)
```

- **Description:** Invokes all generic actions with two arguments. Null actions are skipped.
- **Type Parameters:** `T1`, `T2` – Types of the arguments.
- **Parameters:**
  - `actions` – A sequence of actions to invoke.
  - `arg1` – First argument.
  - `arg2` – Second argument.

#### `InvokeRange<T1, T2, T3>(IEnumerable<IAction<T1, T2, T3>>, T1, T2, T3)`

```csharp
public static void InvokeRange<T1, T2, T3>(this IEnumerable<IAction<T1, T2, T3>> actions, T1 arg1, T2 arg2, T3 arg3)
```

- **Description:** Invokes all generic actions with three arguments. Null actions are skipped.
- **Type Parameters:** `T1`, `T2`, `T3` – Types of the arguments.
- **Parameters:**
  - `actions` – A sequence of actions to invoke.
  - `arg1` – First argument.
  - `arg2` – Second argument.
  - `arg3` – Third argument.

#### `InvokeRange<T1, T2, T3, T4>(IEnumerable<IAction<T1, T2, T3, T4>>, T1, T2, T3, T4)`

```csharp
public static void InvokeRange<T1, T2, T3, T4>(this IEnumerable<IAction<T1, T2, T3, T4>> actions, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
```

- **Description:** Invokes all generic actions with four arguments. Null actions are skipped.
- **Type Parameters:** `T1`, `T2`, `T3`, `T4` – Types of the arguments.
- **Parameters:**
  - `actions` – A sequence of actions to invoke.
  - `arg1` – First argument.
  - `arg2` – Second argument.
  - `arg3` – Third argument.
  - `arg4` – Fourth argument.

---

#### `InvokeRange(IAction[])`

```csharp
public static void InvokeRange(this IAction[] actions)
```

- **Description:** Invokes all actions in the array. Null actions are safely skipped.
- **Parameter:** `actions` – Array of actions to invoke.

#### `InvokeRange<T>(IAction<T>[], T)`

```csharp
public static void InvokeRange<T>(this IAction<T>[] actions, T arg)
```

- **Description:** Invokes all generic actions in the array with one argument. Null actions are skipped.
- **Type Parameter:** `T` – Type of the argument.
- **Parameters:**
  - `actions` – A sequence of actions to invoke.
  - `arg` – The argument to pass to each action.

#### `InvokeRange<T1, T2>(IAction<T1, T2>[], T1, T2)`

```csharp
public static void InvokeRange<T1, T2>(this IAction<T1, T2>[] actions, T1 arg1, T2 arg2)
```

- **Description:** Invokes all generic actions in the array with two arguments. Null actions are skipped.
- **Type Parameters:** `T1`, `T2` – Types of the arguments.
- **Parameters:**
  - `actions` – A sequence of actions to invoke.
  - `arg1` – First argument.
  - `arg2` – Second argument.

#### `InvokeRange<T1, T2, T3>(IAction<T1, T2, T3>[], T1, T2, T3)`

```csharp
public static void InvokeRange<T1, T2, T3>(this IAction<T1, T2, T3>[] actions, T1 arg1, T2 arg2, T3 arg3)
```

- **Description:** Invokes all generic actions in the array with three arguments. Null actions are skipped.
- **Type Parameters:** `T1`, `T2`, `T3` – Types of the arguments.
- **Parameters:**
  - `actions` – A sequence of actions to invoke.
  - `arg1` – First argument.
  - `arg2` – Second argument.
  - `arg3` – Third argument.

#### `InvokeRange<T1, T2, T3, T4>(IAction<T1, T2, T3, T4>[], T1, T2, T3, T4)`

```csharp
public static void InvokeRange<T1, T2, T3, T4>(this IAction<T1, T2, T3, T4>[] actions, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
```

- **Description:** Invokes all generic actions in the array with four arguments. Null actions are skipped.
- **Type Parameters:** `T1`, `T2`, `T3`, `T4` – Types of the arguments.
- **Parameters:**
  - `actions` – A sequence of actions to invoke.
  - `arg1` – First argument.
  - `arg2` – Second argument.
  - `arg3` – Third argument.
  - `arg4` – Fourth argument.

---

## 🗂 Examples of Usage

#### `IEnumerable<IAction>` (no parameters)
```csharp
IEnumerable<IAction> actions = new HashSet<IAction> { action1, null, action2 };
actions.InvokeRange(); // Invokes action1 and action2, skips null
```

#### `IEnumerable<IAction<T>>` (with one parameter)
```csharp
IEnumerable<IAction<GameObject>> actions = new HashSet<IAction<GameObject>> { action1, null };
actions.InvokeRange(someGameObject); // Invokes non-null actions
```

#### `IEnumerable<IAction<T1, T2>>` (with two parameters)
```csharp
IEnumerable<IAction<GameObject, int>> actions = new HashSet<IAction<GameObject, int>> { action1, action2 };
actions.InvokeRange(go, 5); // Invokes each non-null action
```

#### `IEnumerable<IAction<T1, T2, T3>>` (with three parameters)
```csharp
IEnumerable<IAction<GameObject, int, string>> actions = new HashSet<IAction<GameObject, int, string>> { action1 };
actions.InvokeRange(go, 5, "Hello"); // Invokes each non-null action
```

#### `IEnumerable<IAction<T1, T2, T3, T4>>` (with four parameters)
```csharp
IEnumerable<IAction<GameObject, int, string, bool>> actions = new HashSet<IAction<GameObject, int, string, bool>> { action1 };
actions.InvokeRange(go, 5, "Hello", true); // Invokes each non-null action
```

---

#### `IAction[]` (no parameters)
```csharp
IAction[] actions = new IAction[] { action1, null, action2 };
actions.InvokeRange(); // Invokes action1 and action2
```

#### `IAction<T>[]` (with one parameter)
```csharp
IAction<GameObject>[] actions = new IAction<GameObject>[] { action1, null };
actions.InvokeRange(someGameObject);
```

#### `IAction<T1, T2>[]` (with two parameters)
```csharp
IAction<GameObject, int>[] actions = new IAction<GameObject, int>[] { action1, action2 };
actions.InvokeRange(go, 5);
```

#### `IAction<T1, T2, T3>[]` (with three parameters)
```csharp
IAction<GameObject, int, string>[] actions = new IAction<GameObject, int, string>[] { action1 };
actions.InvokeRange(go, 5, "Hello");
```

#### `IAction<T1, T2, T3, T4>[]` (with four parameters)
```csharp
IAction<GameObject, int, string, bool>[] actions = new IAction<GameObject, int, string, bool>[] { action1 };
actions.InvokeRange(go, 5, "Hello", true);
```

Provide utility methods for invoking arrays or collections of [IAction](IActions.md). These methods automatically skip
`null` actions and execute them sequentially.

---

## 🏹 Methods

#### `InvokeRange(IEnumerable<IAction>)`

```csharp
public static void InvokeRange(this IEnumerable<IAction> actions)
```

- **Description:** Invokes all actions in the enumerable sequence. Null actions are safely skipped.
- **Parameter:** `actions` – A sequence of actions to invoke.

#### `InvokeRange<T>(IEnumerable<IAction<T>>, T)`

```csharp
public static void InvokeRange<T>(this IEnumerable<IAction<T>> actions, T arg)
```

- **Description:** Invokes all generic actions with a single argument. Null actions are skipped.
- **Type Parameter:** `T` – Type of the argument.
- **Parameters:**
    - `actions` – A sequence of actions to invoke.
    - `arg` – The argument to pass to each action.

#### `InvokeRange<T1, T2>(IEnumerable<IAction<T1, T2>>, T1, T2)`

```csharp
public static void InvokeRange<T1, T2>(this IEnumerable<IAction<T1, T2>> actions, T1 arg1, T2 arg2)
```

- **Description:** Invokes all generic actions with two arguments. Null actions are skipped.
- **Type Parameters:** `T1`, `T2` – Types of the arguments.
- **Parameters:**
    - `actions` – A sequence of actions to invoke.
    - `arg1` – First argument.
    - `arg2` – Second argument.

#### `InvokeRange<T1, T2, T3>(IEnumerable<IAction<T1, T2, T3>>, T1, T2, T3)`

```csharp
public static void InvokeRange<T1, T2, T3>(this IEnumerable<IAction<T1, T2, T3>> actions, T1 arg1, T2 arg2, T3 arg3)
```

- **Description:** Invokes all generic actions with three arguments. Null actions are skipped.
- **Type Parameters:** `T1`, `T2`, `T3` – Types of the arguments.
- **Parameters:**
    - `actions` – A sequence of actions to invoke.
    - `arg1` – First argument.
    - `arg2` – Second argument.
    - `arg3` – Third argument.

#### `InvokeRange<T1, T2, T3, T4>(IEnumerable<IAction<T1, T2, T3, T4>>, T1, T2, T3, T4)`

```csharp
public static void InvokeRange<T1, T2, T3, T4>(this IEnumerable<IAction<T1, T2, T3, T4>> actions, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
```

- **Description:** Invokes all generic actions with four arguments. Null actions are skipped.
- **Type Parameters:** `T1`, `T2`, `T3`, `T4` – Types of the arguments.
- **Parameters:**
    - `actions` – A sequence of actions to invoke.
    - `arg1` – First argument.
    - `arg2` – Second argument.
    - `arg3` – Third argument.
    - `arg4` – Fourth argument.

---

#### `InvokeRange(IAction[])`

```csharp
public static void InvokeRange(this IAction[] actions)
```

- **Description:** Invokes all actions in the array. Null actions are safely skipped.
- **Parameter:** `actions` – Array of actions to invoke.

#### `InvokeRange<T>(IAction<T>[], T)`

```csharp
public static void InvokeRange<T>(this IAction<T>[] actions, T arg)
```

- **Description:** Invokes all generic actions in the array with one argument. Null actions are skipped.
- **Type Parameter:** `T` – Type of the argument.
- **Parameters:**
    - `actions` – A sequence of actions to invoke.
    - `arg` – The argument to pass to each action.

#### `InvokeRange<T1, T2>(IAction<T1, T2>[], T1, T2)`

```csharp
public static void InvokeRange<T1, T2>(this IAction<T1, T2>[] actions, T1 arg1, T2 arg2)
```

- **Description:** Invokes all generic actions in the array with two arguments. Null actions are skipped.
- **Type Parameters:** `T1`, `T2` – Types of the arguments.
- **Parameters:**
    - `actions` – A sequence of actions to invoke.
    - `arg1` – First argument.
    - `arg2` – Second argument.

#### `InvokeRange<T1, T2, T3>(IAction<T1, T2, T3>[], T1, T2, T3)`

```csharp
public static void InvokeRange<T1, T2, T3>(this IAction<T1, T2, T3>[] actions, T1 arg1, T2 arg2, T3 arg3)
```

- **Description:** Invokes all generic actions in the array with three arguments. Null actions are skipped.
- **Type Parameters:** `T1`, `T2`, `T3` – Types of the arguments.
- **Parameters:**
  - `actions` – A sequence of actions to invoke.
  - `arg1` – First argument.
  - `arg2` – Second argument.
  - `arg3` – Third argument.

#### `InvokeRange<T1, T2, T3, T4>(IAction<T1, T2, T3, T4>[], T1, T2, T3, T4)`

```csharp
public static void InvokeRange<T1, T2, T3, T4>(this IAction<T1, T2, T3, T4>[] actions, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
```

- **Description:** Invokes all generic actions in the array with four arguments. Null actions are skipped.
- **Type Parameters:** `T1`, `T2`, `T3`, `T4` – Types of the arguments.
- **Parameters:**
  - `actions` – A sequence of actions to invoke.
  - `arg1` – First argument.
  - `arg2` – Second argument.
  - `arg3` – Third argument.
  - `arg4` – Fourth argument.

---

## 🗂 Examples of Usage

#### `IEnumerable<IAction>` (no parameters)
```csharp
IEnumerable<IAction> actions = new HashSet<IAction> { action1, null, action2 };
actions.InvokeRange(); // Invokes action1 and action2, skips null
```

#### `IEnumerable<IAction<T>>` (with one parameter)
```csharp
IEnumerable<IAction<GameObject>> actions = new HashSet<IAction<GameObject>> { action1, null };
actions.InvokeRange(someGameObject); // Invokes non-null actions
```

#### `IEnumerable<IAction<T1, T2>>` (with two parameters)
```csharp
IEnumerable<IAction<GameObject, int>> actions = new HashSet<IAction<GameObject, int>> { action1, action2 };
actions.InvokeRange(go, 5); // Invokes each non-null action
```

#### `IEnumerable<IAction<T1, T2, T3>>` (with three parameters)
```csharp
IEnumerable<IAction<GameObject, int, string>> actions = new HashSet<IAction<GameObject, int, string>> { action1 };
actions.InvokeRange(go, 5, "Hello"); // Invokes each non-null action
```

#### `IEnumerable<IAction<T1, T2, T3, T4>>` (with four parameters)
```csharp
IEnumerable<IAction<GameObject, int, string, bool>> actions = new HashSet<IAction<GameObject, int, string, bool>> { action1 };
actions.InvokeRange(go, 5, "Hello", true); // Invokes each non-null action
```

---

#### `IAction[]` (no parameters)
```csharp
IAction[] actions = new IAction[] { action1, null, action2 };
actions.InvokeRange(); // Invokes action1 and action2
```

#### `IAction<T>[]` (with one parameter)
```csharp
IAction<GameObject>[] actions = new IAction<GameObject>[] { action1, null };
actions.InvokeRange(someGameObject);
```

#### `IAction<T1, T2>[]` (with two parameters)
```csharp
IAction<GameObject, int>[] actions = new IAction<GameObject, int>[] { action1, action2 };
actions.InvokeRange(go, 5);
```

#### `IAction<T1, T2, T3>[]` (with three parameters)
```csharp
IAction<GameObject, int, string>[] actions = new IAction<GameObject, int, string>[] { action1 };
actions.InvokeRange(go, 5, "Hello");
```

#### `IAction<T1, T2, T3, T4>[]` (with four parameters)
```csharp
IAction<GameObject, int, string, bool>[] actions = new IAction<GameObject, int, string, bool>[] { action1 };
actions.InvokeRange(go, 5, "Hello", true);
```