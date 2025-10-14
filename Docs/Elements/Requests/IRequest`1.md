# 🧩 IRequest&lt;T&gt;

Represents a request action with <b>one input argument</b>.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Properties](#-properties)
        - [Required](#required)
        - [Arg](#arg)
    - [Methods](#-methods)
        - [Invoke(T)](#invoket)
        - [Consume(out T)](#consumeout-t)
        - [TryGet(out T)](#trygetout-t)

---

## 🗂 Example of Usage

```csharp
IRequest<Character> damageRequest = ...

// Trigger the request from gameplay logic
damageRequest.Invoke(targetCharacter);

// Somewhere in a system that processes damage
if (damageRequest.TryGet(out Character target))
{
    Debug.Log($"Applying damage is required to {target.Name}");
}

if (damageRequest.Consume(out target))
{
    Debug.Log("Damage request handled and consumed.");
}
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface IRequest<T> : IAction<T>
```

- **Description:** Represents a request action with <b>one input argument</b>.
- **Type parameter:** `T` — the type of the argument.
- **Inheritance:** [IAction&lt;T&gt;](../Actions/IAction%601.md)

---

### 🔑 Properties

#### `Required`

```csharp
public bool Required { get; }
```

- **Description:** Indicates whether the request must be handled.

#### `Arg`

```csharp
public T Arg { get; }
```

- **Description:** Gets the request argument.

---

### 🏹 Methods

#### `Invoke(T)`

```csharp
public void Invoke(T arg);
```

- **Description:** Executes the request.
- **Parameter:** `arg` — the input parameter
- **Note:** This method derived from `IAction<T>`

#### `Consume(out T)`

```csharp
public bool Consume(out T arg);
```

- **Description:** Attempts to consume the request and retrieve the argument.
- **Output:** `arg` — the argument value if the request was consumed successfully.
- **Returns:** `true` if successfully consumed.

#### `TryGet(out T)`

```csharp
public bool TryGet(out T arg);
```

- **Description:** Attempts to retrieve the argument.
- **Output:** `arg` — the argument value if successfully retrieved.
- **Returns:** `true` if the argument was retrieved successfully.