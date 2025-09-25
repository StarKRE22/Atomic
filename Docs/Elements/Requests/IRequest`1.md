# 🧩 IRequest&lt;T&gt;

```csharp
public interface IRequest<T> : IAction<T>
```

- **Description:** Represents a request action with <b>one input argument</b>.
- **Type parameter:** `T` — the type of the argument.
- **Inheritance:** [IAction&lt;T&gt;](../Actions/IAction%601.md)

---

## 🔑 Properties

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

#### `TryGet(out T arg)`

```csharp
public bool TryGet(out T arg);
```

- **Description:** Attempts to retrieve the argument.
- **Output:** `arg` — the argument value if successfully retrieved.
- **Returns:** `true` if the argument was retrieved successfully.