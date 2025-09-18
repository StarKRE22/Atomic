# 🧩 InlineAction Classes

The **InlineAction** classes provide wrappers around standard `System.Action` delegates.
They implement the corresponding [IAction](IAction.md) interfaces and allow invoking actions directly, optionally with
parameters.

They also support implicit conversion from the underlying `Action` delegates and, if using Odin Inspector, inline
display and buttons.

---

<details>
  <summary>
    <h2>🧩 InlineAction</h2>
    <br> Represents a <b>parameterless action</b> that can be invoked.
  </summary>

<br>

```csharp
public class InlineAction : IAction
```

---

### 🏗️ Constructors

#### `InlineAction(Action action)`

```csharp
public InlineAction(Action action)
```

- **Description:** Initializes a new instance with the specified action.
- **Parameter:** `action` – The action to invoke.
- **Throws:** `ArgumentNullException` if `action` is null.

---

### 🏹 Methods

#### `Invoke()`

```csharp
public void Invoke()
```

- **Description:** Invokes the wrapped action.

#### `ToString()`

```csharp
public override string ToString();
```

- **Description:** Returns a string that represents the method name of action.
- **Returns:** A string representation of the method name of delegate.

---

### 🪄 Operators

#### `operator InlineAction(Action)`

```csharp
public static implicit operator InlineAction(Action action);
```

- **Description:** Implicitly converts a delegate of type `Action` to a `InlineAction`.
- **Parameter:** `action` – the delegate to wrap.
- **Returns:** A new `InlineAction` containing the specified delegate.

---

### 🗂 Example of Usage

```csharp
IAction helloAction = new InlineAction(() => Console.WriteLine("Hello World!"));
helloAction.Invoke(); // Output: Hello World!
```

</details>

---

<details>
  <summary>
    <h2>🧩 InlineAction&lt;T&gt;</h2>
    <br> Represents an action <b>with one parameter</b> that can be invoked.
  </summary>

<br>

```csharp
public class InlineAction<T> : IAction<T>
```

- **Type parameter** `T` — the input parameter

---

### 🏗️ Constructors

#### `InlineAction(Action<T> action)`

```csharp
public InlineAction(Action<T> action)
```

- **Description:** Initializes a new instance with the specified action.
- **Parameter:** `action` – The action to invoke.
- **Throws:** `ArgumentNullException` if `action` is null.

---

### 🏹 Methods

#### `Invoke(T arg)`

```csharp
public void Invoke(T arg)
```

- **Description:** Invokes the wrapped action with the specified argument.
- **Parameter:** `arg` – The argument to pass to the action.

#### `ToString()`

```csharp
public override string ToString();
```

- **Description:** Returns a string that represents the method name of action.
- **Returns:** A string representation of the method name of delegate.

---

### 🪄 Operators

#### `operator InlineAction<T>(Action<T>)`

```csharp
public static implicit operator InlineAction<T>(Action<T> action);
```

- **Description:** Implicitly converts a delegate of type `Action<T>` to a `InlineAction<T>`.
- **Type Parameter:** `T` — input parameter.
- **Parameter:** `action` – the delegate to wrap.
- **Returns:** A new `InlineAction<T>` containing the specified delegate.

### 🗂 Example of Usage

```csharp
var destroyAction = new InlineAction<GameObject>(GameObject.Destroy);
destroyAction.Invoke(gameObject);
```

</details>

---

<details>
  <summary>
    <h2>🧩 InlineAction&lt;T1, T2&gt;</h2>
    <br> Represents an action <b>with two parameters</b> that can be invoked.
  </summary>

<br>

```csharp
public class InlineAction<T1, T2> : IAction<T1, T2>
```

- **Type parameters**
    - `T1` — the first argument
    - `T2` — the second argument

---

### 🏗️ Constructors

#### `InlineAction(Action<T1, T2> action)`

```csharp
public InlineAction(Action<T1, T2> action)
```

- **Description:** Initializes a new instance with the specified action.
- **Parameter:** `action` – The action to invoke.
- **Throws:** `ArgumentNullException` if `action` is null.

---

### 🏹 Methods

#### `Invoke(T1 arg1, T2 arg2)`

```csharp
public void Invoke(T1 arg1, T2 arg2)
```

- **Description:** Invokes the wrapped action with the specified arguments.
- **Parameters:**
    - `arg1` – The first argument
    - `arg2` – The second argument

#### `ToString()`

```csharp
public override string ToString();
```

- **Description:** Returns a string that represents the method name of action.
- **Returns:** A string representation of the method name of delegate.

---

### 🪄 Operators

#### `operator InlineAction<T1, T2>(Action<T1, T2>)`

```csharp
public static implicit operator InlineAction<T1, T2>(Action<T1, T2> action);
```

- **Description:** Implicitly converts a delegate of type `Action<T1, T2>` to a `InlineAction<T1, T2>`.
- **Type Parameters:**
    - `T1` — the first argument
    - `T2` — the second argument
- **Parameter:** `action` – the delegate to wrap.
- **Returns:** A new `InlineAction<T1, T2>` containing the specified delegate.

---

### 🗂 Example of Usage

```csharp
var damageAction = new InlineAction<Character, int>(
    (character, damage) => character.TakeDamage(damage));

damageAction.Invoke(enemy, 5);
```

</details>

---

<details>
  <summary>
    <h2>🧩 InlineAction&lt;T1, T2, T3&gt;</h2>
    <br> Represents an action <b>with three parameters</b> that can be invoked.
  </summary>

<br>

```csharp
public class InlineAction<T1, T2, T3> : IAction<T1, T2, T3>
```

- **Description:** Represents an action with three parameters that can be invoked.
- **Type parameters**
    - `T1` — the first argument
    - `T2` — the second argument
    - `T3` — the third argument

---

### 🏗️ Constructors

#### `InlineAction(Action<T1, T2, T3> action)`

```csharp
public InlineAction(Action<T1, T2, T3> action)
```

- **Description:** Initializes a new instance with the specified action.
- **Parameter:** `action` – The action to invoke.
- **Throws:** `ArgumentNullException` if `action` is null.

---

### 🏹 Methods

#### `Invoke(T1 arg1, T2 arg2, T3 arg3)`

```csharp
public void Invoke(T1 arg1, T2 arg2, T3 arg3)
```

- **Description:** Invokes the wrapped action with the specified arguments.
- **Parameters:**
    - `arg1` – The first argument
    - `arg2` – The second argument
    - `arg3` – The third argument

#### `ToString()`

```csharp
public override string ToString();
```

- **Description:** Returns a string that represents the method name of action.
- **Returns:** A string representation of the method name of delegate.

---

### 🪄 Operators

#### `operator InlineAction<T1, T2, T3>(Action<T1, T2, T3>)`

```csharp
public static implicit operator InlineAction<T1, T2, T3>(Action<T1, T2, T3> action);
```

- **Description:** Implicitly converts a delegate of type `Action<T1, T2, T3>` to a `InlineAction<T1, T2, T3>`.
- **Type Parameters:**
    - `T1` — the first argument
    - `T2` — the second argument
    - `T3` — the third argument
- **Parameter:** `action` – the delegate to wrap.
- **Returns:** A new `InlineAction<T1, T2, T3>` containing the specified delegate.

---

### 🗂 Example of Usage

```csharp
var moveResourcesAction = new InlineAction<Storage, Storage, int>((source, destination, amount) => 
{
    source.SpendResources(amount);
    destination.EarnResources(amount);
});

moveResourcesAction.Invoke(storageA, storageB, 100);
```

</details>

---

<details>
  <summary>
    <h2>🧩 InlineAction&lt;T1, T2, T3, T4&gt;</h2>
    <br> Represents an action <b>with four parameters</b> that can be invoked.
  </summary>

<br>

```csharp
public class InlineAction<T1, T2, T3, T4> : IAction<T1, T2, T3, T4>
```

- **Type parameters**
    - `T1` — the first argument
    - `T2` — the second argument
    - `T3` — the third argument
    - `T4` — the fourth argument

---

### 🏗️ Constructors

#### `InlineAction(Action<T1, T2, T3, T4> action)`

```csharp
public InlineAction(Action<T1, T2, T3, T4> action)
```

- **Description:** Initializes a new instance with the specified action.
- **Parameter:** `action` – The action to invoke.
- **Throws:** `ArgumentNullException` if `action` is null.

---

### 🏹 Methods

#### `Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4)`

```csharp
public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
```

- **Description:** Invokes the wrapped action with the specified arguments.
- **Parameters:**
    - `arg1` – The first argument
    - `arg2` – The second argument
    - `arg3` – The third argument
    - `arg4` – The fourth argument

#### `ToString()`

```csharp
public override string ToString();
```

- **Description:** Returns a string that represents the method name of action.
- **Returns:** A string representation of the method name of delegate.

---

### 🪄 Operators

#### `operator InlineAction<T1, T2, T3, T4>(Action<T1, T2, T3, T4>)`

```csharp
public static implicit operator InlineAction<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action);
```

- **Description:** Implicitly converts a delegate of type `Action<T1, T2, T3, T4>` to a `InlineAction<T1, T2, T3, T4>`.
- **Type Parameters:**
    - `T1` — the first argument
    - `T2` — the second argument
    - `T3` — the third argument
    - `T4` — the third argument
- **Parameter:** `action` – the delegate to wrap.
- **Returns:** A new `InlineAction<T1, T2, T3, T4>` containing the specified delegate.

---

### 🗂 Example of Usage

```csharp
var moveAction = new InlineAction<Transform, Vector3, float, float>(
    (transform, direction, speed, deltaTime) => transform.position += direction * (speed * deltaTime)    
);
moveAction.Invoke(transform, Vector3.forward, 10, 0.02);
```

</details>

---

## 📌 Best Practice

`InlineAction` is ideal for creating actions for specific game objects using **lambda expressions**, making it easy to
define custom behavior inline for events, commands, or reactive systems.

Below is an example of creating a weapon that shoots bullets, manages ammo, and triggers a cooldown using
`InlineAction`:

```csharp
public sealed class WeaponInstaller : SceneEntityInstaller<IWeapon>
{
    [SerializeField] private GameEntity _owner;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private ReactiveVariable<int> _ammo = 100;
    [SerializeField] private Cooldown _cooldown = 0.5f;

    public override void Install(IWeapon weapon)
    {
        weapon.AddFireEvent(new BaseEvent());
        weapon.AddFireAction(new InlineAction(() =>
        {
            if (_ammo.Value <= 0 || !_cooldown.IsCompleted())
                return;

            _ammo.Value--;
            
            BulletUseCase.Spawn(
                GameContext.Instance,
                _firePoint.position,
                _firePoint.rotation,
                _owner.GetTeamType().Value
            );
            
            _cooldown.ResetTime();
            weapon.GetFireEvent().Invoke();
        }));
    }
}
```