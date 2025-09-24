
<details>
  <summary>
    <h2>🧩 IRequest&lt;T&gt;</h2>
    <br> Represents a request action with <b>one input argument</b>.
  </summary>

<br>

```csharp
public interface IRequest<T> : IAction<T>
```

- **Type parameter:** `T` — the type of the argument.

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
- **Note:** This method derived from [IAction&lt;T&gt;.Invoke()](../Actions/IAction.md#invoket)

#### `Consume(out T)`

```csharp
public bool Consume(out T arg);
```

- **Description:** Attempts to consume the request and retrieve the argument.
- **Output parameter:** `arg` — the argument value if the request was consumed successfully.
- **Returns:** `true` if successfully consumed.

#### `TryGet(out T arg)`

```csharp
public bool TryGet(out T arg);
```

- **Description:** Attempts to retrieve the argument.
- **Output parameter:** `arg` — the argument value if successfully retrieved.
- **Returns:** `true` if the argument was retrieved successfully.

</details>