# 🧩 BaseRequest&lt;T&gt;

```csharp
public class BaseRequest<T> : IRequest<T>
```

- **Description:** Represents a request action with <b>one input argument</b>.
- **Inheritance:** [IRequest&lt;T&gt;](IRequest%601.md)
- **Type parameter:** `T` — type of the argument.
- **Note:** Supports Odin Inspector

---

## 🔑 Properties

#### `Required`

```csharp
public bool Required { get; }
```

- **Description:** Indicates whether the request is currently required.

#### `Arg`

```csharp
public T Arg { get; }
```

- **Description:** The stored argument.

---

## 🏹 Methods

#### `Invoke(T)`

```csharp
public void Invoke(T arg);
```

- **Description:** Marks the request as required and stores the argument.
- **Parameter:** `arg` — the input argument.

#### `Consume(out T)`

```csharp
public bool Consume(out T arg);
```

- **Description:** Attempts to consume the request and retrieve the argument.
- **Output:** `arg` — the argument if successfully consumed.
- **Returns:** `true` if the request was required and is now consumed.

#### `TryGet(out T)`

```csharp
public bool TryGet(out T arg);
```

- **Description:** Attempts to retrieve the argument without consuming the request.
- **Output:** `arg` — the stored argument.
- **Returns:** `true` if the request is currently required.