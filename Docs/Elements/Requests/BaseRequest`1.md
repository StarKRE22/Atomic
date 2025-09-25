
<details>
  <summary>
    <h2>🧩 BaseRequest&lt;T&gt;</h2>
    <br> Represents a request action with <b>one input argument</b>.
  </summary>

<br>

```csharp
public class BaseRequest<T> : IRequest<T>
```

- **Type parameter:** `T` — type of the argument.

---

### 🔑 Properties

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

### 🏹 Methods

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
- **Output parameter:** `arg` — the argument if successfully consumed.
- **Returns:** `true` if the request was required and is now consumed.

#### `TryGet(out T)`

```csharp
public bool TryGet(out T arg);
```

- **Description:** Attempts to retrieve the argument without consuming the request.
- **Output parameter:** `arg` — the stored argument.
- **Returns:** `true` if the request is currently required.

</details>