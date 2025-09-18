# 🧩 Action Interfaces

The **IAction** interfaces define a family of contracts for executing parameterized actions. They provide a lightweight
abstraction for invoking logic, often used in event systems, command patterns, or reactive programming.

---

<details>
  <summary>
    <h2>🧩 IAction</h2>
    <br> Represents a <b>parameterless executable action</b>.
  </summary>

<br>

```csharp
public interface IAction
```

### 🏹 Method
#### `Invoke()`
```csharp
void Invoke();
```
- **Description:** Executes the action logic

### 🗂 Example of Usage
```csharp
public sealed class HelloWorldAction : IAction
{
    public void Invoke() => Console.WriteLine("Hello World!");
}

// Usage
IAction action = new HelloWorldAction();
action.Invoke(); // Output: Hello World!
```

</details>

---

<details>
  <summary>
    <h2>🧩 IAction&lt;T&gt;</h2>
    <br> Represents an executable action that <b>takes one argument</b>.
  </summary>

<br>

```csharp
public interface IAction<in T>
```
- **Type parameter:** `T` — the input parameter

### 🏹 Method
#### `Invoke(T)`
```csharp
void Invoke(T arg);
```
- **Description:** Executes the action with the specified argument
- **Parameter:** `arg` — the input parameter

### 🗂 Example of Usage
```csharp
public sealed class DestroyGameObjectAction : IAction<GameObject>
{
    public void Invoke(GameObject go) => GameObject.Destroy(go);
}

// Usage
IAction<GameObject> action = new DestroyGameObjectAction();
action.Invoke(gameObject);
```

</details>

---

<details>
  <summary>
    <h2>🧩 IAction&lt;T1, T2&gt;</h2>
    <br> Represents an executable action that <b>takes two arguments</b>.
  </summary>

<br>

```csharp
public interface IAction<in T1, in T2>
```
- **Type parameters:**
  - `T1` — the first argument
  - `T2` — the second argument

### 🏹 Method
#### `Invoke(T1, T2)`
```csharp
void Invoke(T1 arg1, T2 arg2);
```
- **Description:** Executes the action with the specified arguments
- **Parameters:**
  - `arg1` — the first argument
  - `arg2` — the second argument

### 🗂 Example of Usage
```csharp
public sealed class DealDamageAction : IAction<Character, int>
{
    public void Invoke(Character character, int damage) => character.TakeDamage(damage);
}

// Usage
IAction<Character, int> action = new DealDamageAction();
action.Invoke(enemy, 5);
```

</details>

---

<details>
  <summary>
    <h2>🧩 IAction&lt;T1, T2, T3&gt;</h2>
    <br> Represents an executable action that <b>takes three arguments</b>.
  </summary>

<br>

```csharp
public interface IAction<in T1, in T2, in T3>
```
- **Type parameters:**
  - `T1` — the first argument
  - `T2` — the second argument
  - `T3` — the third argument

### 🏹 Method
#### `Invoke(T1, T2, T3)`
```csharp
void Invoke(T1 arg1, T2 arg2, T3 arg3);
```
- **Description:** Executes the action with the specified arguments
- **Parameters:**
  - `arg1` — the first argument
  - `arg2` — the second argument
  - `arg3` — the third argument

### 🗂 Example of Usage
```csharp
public sealed class MoveResourcesAction : IAction<Storage, Storage, int>
{
    public void Invoke(Storage source, Storage destination, int amount)
    {
        source.SpendResources(amount);
        destination.EarnResources(amount);
    }
}

// Usage
IAction<Storage, Storage, int> action = new MoveResourcesAction();
action.Invoke(storageA, storageB, 100);
```

</details>

---

<details>
  <summary>
    <h2>🧩 IAction&lt;T1, T2, T3, T4&gt;</h2>
    <br> Represents an executable action that <b>takes four arguments</b>.
  </summary>

<br>

```csharp
public interface IAction<in T1, in T2, in T3, in T4>
```
- **Type parameters:**
  - `T1` — the first argument
  - `T2` — the second argument
  - `T3` — the third argument
  - `T4` — the fourth argument

### 🏹 Method
#### `Invoke(T1, T2, T3, T4)`
```csharp
void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
```
- **Description:** Executes the action with the specified arguments
- **Parameters:**
  - `arg1` — the first argument
  - `arg2` — the second argument
  - `arg3` — the third argument
  - `arg4` — the fourth argument

### 🗂 Example of Usage
```csharp
public sealed class MoveTransformAction : IAction<Transform, Vector3, float, float>
{
    public void Invoke(Transform transform, Vector3 direction, float speed, float deltaTime) 
    {
        transform.position += direction * (speed * deltaTime);
    }
}

// Usage
IAction<Transform, Vector3, float, float> action = new MoveTransformAction();
action.Invoke(transform, Vector3.forward, 10, 0.02);
```

</details>