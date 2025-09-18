# 🧩 IRequest

Represents a **deferred action** that can be executed at a later time. It is particularly useful for scenarios where
input is collected in one phase (e.g., `Update`) but processed in another (e.g., `FixedUpdate`). Requests also help *
*prevent duplicate commands** by ensuring the same request is not processed multiple times while active.

It extends the [IAction](../Actions/IAction.md) interface and provide s**required flags** and **argument retrieval /
consumption** functionality.

---

<details>
  <summary>
    <h2>🧩 IRequest</h2>
    <br> Represents a <b>parameterless</b> request action.
  </summary>

<br>

```csharp
public interface IRequest : IAction
```

---

### 🔑 Properties

#### `Required`

```csharp
public bool Required { get; }
```

- **Description:** Indicates whether the request must be handled.

---

### 🏹 Methods

#### `Invoke()`

```csharp
public void Invoke();
```

- **Description:** Executes the request.
- **Note:** This method derived from [IAction.Invoke()](../Actions/IAction.md#invoke)

#### `Consume()`

```csharp
public bool Consume();
```

- **Description:** Attempts to consume the request.
- **Returns:** `true` if successfully consumed; otherwise `false`.

</details>


---

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

---

<details>
  <summary>
    <h2>🧩 IRequest&lt;T1, T2&gt;</h2>
    <br> Represents a request action with <b>two input arguments</b>.
  </summary>

<br>

```csharp
public interface IRequest<T1, T2> : IAction<T1, T2>
```

- **Type parameters:**
    - `T1` — first argument
    - `T2` — second argument

---

### 🔑 Properties

#### `Required`

```csharp
public bool Required { get; }
```

- **Description:** Indicates whether the request must be handled.

#### `Arg1`

```csharp
public T1 Arg1 { get; }
```

- **Description:** Get the first argument of the request.

#### `Arg2`

```csharp
public T2 Arg2 { get; }
```

- **Description:** Get the second argument of the request.

---

### 🏹 Methods

#### `Invoke(T1, T2)`

```csharp
public void Invoke(T1 arg1, T2 arg2);
```

- **Description:** Executes the request.
- **Parameters:**
    - `arg1` — the first input parameter
    - `arg2` — the second input parameter
- **Note:** This method derived from [IAction<T1, T2>.Invoke()](../Actions/IAction.md#invoket1-t2)

#### `Consume(out T1, out T2)`

```csharp
public bool Consume(out T1 arg1, out T2 arg2);
```

- **Description:** Attempts to consume the request and retrieve the arguments.
- **Output parameters:**
    - `arg1` — the first argument value if the request was consumed successfully.
    - `arg2` — the second argument value if the request was consumed successfully.
- **Returns:** `true` if successfully consumed.

#### `TryGet(out T1, out T2)`

```csharp
public bool TryGet(out T1 arg1, out T2 arg2);
```

- **Description:**  Attempts to retrieve both arguments.
- **Output parameters:**
    - `arg1` — the first argument value if successfully retrieved.
    - `arg2` — the second argument value if successfully retrieved.
- **Returns:** `true` if the argument was retrieved successfully.

</details>

---

<details>
  <summary>
    <h2>🧩 IRequest&lt;T1, T2, T3&gt;</h2>
    <br> Represents a request action with <b>three input arguments</b>.
  </summary>

<br>

```csharp
public interface IRequest<T1, T2, T3> : IAction<T1, T2, T3>
```

- **Type parameters:**
    - `T1` — first argument
    - `T2` — second argument
    - `T3` — third argument

---

### 🔑 Properties

#### `Required`

```csharp
public bool Required { get; }
```

- **Description:** Indicates whether the request must be handled.

#### `Arg1`

```csharp
public T1 Arg1 { get; }
```

- **Description:** Get the first argument of the request.

#### `Arg2`

```csharp
public T2 Arg2 { get; }
```

- **Description:** Get the second argument of the request.

#### `Arg3`

```csharp
public T3 Arg3 { get; }
```

- **Description:** Get the third argument of the request.

---

### 🏹 Methods

#### `Invoke(T1, T2, T3)`

```csharp
public void Invoke(T1 arg1, T2 arg2, T3 arg3);
```

- **Description:** Executes the request.
- **Parameters:**
    - `arg1` — the first input parameter
    - `arg2` — the second input parameter
    - `arg3` — the third input parameter
- **Note:** This method derived from [IAction<T1, T2, T3>.Invoke()](../Actions/IAction.md#invoket1-t2-t3)

#### `Consume(out T1, out T2, out T3)`

```csharp
public bool Consume(out T1 arg1, out T2 arg2, out T3 arg3);
```

- **Description:** Attempts to consume the request and retrieve the arguments.
- **Output parameters:**
    - `arg1` — the first argument value if the request was consumed successfully.
    - `arg2` — the second argument value if the request was consumed successfully.
    - `arg3` — the third argument value if the request was consumed successfully.
- **Returns:** `true` if successfully consumed.

#### `TryGet(out T1, out T2, out T3)`

```csharp
public bool TryGet(out T1 arg1, out T2 arg2, out T3 arg3);
```

- **Description:**  Attempts to retrieve both arguments.
- **Output parameters:**
    - `arg1` — the first argument value if successfully retrieved.
    - `arg2` — the second argument value if successfully retrieved.
    - `arg3` — the third argument value if successfully retrieved.
- **Returns:** `true` if the argument was retrieved successfully.

</details>

---


<details>
  <summary>
    <h2>🧩 IRequest&lt;T1, T2, T3, T4&gt;</h2>
    <br> Represents a request action with <b>four input arguments</b>.
  </summary>

<br>

```csharp
public interface IRequest<T1, T2, T3, T4> : IAction<T1, T2, T3, T4>
```

- **Type parameters:**
    - `T1` — first argument
    - `T2` — second argument
    - `T3` — third argument
    - `T4` — fourth argument

---

### 🔑 Properties

#### `Required`

```csharp
public bool Required { get; }
```

- **Description:** Indicates whether the request must be handled.

#### `Arg1`

```csharp
public T1 Arg1 { get; }
```

- **Description:** Get the first argument of the request.

#### `Arg2`

```csharp
public T2 Arg2 { get; }
```

- **Description:** Get the second argument of the request.

#### `Arg3`

```csharp
public T3 Arg3 { get; }
```

- **Description:** Get the third argument of the request.

#### `Arg4`

```csharp
public T4 Arg4 { get; }
```

- **Description:** Get the fourth argument of the request.

---

### 🏹 Methods

#### `Invoke(T1, T2, T3, T4)`

```csharp
public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
```

- **Description:** Executes the request.
- **Parameters:**
    - `arg1` — the first input parameter
    - `arg2` — the second input parameter
    - `arg3` — the third input parameter
    - `arg4` — the fourth input parameter
- **Note:** This method derived from [IAction<T1, T2, T3, T4>.Invoke()](../Actions/IAction.md#invoket1-t2-t3-t4)

#### `Consume(out T1, out T2, out T3, out T4)`

```csharp
public bool Consume(out T1 arg1, out T2 arg2, out T3 arg3, out T4 arg4);
```

- **Description:** Attempts to consume the request and retrieve the arguments.
- **Output parameters:**
    - `arg1` — the first argument value if the request was consumed successfully.
    - `arg2` — the second argument value if the request was consumed successfully.
    - `arg3` — the third argument value if the request was consumed successfully.
    - `arg4` — the fourth argument value if the request was consumed successfully.
- **Returns:** `true` if successfully consumed.

#### `TryGet(out T1, out T2, out T3, out T4)`

```csharp
public bool TryGet(out T1 arg1, out T2 arg2, out T3 arg3, out T4 arg4);
```

- **Description:**  Attempts to retrieve both arguments.
- **Output parameters:**
    - `arg1` — the first argument value if successfully retrieved.
    - `arg2` — the second argument value if successfully retrieved.
    - `arg3` — the third argument value if successfully retrieved.
    - `arg4` — the fourth argument value if successfully retrieved.
- **Returns:** `true` if the argument was retrieved successfully.

</details>

---

## 🗂 Example of Usage

Below are examples of using `IRequest` with the `Atomic.Entities` framework.

---

### Example #1: Move Input Using Requests

This example demonstrates how a `MoveController` can **produce a request in update**, and `MoveBehaviour` can **consume
it later in fixed update**:

```csharp
//Add to entity "MoveRequest" as "BaseRequest<Vector3>"
entity.AddMoveRequest(new BaseRequest<Vector3>());
entity.AddBehaviour<MoveController>();
entity.AddBehaviour<MoveBehaviour>();
```

```csharp
// MoveController produces the request
public sealed class MoveController : IEntityInit, IEntityTick
{
    private IRequest<Vector3> _moveRequest;
    
    public void Init(IEntity entity)
    {
        _moveRequest = entity.GetMoveRequest();    
    }

    public void Tick(IEntity entity, float deltaTime)
    {
        float dx = Input.GetAxis("Horizontal");
        float dz = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(dx, 0, dz );
        _moveRequest.Invoke(moveDirection);
    }
}
```

```csharp
// MoveBehaviour consumes the request
public sealed class MoveBehaviour : IEntityInit, IEntityFixedTick
{
    private Transform _transform;
    private IValue<float> _moveSpeed;
    private IRequest<Vector3> _moveRequest;

    public void Init(IEntity entity)
    {
        _transform = entity.GetTransform();
        _moveSpeed = entity.GetMoveSpeed();
        _moveRequest = entity.GetMoveRequest();
    }

    public void FixedTick((IEntity entity, float deltaTime))
    {
        if (_moveRequest.Consume(out Vector3 moveDirection))
            _transform.position += moveDirection * Time.fixedDeltaTime * _moveSpeed.Value;
    }
}
```

---

### Example #2: Target Following Using Requests

In this example, a `AIFollowBehaviour` triggers a movement request, which is later processed by `MoveBehaviour`:

```csharp
//Add to entity "MoveRequest" as "BaseRequest<Vector3>"
entity.AddMoveRequest(new BaseRequest<Vector3>());
entity.AddBehaviour<AIFollowBehaviour>();
entity.AddBehaviour<MoveBehaviour>();
```

```csharp
// AIFollowBehaviour produces the request
public sealed class AIFollowBehaviour : IEntityInit, IEntityTick
{
    private IValue<IEntity> _target;
    private IValue<Vector3> _position;
    private IRequest<Vector3> _moveRequest;
    private IValue<float> _stoppingDistance;
    
    public void Init(IEntity entity)
    {
        _target = entity.GetTarget();
        _position = entity.GetPosition();
        _moveRequest = entity.GetMoveRequest();    
        _stoppingDistance = entity.GetStoppingDistance();
    }

    public void Tick(IEntity entity, float deltaTime)
    {
        IEntity target = _target.Value;
        if (target == null)
            return;
        
        Vector3 delta = _target.GetPosition().Value - _position.Value;
        if (delta.magnitude <= _stoppingDistance.Value)
            return;
        
        Vector3 moveDirection = delta.normalized;
        _moveRequest.Invoke(moveDirection);
    }
}
```
```csharp
// MoveBehaviour consumes the request
public sealed class MoveBehaviour : IEntityInit, IEntityFixedTick
{
    private Transform _transform;
    private IValue<float> _moveSpeed;
    private IRequest<Vector3> _moveRequest;

    public void Init(IEntity entity)
    {
        _transform = entity.GetTransform();
        _moveSpeed = entity.GetMoveSpeed();
        _moveRequest = entity.GetMoveRequest();
    }

    public void FixedTick((IEntity entity, float deltaTime))
    {
        if (_moveRequest.Consume(out Vector3 moveDirection))
            _transform.position += moveDirection * Time.fixedDeltaTime * _moveSpeed.Value;
    }
}
```

## 📝Notes

- **Deferred execution** – Requests can be stored and processed later via `Consume()`.
- **Duplicate prevention** – Multiple identical requests can be avoided because `Consume()` only processes requests that
  are still required.
- **Required** – Indicates whether the request currently needs handling.
- **TryGet / Consume** – Methods to safely inspect or process the request arguments.