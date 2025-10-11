# 🧩 CompositeAction

Represents a group of **parameterless actions** that are executed sequentially.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Constructors](#-constructors)
      - [Default Constructor](#default-constructor)
      - [Params Constructor](#params-constructor)
      - [IEnumerable Constructor](#ienumerable-constructor)
    - [Methods](#-methods)
        - [Invoke()](#invoke)

---

## 🗂 Example of Usage

Below is an example of using composite action for game startup:

```csharp
IAction startupAction = new CompositeAction(
    new ActivatePlayerAction(), //IAction
    new ActivateEnemiesAction(), //IAction
    new ActivateWeapons(), //IAction
    new ActivateGameTimerAction(), //IAction
);

```

Usage:

```csharp
startupAction.Invoke();
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
[Serializable]
public class CompositeAction : IAction
```

- **Description:** Represents a group of **parameterless actions** that are executed sequentially.
- **Inheritance:** [IAction](IAction.md)
- **Notes:** Supports Unity serialization and Odin Inspector

---

### 🏗️ Constructors <div id="-constructors"></div>

#### `Default Constructor`

```csharp
public CompositeAction()
```

- **Description:** Initializes a new instance
- **Note:** This constructor is intended **only for use by the Unity Inspector** when using `[SerializeReference]`.

#### `Params Constructor`

```csharp
public CompositeAction(params IAction[] actions)
```

- **Description:** Initializes a new instance with the specified array of actions.
- **Parameter:** `actions` – One or more actions to include in the group.
- **Throws:** `ArgumentNullException` if `actions` is null.

#### `IEnumerable Constructor`

```csharp
public CompositeAction(IEnumerable<IAction> actions)
```

- **Description:** Initializes a new instance with the specified collection of actions.
- **Parameter:** `actions` – A collection of actions to include in the group.
- **Throws:** `ArgumentNullException` if `actions` is null.

---

### 🏹 Methods

#### `Invoke()`

```csharp
public void Invoke()
```

- **Description:** Invokes all actions in the group sequentially.